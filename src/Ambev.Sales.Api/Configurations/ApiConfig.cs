using Ambev.Sales.Api.Models;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Ambev.Sales.Api.Configurations
{
    public static class ApiConfig
    {
        public static void ConfigureApi(this IServiceCollection services)
        {
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IActionResultConverter, ActionResultConverter>();
        }
    }
}
