using Ambev.Sales.Domain.HttpResponse.Models;
using Newtonsoft.Json;
using System.Net.Mime;
using System.Text;

namespace Ambev.Sales.Repositories
{
    public abstract class ApiRepository
    {
        protected readonly HttpClient _httpClient;

        public ApiRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        protected async Task<T> GetAsync<T>(string url)
        {
            var uri = new Uri(url, UriKind.Relative);
            using var response = await _httpClient.GetAsync(uri);
            return await HandleRespose<T>(response);
        }

        protected async Task<T> PostAsync<T>(string url, object request)
        {
            var uri = new Uri(url, UriKind.Relative);
            using var content = GetContent(request);
            using var response = await _httpClient.PostAsync(uri, content);
            return await HandleRespose<T>(response);
        }

        protected async Task PutAsync(string url, object request)
        {
            var uri = new Uri(url, UriKind.Relative);
            using var content = GetContent(request);
            using var response = await _httpClient.PutAsync(uri, content);
            await HandleRespose(response);
        }

        protected async Task DeleteAsync(string url)
        {
            var uri = new Uri(url, UriKind.Relative);
            using var response = await _httpClient.DeleteAsync(uri);
            await HandleRespose(response);
        }

        protected async Task<T> HandleRespose<T>(HttpResponseMessage response)
        {
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return default;
            }
            var responseContent = await HandleRespose(response);
            return JsonConvert.DeserializeObject<T>(responseContent)!;
        }

        private async static Task<string> HandleRespose(HttpResponseMessage response)
        {
            var responseContent = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                var erro = JsonConvert.DeserializeObject<ErrorMessage>(responseContent)!;
                throw new Exception(erro.Message);
            }
            return responseContent;
        }

        protected static StringContent GetContent(object request)
        {
            var json = JsonConvert.SerializeObject(request);
            return new StringContent(json, Encoding.UTF8, MediaTypeNames.Application.Json);
        }
    }
}
