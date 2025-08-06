using Microsoft.AspNetCore.Mvc;
using WarehouseService.Abstractions;
using WarehouseService.Models;
using WarehouseService.ViewModels.Request;
using WarehouseService.ViewModels.Response;

namespace WarehouseService.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ReceiptDocumentsController : ControllerBase
    {
        private readonly IReceiptDocumentService _receiptDocumentService;

        public ReceiptDocumentsController(IReceiptDocumentService receiptDocumentService)
        {
            _receiptDocumentService = receiptDocumentService;
        }

        [HttpPost]
        public async Task<ActionResult<ReceiptDocumentDto>> Create(CreateReceiptDocumentDto model)
        {
            var receiptDocumentDto = await _receiptDocumentService.Create(model);
            return Ok(receiptDocumentDto);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ReceiptDocumentDto>> Get(Guid id)
        {
            var receiptDocumentDto = await _receiptDocumentService.Get(id);
            return Ok(receiptDocumentDto);
        }

        [HttpGet]
        public async Task<ActionResult<ReceiptDocumentDto[]>> Get(ReceiptDocumentFilter filter)
        {
            var receiptDocumentDto = await _receiptDocumentService.Get(filter);
            return Ok(receiptDocumentDto);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<ReceiptDocumentDto>> Update(Guid id, UpdateReceiptDocumentDto model)
        {
            var receiptDocumentDto = await _receiptDocumentService.Update(id, model);
            return Ok(receiptDocumentDto);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            await _receiptDocumentService.Delete(id);
            return Ok();
        }
    }
}
