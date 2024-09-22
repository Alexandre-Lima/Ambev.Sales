using Afya.Wedoo.IntegrationTest.Configurations;
using Ambev.Sales.Domain.Entities;
using Ambev.Sales.Domain.Repositories;
using Ambev.Sales.IntegrationTest.Builders;
using Ambev.Sales.IntegrationTest.Configurations;
using FluentAssertions;
using FluentAssertions.Common;
using Moq;
using System.Text;
using System.Text.Json;

namespace Ambev.Sales.IntegrationTest.Controllers
{
    public class SalesControllerTest : IntegrationTestBase, IClassFixture<CustomWebApplicationFactory<Program>>
    {
        public SalesControllerTest(CustomWebApplicationFactory<Program> factory) : base(factory)
        {
        }

        [Fact]
        public async Task CreateSales_ShouldReturnSuccess()
        {
            // Arrange
            var request = new CreateSalesRequestBuilder().Build();

            var content = new StringContent(
                JsonSerializer.Serialize(request), 
                Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync($"/api/sales", content);

            // Assert
            response.IsSuccessStatusCode.Should().BeTrue();
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);
        }

        [Fact]
        public async Task CreateSales_ShouldReturnBadRequest()
        {
            // Arrange
            var request = new CreateSalesRequestBuilder()
                .WithSaleNumberIsNull()
                .Build();

            var content = new StringContent(
               JsonSerializer.Serialize(request),
               Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync($"/api/sales", content);

            // Assert
            response.IsSuccessStatusCode.Should().BeFalse();
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
        }
    }
}
