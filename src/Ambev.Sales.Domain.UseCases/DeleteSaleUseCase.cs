using Ambev.Sales.Domain.Constants;
using Ambev.Sales.Domain.HttpResponse;
using Ambev.Sales.Domain.Repositories;
using Ambev.Sales.Domain.Shared;
using Microsoft.Extensions.Logging;

namespace Ambev.Sales.Domain.UseCases
{
    public class DeleteSaleUseCase : UseCase<string, bool>, IDeleteSaleUseCase
    {
        private readonly ISaleRepository _repository;

        public DeleteSaleUseCase(
               ISaleRepository repository,
               ILogger<DeleteSaleUseCase> logger
              ) : base(logger, MessageConstant.FailedDelete)
        {
            _repository = repository;
        }

        protected override async Task<UseCaseResponse<bool>> OnExecute(string saleId)
        {
            var response = await _repository.GetSaleAsync(saleId);

            if (response == null)
                return NotFound(MessageConstant.NotFound);

            await _repository.DeleteSaleAsync(saleId);

            return Result(true);
        }
    }
}
