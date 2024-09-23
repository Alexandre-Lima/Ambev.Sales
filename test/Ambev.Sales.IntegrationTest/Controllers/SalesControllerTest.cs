using Afya.Wedoo.IntegrationTest.Configurations;
using Ambev.Sales.Domain.Entities;
using Ambev.Sales.IntegrationTest.Builders;
using Ambev.Sales.IntegrationTest.Configurations;
using Ambev.Sales.IntegrationTest.Utils;
using Bogus;
using FluentAssertions;
using System.Text;
using System.Text.Json;

namespace Ambev.Sales.IntegrationTest.Controllers
{
    public class SalesControllerTest : IntegrationTestBase, IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private static readonly Faker _faker = FakerPtBr.CreateFaker();

        public SalesControllerTest(CustomWebApplicationFactory<Program> factory) : base(factory)
        {
        }

        [Fact]
        public async Task CreateSales_ShouldReturnSuccess()
        {
            // Arrange
            var request = new SalesRequestBuilder().Build();

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
            var request = new SalesRequestBuilder()
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

        [Fact]
        public async Task UpdateSales_ShouldReturnSuccess()
        {
            // Arrange
            var request = new SalesRequestBuilder().Build();
            var saleId = _faker.Random.String(20);

            var content = new StringContent(
                JsonSerializer.Serialize(request),
                Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PutAsync($"/api/sales/{saleId}", content);

            // Assert
            response.IsSuccessStatusCode.Should().BeTrue();
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }

        [Fact]
        public async Task UpdateSales_ShouldReturnBadRequest()
        {
            // Arrange
            var request = new SalesRequestBuilder()
                .WithSaleNumberIsNull()
                .Build();
            var saleId = _faker.Random.String(20);

            var content = new StringContent(
               JsonSerializer.Serialize(request),
               Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PutAsync($"/api/sales/{saleId}", content);

            // Assert
            response.IsSuccessStatusCode.Should().BeFalse();
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task CancelSales_ShouldReturnSuccess()
        {
            // Arrange
            var saleId = "66efa911acd3cb34a888f8a4";

            // Act
            var response = await _client.DeleteAsync($"/api/sales/cancellation/{saleId}");

            // Assert
            response.IsSuccessStatusCode.Should().BeTrue();
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }

        [Fact]
        public async Task CancelSales_ShouldReturnNotFound()
        {
            // Arrange
            var saleId = _faker.Random.String2(20);

            // Act
            var response = await _client.DeleteAsync($"/api/sales/cancellation/{saleId}");

            // Assert
            response.IsSuccessStatusCode.Should().BeFalse();
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task CancelSales_ShouldReturnMethodNotAllowed()
        {
            // Arrange
            var saleId = string.Empty;

            // Act
            var response = await _client.DeleteAsync($"/api/sales/cancellation/{saleId}");

            // Assert
            response.IsSuccessStatusCode.Should().BeFalse();
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.MethodNotAllowed);
        }

        [Fact]
        public async Task CancelSalesItem_ShouldReturnSuccess()
        {
            // Arrange
            var item = new SalesItemRequestBuilder()
             .WithProductsId(Guid.Parse("90b753f4-09ec-4470-bf1c-f6269aadee9f"))
             .Build();

            var content = new StringContent(
                JsonSerializer.Serialize(item),
                Encoding.UTF8, "application/json");

            var saleId = "66efa911acd3cb34a888f8a4";

            // Act
            var response = await _client.PatchAsync($"/api/sales/cancellation/{saleId}/item/", content);

            // Assert
            response.IsSuccessStatusCode.Should().BeTrue();
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }

        [Fact]
        public async Task CancelSalesItem_ShouldReturnBadRequest()
        {
            // Arrange
            SalesItemRequest? item = null;

            var content = new StringContent(
                JsonSerializer.Serialize(item),
                Encoding.UTF8, "application/json");

            var saleId = "66efa911acd3cb34a888f8a4";

            // Act
            var response = await _client.PatchAsync($"/api/sales/cancellation/{saleId}/item/", content);

            // Assert
            response.IsSuccessStatusCode.Should().BeFalse();
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task CancelSalesItem_ShouldReturnNotFound()
        {
            // Arrange
            var item = new SalesItemRequestBuilder()
                .Build();

            var content = new StringContent(
                JsonSerializer.Serialize(item),
                Encoding.UTF8, "application/json");

            var saleId = _faker.Random.String2(20);

            // Act
            var response = await _client.PatchAsync($"/api/sales/cancellation/{saleId}/item/", content);

            // Assert
            response.IsSuccessStatusCode.Should().BeFalse();
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task CancelSalesItem_ShouldReturnItemNotFound()
        {
            // Arrange
            var item = new SalesItemRequestBuilder()
                .Build();

            var content = new StringContent(
                JsonSerializer.Serialize(item),
                Encoding.UTF8, "application/json");

            var saleId = "66efa911acd3cb34a888f8a4";

            // Act
            var response = await _client.PatchAsync($"/api/sales/cancellation/{saleId}/item/", content);

            // Assert
            response.IsSuccessStatusCode.Should().BeFalse();
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
        }
    }
}
