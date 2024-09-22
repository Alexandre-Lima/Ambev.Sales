using Bogus;

namespace Ambev.Sales.UnitTests.Utils
{
    public static class FakerPtBr
    {
        public static Faker CreateFaker()
        {
            return new Faker("pt_BR");
        }
    }
}
