using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameLibrary.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace GameLibrary
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //var host = CreateHostBuilder(args).Build();
            //RunSeeding(host);
            CreateHostBuilder(args).Build().Run();
        }

        private static void RunSeeding(IHost host)
        {
            var seeder = host.Services.GetService<GameSeeder>();
            seeder.SeedAsync().Wait();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(SetupConfiguration)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        private static void SetupConfiguration(HostBuilderContext ctx, IConfigurationBuilder builder)
        {
            //remove default configuration
            builder.Sources.Clear();
            builder.AddJsonFile("config.json", false, true)
                   //.AddXmlFile("config.xml", true)
                   .AddEnvironmentVariables();
        }
    }
}
