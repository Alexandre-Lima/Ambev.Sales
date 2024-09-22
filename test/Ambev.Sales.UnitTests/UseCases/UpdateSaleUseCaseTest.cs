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
    public class UpdateSaleUseCaseTest
    {

        private readonly Mock<ISaleRepository> _repository;
        private readonly UpdateSaleUseCase _useCase;
        private static readonly Faker _faker = FakerPtBr.CreateFaker();

        public UpdateSaleUseCaseTest()
        {
            _repository = new Mock<ISaleRepository>();
            _useCase = new UpdateSaleUseCase(
                _repository.Object,
                Mock.Of<ILogger<UpdateSaleUseCase>>()
                );
        }

        [Fact]
        public async Task Execute_ReturnsOk()
        {
            // Arrange
            var saleUpdate = (new SalesRequestBuilder().Build(), _faker.Random.String(20));

            var request = saleUpdate.Item1;
            var saleId = saleUpdate.Item2;

            _repository
              .Setup(repository => repository.UpdateSaleAsync(request, saleId));

            // Act
            var response = await _useCase.Execute(saleUpdate);

            // Assert
            response.Status.Should().Be(UseCaseResponseKind.Success);
            response.Result.Should().BeTrue();
            response.ErrorMessage.Should().BeNull();
        }

        [Fact]
        public async Task Execute_Returns_InternalServerError()
        {
            // Arrange
            var saleUpdate = (new SalesRequestBuilder().Build(), _faker.Random.String(20));

            var request = saleUpdate.Item1;
            var saleId = saleUpdate.Item2;

            _repository
              .Setup(repository => repository.UpdateSaleAsync(request, saleId))
              .Returns(Task.FromException<SalesResponse>(new Exception()));

            // Act
            var response = await _useCase.Execute(saleUpdate);

            // Assert
            response.Status.Should().Be(UseCaseResponseKind.InternalServerError);
            response.Result.Should().BeFalse();
            response.ErrorMessage.Should().Be(MessageConstant.FailedPut);
        }
    }
}