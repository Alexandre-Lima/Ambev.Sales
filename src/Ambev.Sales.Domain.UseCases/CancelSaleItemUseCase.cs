using Ambev.Sales.Domain.Constants;
using Ambev.Sales.Domain.Entities;
using Ambev.Sales.Domain.HttpResponse;
using Ambev.Sales.Domain.Repositories;
using Ambev.Sales.Domain.Shared;
using Microsoft.Extensions.Logging;

namespace Ambev.Sales.Domain.UseCases
{
    public class CancelSaleItemUseCase : UseCase<(SalesItemRequest, string), bool>, ICancelSaleItemUseCase
    {
        private readonly ISaleRepository _repository;

        public CancelSaleItemUseCase(
               ISaleRepository repository,
               ILogger<CancelSaleItemUseCase> logger
              ) : base(logger, MessageConstant.FailedDelete)
        {
            _repository = repository;
        }

        protected override async Task<UseCaseResponse<bool>> OnExecute((SalesItemRequest, string) request)
        {
            var itemSale = request.Item1;
            var saleId = request.Item2;

            var response = await _repository.GetSaleAsync(saleId);

            if (response == null)
                return NotFound(MessageConstant.NotFound);

            var item = response.Items.FirstOrDefault(a => a.ProductsId == itemSale.ProductsId);
            if (item == null)
                return NotFound(MessageConstant.NotFoundItem);

            item.Cancelled = itemSale.Cancelled;
            await _repository.UpdateSaleAsync(response, saleId);

            return Result(true);
        }
    }
}
