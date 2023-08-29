using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using Uptym.Services.Maintenance.ScheduleTriggers.Common;
using Uptym.Services.Maintenance.ScheduleTriggers.Jobs;
using Uptym.Services.Subscription.ScheduleTrigger;

namespace Uptym.Extensions
{
    public static class BackgroundServiceExtensions
    {
        public static IServiceCollection AddBackgroundScheduleService(this IServiceCollection services, IConfiguration configuration)
        {
            ////Background Task Scheduler
            //Register Container
            services.AddSingleton<IJobFactory, UptymJobFactory>();
            services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();
            services.AddSingleton<UptymJobRunner>();
            services.AddHostedService<UptymHostedService>();


            //Register Jobs
            services.AddSingleton<NotificationJob>();
            services.AddSingleton<AutoScheduleJob>();
            services.AddScoped<OtherJob>();
            services.AddScoped<SubscriptionJob>();


            //Register Trigger
            //naming convention for cron AutoSchedulerConfiguration.<JOB_CLASS_NAME>.Cron
            services.AddSingleton(new UptymJobSchedule(
                jobType: typeof(NotificationJob),
                cronExpression: configuration.GetValue<string>("AutoSchedulerConfiguration:NotificationJob.Cron")
            )); //every 10 seconds

            services.AddSingleton(new UptymJobSchedule(
                jobType: typeof(OtherJob),
                cronExpression: configuration.GetValue<string>("AutoSchedulerConfiguration:OtherJob.Cron")
                )); //every 5 seconds

            services.AddSingleton(new UptymJobSchedule(
                jobType: typeof(AutoScheduleJob),
                cronExpression: configuration.GetValue<string>("AutoSchedulerConfiguration:AutoScheduleJob.Cron")
                )); //every 5 seconds

            services.AddSingleton(new UptymJobSchedule(
                jobType: typeof(SubscriptionJob),
                cronExpression: configuration.GetValue<string>("AutoSchedulerConfiguration:SubscriptionJob.Cron")
                )); //every day at 00:00

            return services;
        }

        public static IApplicationBuilder UseCustomised(this IApplicationBuilder app)
        {
            // For symmetry, you may wish to still put this in an extension,
            // but you could also decide not to.
            //app.UsePolicy();

            return app;
        }
    }
}
