using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Serilog;
using Topshelf;
using Topshelf.Runtime;
using Uptym.AutoScheduler.ScheduleTriggers;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Uptym.Services.Maintenance.ScheduleTriggers;

namespace Uptym
{
    public class Program
    {
        //  public static void Main(string[] args) => CreateWebHostBuilder(args).Build().Run();
 
        private static void Main(string[] args)
        {
  
                                CreateWebHostBuilder(args).Build().Run();

            //Code For Auto Run AutoSchedule Module
            //var hostBuilder = new HostBuilder().ConfigureAppConfiguration((hostContext, config) =>
            //      {
            //      })
            //     .ConfigureServices((hostContext, services) =>
            //     {
            //         CreateWebHostBuilder(args).Build().Run();
            //         services.AddTransient<IScheduleTriggerService>();
            //      });

            //    var host = hostBuilder.Build();

            //// Begin Auto Scheduler configuration
            //var exitCode = HostFactory.Run(x =>
            //{
            //    x.Service<Scheduler>(s =>
            //    {
            //        s.ConstructUsing(beat => new Scheduler());
            //        s.WhenStarted(beat => beat.Start());
            //        s.WhenStopped(beat => beat.Stop());
            //    });
            //    x.RunAsLocalSystem();
            //    x.SetServiceName("UptimeAutoScheduler");
            //    x.SetDisplayName("Uptime Auto Scheduler");
            //    x.SetDescription("Uptime Auto Scheduler");

            //});
            //int exitCodeValue = (int)Convert.ChangeType(exitCode, exitCode.GetTypeCode());
            //Environment.ExitCode = exitCodeValue;
         }

         



        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseSerilog();

    


        }
}
