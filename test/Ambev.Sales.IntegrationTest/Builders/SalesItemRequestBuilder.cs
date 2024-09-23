using Ambev.Sales.Domain.Entities;
using Ambev.Sales.IntegrationTest.Utils;
using Bogus;

namespace Ambev.Sales.IntegrationTest.Builders
{
    public class SalesItemRequestBuilder
    {
        private readonly SalesItemRequest _instance;
        private static readonly Faker _faker = FakerPtBr.CreateFaker();

        public SalesItemRequestBuilder()
        {
            _instance = new SalesItemRequest
            {
                ProductsId = Guid.NewGuid(),
                Cancelled = _faker.Random.Bool(),
            };
        }

        public SalesItemRequestBuilder WithProductsId(Guid productId)
        {
            _instance.ProductsId = productId;
            return this;
        }

        public SalesItemRequest Build()
        {
            return _instance;
        }
    }
}
