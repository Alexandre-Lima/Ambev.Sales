using Ambev.Sales.Domain.UseCases;

namespace Ambev.Sales.Api.Configurations
{
    public static class UseCaseConfig
    {
        public static void ConfigureUseCase(this IServiceCollection services)
        {
            services.AddScoped<ICreateSaleUseCase, CreateSaleUseCase>();
            services.AddScoped<IUpdateSaleUseCase, UpdateSaleUseCase>();
            services.AddScoped<IDeleteSaleUseCase, DeleteSaleUseCase>();
            services.AddScoped<ICancelSaleItemUseCase, CancelSaleItemUseCase>();
        }
    }
}
