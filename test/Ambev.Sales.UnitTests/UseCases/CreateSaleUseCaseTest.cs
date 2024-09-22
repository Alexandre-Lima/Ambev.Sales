using Ambev.Sales.Domain.Constants;
using Ambev.Sales.Domain.Entities;
using Ambev.Sales.Domain.HttpResponse;
using Ambev.Sales.Domain.Repositories;
using Ambev.Sales.Domain.UseCases;
using Ambev.Sales.UnitTests.Builders;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;

namespace Ambev.Sales.UnitTests.UseCases
{
    public class CreateSaleUseCaseTest
    {

        private readonly Mock<ISaleRepository> _repository;
        private readonly CreateSaleUseCase _useCase;

        public CreateSaleUseCaseTest()
        {
            _repository = new Mock<ISaleRepository>();
            _useCase = new CreateSaleUseCase(
                _repository.Object,
                Mock.Of<ILogger<CreateSaleUseCase>>()
                );
        }

        [Fact]
        public async Task Execute_ReturnsOk()
        {
            // Arrange
            var request = new SalesRequestBuilder().Build();
            var result = new SalesResponseBuilder().Build();

            _repository
              .Setup(repository => repository.CreateSaleAsync(request))
              .ReturnsAsync(result);

            // Act
            var response = await _useCase.Execute(request);

            // Assert
            response.Status.Should().Be(UseCaseResponseKind.Created);
            response.Result.Should().BeEquivalentTo(result);
            response.ErrorMessage.Should().BeNull();
        }

        [Fact]
        public async Task Execute_Returns_InternalServerError()
        {
            // Arrange
            var request = new SalesRequestBuilder().Build();

            _repository
              .Setup(repository => repository.CreateSaleAsync(request))
              .Returns(Task.FromException<SalesResponse>(new Exception()));

            // Act
            var response = await _useCase.Execute(request);

            // Assert
            response.Status.Should().Be(UseCaseResponseKind.InternalServerError);
            response.Result.Should().BeNull();
            response.ErrorMessage.Should().Be(MessageConstant.FailedPost);
        }
    }
}