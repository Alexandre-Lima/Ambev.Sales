using Ambev.Sales.Domain.Constants;
using Ambev.Sales.Domain.Entities;
using Ambev.Sales.Domain.HttpResponse;
using Ambev.Sales.Domain.Repositories;
using Ambev.Sales.Domain.Shared;
using Microsoft.Extensions.Logging;

namespace Ambev.Sales.Domain.UseCases
{
    public class CreateSaleUseCase : UseCase<SalesRequest, SalesResponse>, ICreateSaleUseCase
    {
        private readonly ISaleRepository _repository;

        public CreateSaleUseCase(
               ISaleRepository repository,
               ILogger<CreateSaleUseCase> logger
              ) : base(logger, MessageConstant.FailedPost)
        {
            _repository = repository;
        }

        protected override async Task<UseCaseResponse<SalesResponse>> OnExecute(SalesRequest request)
        {
            var result = await _repository.CreateSaleAsync(request);
            return Created(result);
        }
    }
}
