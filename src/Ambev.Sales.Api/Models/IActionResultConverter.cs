using Ambev.Sales.Domain.HttpResponse;
using Ambev.Sales.Domain.HttpResponse.Models;
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
            if (response.Status == UseCaseResponseKind.Created)
                return new CreatedResult("", response.Result);
            
            if (response.Status == UseCaseResponseKind.Success)
                return new OkObjectResult(response.Result);
            
            var errorMessage = new ErrorMessage
            {
                Message = response.ErrorMessage!
            };

            return new ObjectResult(errorMessage)
            {
                StatusCode = (int)response.Status
            };

        }
    }
}
