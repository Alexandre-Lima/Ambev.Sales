using Bogus;

namespace Ambev.Sales.IntegrationTest.Utils
{
    public static class FakerPtBr
    {
        public static Faker CreateFaker()
        {
            return new Faker("pt_BR");
        }
    }
}
