using Ambev.Sales.Domain.HttpResponse;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.Sales.Api.Models
{
    public interface IActionResultConverter
    {
        IActionResult Convert<T>(UseCaseResponse<T> response);
    }

    public class ActionResultConverter : IActionResultConverter
    {
        public IActionResult Convert<T>(UseCaseResponse<T> response)
        {
            return new OkObjectResult(response.Result);
        }
    }
}
