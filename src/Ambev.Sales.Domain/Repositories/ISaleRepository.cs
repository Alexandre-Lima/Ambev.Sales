using Ambev.Sales.Domain.Entities;

namespace Ambev.Sales.Domain.Repositories
{
    public interface ISaleRepository
    {
        Task<SalesResponse> CreateSaleAsync(SalesRequest request);

        Task UpdateSaleAsync(SalesRequest request, string saleId);
    }
}
