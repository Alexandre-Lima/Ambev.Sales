using Ambev.Sales.Domain.Entities;
using Ambev.Sales.IntegrationTest.Utils;
using Bogus;

namespace Ambev.Sales.IntegrationTest.Builders
{
    public class SalesRequestBuilder
    {
        private readonly SalesRequest _instance;
        private static readonly Faker _faker = FakerPtBr.CreateFaker();

        public SalesRequestBuilder()
        {
            _instance = new SalesRequest
            {
                BranchId = Guid.NewGuid(),
                CustomerId = Guid.NewGuid(),
                SaleNumber = _faker.Random.String(20),
                SaleDate = DateTime.Now,
                TotalSale = _faker.Random.Decimal(),
                Items = new List<SaleItem>
                {
                    NewItem()
                }
            };
        }

        private static SaleItem NewItem()
        {
            return new SaleItem
            {
                ProductsId = Guid.Parse("90b753f4-09ec-4470-bf1c-f6269aadee9f"),
                Quantities = _faker.Random.Decimal(),
                Discounts = _faker.Random.Decimal(),
                UnitValues = _faker.Random.Decimal(),
                TotalItem = _faker.Random.Decimal(),
                Cancelled = _faker.Random.Bool(),
            };
        }

        public SalesRequestBuilder WithSaleNumberIsNull()
        {
            _instance.SaleNumber = null;
            return this;
        }

        public SalesRequest Build()
        {
            return _instance;
        }
    }
}
