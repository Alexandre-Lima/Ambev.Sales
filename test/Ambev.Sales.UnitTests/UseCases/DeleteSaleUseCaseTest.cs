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
    public class DeleteSaleUseCaseTest
    {

        private readonly Mock<ISaleRepository> _repository;
        private readonly DeleteSaleUseCase _useCase;
        private static readonly Faker _faker = FakerPtBr.CreateFaker();

        public DeleteSaleUseCaseTest()
        {
            _repository = new Mock<ISaleRepository>();
            _useCase = new DeleteSaleUseCase(
                _repository.Object,
                Mock.Of<ILogger<DeleteSaleUseCase>>()
                );
        }

        [Fact]
        public async Task Execute_ReturnsOk()
        {
            // Arrange
            var result = new SalesRequestBuilder().Build();
            var saleId = _faker.Random.String(20);

            _repository
              .Setup(repository => repository.GetSaleAsync(saleId))
              .ReturnsAsync(result);

            _repository
              .Setup(repository => repository.DeleteSaleAsync(saleId));

            // Act
            var response = await _useCase.Execute(saleId);

            // Assert
            response.Status.Should().Be(UseCaseResponseKind.Success);
            response.Result.Should().BeTrue();
            response.ErrorMessage.Should().BeNull();
        }

        [Fact]
        public async Task Execute_Returns_NotFound()
        {
            // Arrange
            var saleId = _faker.Random.String(20);

            _repository
             .Setup(repository => repository.GetSaleAsync(saleId));

            // Act
            var response = await _useCase.Execute(saleId);

            // Assert
            response.Status.Should().Be(UseCaseResponseKind.NotFound);
            response.Result.Should().BeFalse();
            response.ErrorMessage.Should().Be(MessageConstant.NotFound);
        }

        [Fact]
        public async Task Execute_Returns_InternalServerError()
        {
            // Arrange
            var result = new SalesRequestBuilder().Build();
            var saleId = _faker.Random.String(20);

            _repository
              .Setup(repository => repository.GetSaleAsync(saleId))
              .ReturnsAsync(result);

            _repository
              .Setup(repository => repository.DeleteSaleAsync(saleId))
              .Returns(Task.FromException<SalesResponse>(new Exception()));

            // Act
            var response = await _useCase.Execute(saleId);

            // Assert
            response.Status.Should().Be(UseCaseResponseKind.InternalServerError);
            response.Result.Should().BeFalse();
            response.ErrorMessage.Should().Be(MessageConstant.FailedDelete);
        }
    }
}