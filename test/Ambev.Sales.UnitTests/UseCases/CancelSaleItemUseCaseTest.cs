using Ambev.Sales.Domain.Constants;
using Ambev.Sales.Domain.Entities;
using Ambev.Sales.Domain.HttpResponse;
using Ambev.Sales.Domain.Repositories;
using Ambev.Sales.Domain.UseCases;
using Ambev.Sales.UnitTests.Builders;
using Ambev.Sales.UnitTests.Utils;
using Bogus;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;

namespace Ambev.Sales.UnitTests.UseCases
{
    public class CancelSaleItemUseCaseTest
    {

        private readonly Mock<ISaleRepository> _repository;
        private readonly CancelSaleItemUseCase _useCase;
        private static readonly Faker _faker = FakerPtBr.CreateFaker();

        public CancelSaleItemUseCaseTest()
        {
            _repository = new Mock<ISaleRepository>();
            _useCase = new CancelSaleItemUseCase(
                _repository.Object,
                Mock.Of<ILogger<CancelSaleItemUseCase>>()
                );
        }

        [Fact]
        public async Task Execute_ReturnsOk()
        {
            // Arrange
            var result = new SalesRequestBuilder().Build();
            var item = new SalesItemRequestBuilder()
                .WithProductsId(result.Items.First().ProductsId)
                .Build();

            var itemRequest = (item, _faker.Random.String(20));

            var saleId = itemRequest.Item2;

            _repository
              .Setup(repository => repository.GetSaleAsync(saleId))
              .ReturnsAsync(result);

            _repository
               .Setup(repository => repository.UpdateSaleAsync(result, saleId));

            // Act
            var response = await _useCase.Execute(itemRequest);

            // Assert
            response.Status.Should().Be(UseCaseResponseKind.Success);
            response.Result.Should().BeTrue();
            response.ErrorMessage.Should().BeNull();
        }

        [Fact]
        public async Task Execute_Returns_NotFound()
        {
            // Arrange
            var result = new SalesRequestBuilder().Build();
            var item = new SalesItemRequestBuilder()
                .WithProductsId(result.Items.First().ProductsId)
                .Build();

            var itemRequest = (item, _faker.Random.String(20));

            // Act
            var response = await _useCase.Execute(itemRequest);

            // Assert
            response.Status.Should().Be(UseCaseResponseKind.NotFound);
            response.Result.Should().BeFalse();
            response.ErrorMessage.Should().Be(MessageConstant.NotFound);
        }

        [Fact]
        public async Task Execute_Returns_Item_NotFound()
        {
            // Arrange
            var result = new SalesRequestBuilder().Build();
            var item = new SalesItemRequestBuilder()
              .Build();

            var itemRequest = (item, _faker.Random.String(20));

            var saleId = itemRequest.Item2;

            _repository
              .Setup(repository => repository.GetSaleAsync(saleId))
              .ReturnsAsync(result);

            // Act
            var response = await _useCase.Execute(itemRequest);

            // Assert
            response.Status.Should().Be(UseCaseResponseKind.NotFound);
            response.Result.Should().BeFalse();
            response.ErrorMessage.Should().Be(MessageConstant.NotFoundItem);
        }

        [Fact]
        public async Task Execute_Returns_InternalServerError()
        {
            // Arrange
            var item = new SalesItemRequestBuilder()
            .Build();

            var itemRequest = (item, _faker.Random.String(20));

            var saleId = itemRequest.Item2;

            _repository
              .Setup(repository => repository.GetSaleAsync(saleId))
              .Returns(Task.FromException<SalesRequest?>(new Exception()));

            // Act
            var response = await _useCase.Execute(itemRequest);

            // Assert
            response.Status.Should().Be(UseCaseResponseKind.InternalServerError);
            response.Result.Should().BeFalse();
            response.ErrorMessage.Should().Be(MessageConstant.FailedDelete);
        }
    }
}