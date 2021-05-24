using AutoDailyReport.Domain;
using AutoDailyReport.Domain.Extensions;
using GitTools;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace AutoDailyReport
{
    class Program
    {

        private static IServiceProvider serviceProvider;

        static void Main(string[] args)
        {
            Startup();

            var postDataService = serviceProvider.GetService<IPostDataService>();
            var sendService = serviceProvider.GetService<ISendService>();
            var postData = postDataService.GetDailyPostData();
            sendService.Send(postData);

        }

        private static IConfiguration GetConfiguration()
        {
            var envName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var builder = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{envName}.json", optional: true, reloadOnChange: true);
            return builder.Build();
        }

        private static void Startup()
        {
            var services = new ServiceCollection();

            AddServices(services, GetConfiguration());


            serviceProvider = services.BuildServiceProvider();
        }

        private static IServiceCollection AddServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddConfiguration(configuration);
            var gitLib = configuration.GetSection("DailyReport:GitLib")
                .GetChildren().ToDictionary(c => c.Key, c => c.Value);
            services.AddSingleton<IGitTool, GitToolBase>((option) => new GitToolBase(gitLib));
            services.AddSingleton<IPostDataService, PostDataService>();
            services.AddSingleton<ISendService, SendService>();

            return services;
        }
    }
}
