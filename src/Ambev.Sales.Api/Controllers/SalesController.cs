using Ambev.Sales.Api.Models;
using Ambev.Sales.Domain.Entities;
using Ambev.Sales.Domain.UseCases;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Ambev.Sales.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SalesController : ControllerBase
    {
        private readonly IActionResultConverter _actionResultConverter;
        private readonly ICreateSaleUseCase _createSaleUseCase;

        public SalesController(
            IActionResultConverter actionResultConverter,
            ICreateSaleUseCase createSaleUseCase)
        {
            _actionResultConverter = actionResultConverter;
            _createSaleUseCase = createSaleUseCase;
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created, Type = typeof(SalesResponse))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(string))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(string))]
        public async Task<IActionResult> CreateSales([FromBody] CreateSalesRequest request)
        {
            return _actionResultConverter.Convert(await _createSaleUseCase.Execute(request));
        }
    }
}
