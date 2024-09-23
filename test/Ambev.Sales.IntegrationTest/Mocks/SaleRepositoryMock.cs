using Ambev.Sales.Domain.Entities;
using Ambev.Sales.Domain.Repositories;
using Ambev.Sales.IntegrationTest.Builders;
using Moq;

namespace Ambev.Sales.IntegrationTest.Mocks
{
    public static class SaleRepositoryMock
    {
        public static Mock<ISaleRepository> Get() => new Mock<ISaleRepository>();

        public static Mock<ISaleRepository> SetupGetContextAsync(this Mock<ISaleRepository> mock)
        {
            var response = new SalesResponseBuilder().Build();
            var request = new SalesRequestBuilder().Build();

            mock.Setup(x => x.CreateSaleAsync(It.IsAny<SalesRequest>()))
                .ReturnsAsync(response);

            mock.Setup(x => x.UpdateSaleAsync(It.IsAny<SalesRequest>(), It.IsAny<string>()));

            mock.Setup(x => x.GetSaleAsync(It.IsAny<string>()))
                .ReturnsAsync(request);

            mock.Setup(x => x.DeleteSaleAsync(It.IsAny<string>()));

            return mock;
        }
    }
}
