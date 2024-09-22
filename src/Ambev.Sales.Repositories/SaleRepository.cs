using Ambev.Sales.Domain.Entities;
using Ambev.Sales.Domain.HttpResponse.Models;
using Ambev.Sales.Domain.Repositories;
using Newtonsoft.Json;
using System.Net.Mime;
using System.Text;

namespace Ambev.Sales.Repositories
{
    public class SaleRepository : ISaleRepository
    {
        private readonly HttpClient _httpClient;

        public SaleRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<SalesResponse> CreateSaleAsync(SalesRequest request)
        {
            using var content = new StringContent(
                JsonConvert.SerializeObject(request),
                Encoding.UTF8,
                MediaTypeNames.Application.Json);

            using var response = await _httpClient.PostAsync(new Uri("", UriKind.Relative), content);
            var responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                var erro = JsonConvert.DeserializeObject<ErrorMessage>(responseContent)!;
                throw new Exception(erro.Message);
            }

            var storageResponse = JsonConvert.DeserializeObject<SalesStorageResponse>(responseContent)!;

            return new SalesResponse { SalesId = storageResponse.Metadata.Id };
        }

        public async Task UpdateSaleAsync(SalesRequest request, string saleId)
        {
            using var content = new StringContent(
                   JsonConvert.SerializeObject(request),
                   Encoding.UTF8,
                   MediaTypeNames.Application.Json);

            using var response = await _httpClient.PutAsync(new Uri(saleId, UriKind.Relative), content);
            var responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                var erro = JsonConvert.DeserializeObject<ErrorMessage>(responseContent)!;
                throw new Exception(erro.Message);
            }
        }
    }
}
