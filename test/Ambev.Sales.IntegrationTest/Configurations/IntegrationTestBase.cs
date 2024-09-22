using Afya.Wedoo.IntegrationTest.Configurations;

namespace Ambev.Sales.IntegrationTest.Configurations
{
    public class IntegrationTestBase
    {
        protected CustomWebApplicationFactory<Program> _factory;
        public readonly HttpClient _client;

        public IntegrationTestBase(CustomWebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
            _factory = factory;
        }
    }
}
