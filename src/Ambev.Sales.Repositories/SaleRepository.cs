using Ambev.Sales.Domain.Entities;
using Ambev.Sales.Domain.Repositories;

namespace Ambev.Sales.Repositories
{
    public class SaleRepository : ApiRepository, ISaleRepository
    {
        public SaleRepository(HttpClient httpClient)
        : base(httpClient)
        {
        }

        public async Task<SalesRequest?> GetSaleAsync(string saleId)
        {
            var url = $"{saleId}?meta=false";
            return await GetAsync<SalesRequest>(url);
        }

        public async Task<SalesResponse> CreateSaleAsync(SalesRequest request)
        {
            var storageResponse = await PostAsync<SalesStorageResponse>($"", request);
            return new SalesResponse { SalesId = storageResponse.Metadata.Id };
        }

        public async Task UpdateSaleAsync(SalesRequest request, string saleId)
        {
            await PutAsync($"{saleId}", request);
        }

        public async Task DeleteSaleAsync(string saleId)
        {
            await DeleteAsync($"{saleId}");
        }
    }
}
