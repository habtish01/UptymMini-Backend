using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Collections.Generic;
using MimeKit;
using Uptym.Core.Common;
using Uptym.Core.Interfaces;
using Uptym.Data.DataContext;
using Uptym.Data.DbModels;
using Uptym.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Uptym.Data.DbModels.SecuritySchema;
using Uptym.Services.Maintenance.ScheduleTriggers;
using Quartz;
using System.Threading.Tasks;
using System.Collections.Specialized;
using Quartz.Impl;
using Uptym.Controllers;
using Quartz.Spi;
using Uptym.Extensions;

namespace Uptym
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            Serilog.Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(configuration).CreateLogger();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {

            services.AddCors();
 
            services.AddMvc(cfg =>
            {
                cfg.EnableEndpointRouting = false;
                AuthorizationPolicy policy = new AuthorizationPolicyBuilder()
                 .RequireAuthenticatedUser()
                 .Build();
                cfg.Filters.Add(new AuthorizeFilter(policy));
            }).SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme‌​)
                    .RequireAuthenticatedUser().Build());
            });

            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("UptymConnectionString")));

            services.AddDefaultIdentity<ApplicationUser>()
                .AddRoles<ApplicationRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddTokenProvider("UptymApp", typeof(DataProtectorTokenProvider<ApplicationUser>));

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
                options.Password.RequiredUniqueChars = 1;

                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromHours(10);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings
                options.User.RequireUniqueEmail = true;
                //options.SignIn.RequireConfirmedEmail = true;
                //options.SignIn.RequireConfirmedPhoneNumber = true;
            });
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                var signingKey = Convert.FromBase64String(Configuration["Jwt:Key"]);
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    NameClaimType = ClaimTypes.NameIdentifier,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(signingKey)
                };
            });
            services.AddSwaggerGen();

            // automapper configuration
            services.AddAutoMapper();

            services.AddHttpClient();

            // email service configuration
            services.AddSingleton<IEmailConfiguration>(Configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>());
            services.AddSingleton<IPaypalConfiguration>(Configuration.GetSection("PaypalConfiguration").Get<PaypalConfiguration>());
            //services.AddScoped<IEmailService, EmailService>();
            //// send error to specific emails configuration
            services.AddScoped<ISendEmailWithErrorConfiguration, SendEmailWithErrorConfiguration>();
            //services.AddSingleton<ISendEmailWithErrorConfiguration>(Configuration.GetSection("SendEmailWithErrorConfiguration").Get<SendEmailWithErrorConfiguration>());

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            //services.AddSingleton(provider => GetScheduler().Result);

            //services.AddSingleton<Uptym.Services.Maintenance.ScheduleTriggers.ScheduleTriggerService>();

            //Auto Schedule Jobs
            services.AddBackgroundScheduleService(Configuration);

            // Registe our services with Autofac container
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterModule(new AutoFacConfiguration());
            builder.Populate(services);
            IContainer container = builder.Build();

            //Create the IServiceProvider based on the container.
            return new AutofacServiceProvider(container);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, AppDbContext appDbContext, ILoggerFactory loggerFactory,
            ISendEmailWithErrorConfiguration sendEmailWithErrorConfiguration, IServiceProvider serviceProvider)
        {
            
        


        loggerFactory.AddSerilog();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();



           // var _allowIp = configuration["ClientSubscriber:Url"];
           // app.UseCors(x => x.WithOrigins(_allowIp).AllowAnyMethod().AllowAnyHeader());
             app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()); app.UseAuthentication(); app.UseMvc();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Uptym V1");
            });
            DataSeedInitializations.Seed(appDbContext, serviceProvider);

            app.UseCors(builder =>
                builder.AllowAnyOrigin()
                       .AllowAnyHeader()
                       .AllowAnyMethod());


            app.UseExceptionHandler(appError =>
            {
                appError.Run(async httpContext =>
                {
                    httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    httpContext.Response.ContentType = "application/json";

                    IExceptionHandlerFeature contextFeature = httpContext.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        // log error
                        Serilog.Log.Error($"Something went wrong: {contextFeature.Error}");



                        //send error to support email in case of flag in appsettings is true
                        if (sendEmailWithErrorConfiguration.AllowSend)
                        {
                            if (!string.IsNullOrEmpty(sendEmailWithErrorConfiguration.ToEmails))
                            {
                                var toEmails = new List<string>();
                                toEmails = sendEmailWithErrorConfiguration.ToEmails.Split(',').ToList();

                                var emailList = new List<EmailAddress>();
                                foreach (var item in toEmails)
                                {
                                    emailList.Add(new EmailAddress() { Name = item, Address = item });
                                }

                                var builder = new BodyBuilder();
                                builder.TextBody = contextFeature.Error.Message;


                            }
                        }
                    }
                });
            });

            app.UseAuthentication();
            app.UseMvc();
            app.UseStaticFiles();

        }
        private async Task<IScheduler> GetScheduler()
        {
            var properties = new NameValueCollection
            {
                { "quartz.scheduler.instanceName", "ScheduleTriggerService" },
                { "quartz.threadPool.type", "Quartz.Simpl.DefaultThreadPool" },
                { "quartz.threadPool.threadCount", "1" },
                { "quartz.jobStore.type", "Quartz.Simpl.RAMJobStore, Quartz" }
            };
            var schedulerFactory = new StdSchedulerFactory(properties);
            var scheduler = await schedulerFactory.GetScheduler();
            await scheduler.Start();                                                                                  
            return scheduler;
        }
    }
}
