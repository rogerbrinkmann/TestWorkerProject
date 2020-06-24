using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


namespace TestWorkerProject
{
    public class Program
    {
        // run task.json to publish publish
        public static void Main(string[] args)
        {

            CreateHostBuilder(args).Build().Run();
            return;
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .UseWindowsService()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<Worker>();
                });
        }
    }
}
