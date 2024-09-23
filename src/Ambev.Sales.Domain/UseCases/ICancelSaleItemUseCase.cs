using Ambev.Sales.Domain.Entities;
using Ambev.Sales.Domain.Shared;

namespace Ambev.Sales.Domain.UseCases
{
    public interface ICancelSaleItemUseCase : IUseCase<(SalesItemRequest, string), bool>
    {

    }
}
