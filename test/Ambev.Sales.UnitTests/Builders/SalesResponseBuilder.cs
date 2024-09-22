using Ambev.Sales.Domain.Entities;
using Ambev.Sales.UnitTests.Utils;
using Bogus;

namespace Ambev.Sales.UnitTests.Builders
{
    public class SalesResponseBuilder
    {
        private readonly SalesResponse _instance;
        private static readonly Faker _faker = FakerPtBr.CreateFaker();

        public SalesResponseBuilder()
        {
            _instance = new SalesResponse
            {
                SalesId = _faker.Random.String(20)
            };
        }

        public SalesResponse Build()
        {
            return _instance;
        }
    }
}
