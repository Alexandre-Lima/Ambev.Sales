using Ambev.Sales.Shared.Configurations;

namespace Ambev.Sales.Api.Configurations
{
    public static class ConfigurationExtensions
    {
        public static ApplicationConfig LoadConfiguration(this IConfiguration source)
        {
            var applicationConfig = source.Get<ApplicationConfig>()!;
            applicationConfig.SalesStorageHub = source.GetSection("SalesStorageHub").Get<ConfigHub>()!;
            return applicationConfig;
        }
    }
}
