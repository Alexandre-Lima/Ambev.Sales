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

            mock.Setup(x => x.CreateSaleAsync(It.IsAny<CreateSalesRequest>()))
                .ReturnsAsync(response);

            return mock;
        }
    }
}
