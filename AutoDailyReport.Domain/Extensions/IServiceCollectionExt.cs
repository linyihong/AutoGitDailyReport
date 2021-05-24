using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AutoDailyReport.Domain.Extensions
{
    public static class IServiceCollectionExt
    {
        public static void AddConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(configuration);
        }
    }
}
