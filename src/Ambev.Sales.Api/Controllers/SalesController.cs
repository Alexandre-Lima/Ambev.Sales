using Ambev.Sales.Api.Models;
using Ambev.Sales.Domain.Entities;
using Ambev.Sales.Domain.UseCases;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace Ambev.Sales.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SalesController : ControllerBase
    {
        private readonly IActionResultConverter _actionResultConverter;
        private readonly ICreateSaleUseCase _createSaleUseCase;
        private readonly IUpdateSaleUseCase _updateSaleUseCase;
        private readonly IDeleteSaleUseCase _deleteSaleUseCase;
        private readonly ICancelSaleItemUseCase _cancelSaleItemUseCase;

        public SalesController(
            IActionResultConverter actionResultConverter,
            ICreateSaleUseCase createSaleUseCase,
            IUpdateSaleUseCase updateSaleUseCase,
            IDeleteSaleUseCase deleteSaleUseCase,
            ICancelSaleItemUseCase cancelSaleItemUseCase)
        {
            _actionResultConverter = actionResultConverter;
            _createSaleUseCase = createSaleUseCase;
            _updateSaleUseCase = updateSaleUseCase;
            _deleteSaleUseCase = deleteSaleUseCase;
            _cancelSaleItemUseCase = cancelSaleItemUseCase;
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created, Type = typeof(SalesResponse))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(string))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(string))]
        public async Task<IActionResult> CreateSales([FromBody] SalesRequest request)
        {
            return _actionResultConverter.Convert(await _createSaleUseCase.Execute(request));
        }

        [HttpPut("{saleId}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(bool))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(string))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(string))]
        public async Task<IActionResult> UpdateSales([FromBody] SalesRequest request, string saleId)
        {
            var saleUpdate = (request, saleId);
            return _actionResultConverter.Convert(await _updateSaleUseCase.Execute(saleUpdate));
        }

        [HttpDelete("cancellation/{saleId}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(bool))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(string))]
        [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(string))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(string))]
        public async Task<IActionResult> CancelSales([Required] string saleId)
        {
            return _actionResultConverter.Convert(await _deleteSaleUseCase.Execute(saleId));
        }

        [HttpPatch("cancellation/{saleId}/item")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(bool))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(string))]
        [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(string))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(string))]
        public async Task<IActionResult> CancelSaleItem([FromBody] SalesItemRequest request, string saleId)
        {
            var itemCancel = (request, saleId);
            return _actionResultConverter.Convert(await _cancelSaleItemUseCase.Execute(itemCancel));
        }
    }
}
