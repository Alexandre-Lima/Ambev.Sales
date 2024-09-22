using Ambev.Sales.Domain.Repositories;
using Ambev.Sales.IntegrationTest.Mocks;
using Ambev.Sales.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;

namespace Afya.Wedoo.IntegrationTest.Configurations
{
    public class CustomWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
    {
        public required Mock<ISaleRepository> MockSaleRepository;
        public required Mock<IServiceCollection> MockService;

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(services =>
            {
                MockSaleRepository = SaleRepositoryMock.Get().SetupGetContextAsync();
                services.RemoveAll(typeof(SaleRepository));
                services.AddScoped(x => MockSaleRepository.Object);
            });

        }
    }
}