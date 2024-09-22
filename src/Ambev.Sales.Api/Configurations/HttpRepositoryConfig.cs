using Ambev.Sales.Domain.Repositories;
using Ambev.Sales.Repositories;
using Ambev.Sales.Shared.Configurations;
using System.Net.Mime;

namespace Ambev.Sales.Api.Configurations
{
    public static class HttpRepositoryConfig
    {
        public static void ConfigureHttpRepository(this IServiceCollection services,
                 ApplicationConfig applicationConfig)
        {
            services.AddHttpClient<ISaleRepository, SaleRepository>(client =>
            {
                client.BaseAddress = new Uri(applicationConfig.SalesStorageHub.Url);
                client.DefaultRequestHeaders.Add("X-Master-Key", applicationConfig.SalesStorageHub.MasterKey);
                client.DefaultRequestHeaders.Add("X-Access-Key", applicationConfig.SalesStorageHub.AccessKey);
                client.DefaultRequestHeaders.Add("Accept", MediaTypeNames.Application.Json);
            });
        }
    }
}
