using Ambev.Sales.Domain.Entities;
using Ambev.Sales.UnitTests.Utils;
using Bogus;

namespace Ambev.Sales.UnitTests.Builders
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
                ProductsId = Guid.NewGuid(),
                Quantities = _faker.Random.Decimal(),
                Discounts = _faker.Random.Decimal(),
                UnitValues = _faker.Random.Decimal(),
                TotalItem = _faker.Random.Decimal(),
                Cancelled = _faker.Random.Bool(),
            };
        }

        public SalesRequest Build()
        {
            return _instance;
        }
    }
}
