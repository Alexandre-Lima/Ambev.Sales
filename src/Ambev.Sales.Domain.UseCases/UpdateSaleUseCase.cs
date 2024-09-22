using Ambev.Sales.Domain.Constants;
using Ambev.Sales.Domain.Entities;
using Ambev.Sales.Domain.HttpResponse;
using Ambev.Sales.Domain.Repositories;
using Ambev.Sales.Domain.Shared;
using Microsoft.Extensions.Logging;

namespace Ambev.Sales.Domain.UseCases
{
    public class UpdateSaleUseCase : UseCase<(SalesRequest, string), bool>, IUpdateSaleUseCase
    {
        private readonly ISaleRepository _repository;

        public UpdateSaleUseCase(
               ISaleRepository repository,
               ILogger<UpdateSaleUseCase> logger
              ) : base(logger, MessageConstant.FailedPut)
        {
            _repository = repository;
        }

        protected override async Task<UseCaseResponse<bool>> OnExecute((SalesRequest, string) saleUpdate)
        {
            var request = saleUpdate.Item1;
            var saleId = saleUpdate.Item2;

            await _repository.UpdateSaleAsync(request, saleId);

            return Result(true);
        }
    }
}
