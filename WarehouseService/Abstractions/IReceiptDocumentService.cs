using WarehouseService.Models;
using WarehouseService.ViewModels.Request;
using WarehouseService.ViewModels.Response;

namespace WarehouseService.Abstractions
{
    public interface IReceiptDocumentService
    {
        public Task<ReceiptDocumentDto> Create(CreateReceiptDocumentDto model);
        public Task<ReceiptDocumentDto> Update(Guid id, UpdateReceiptDocumentDto model);
        public Task<ReceiptDocumentDto[]> Get(ReceiptDocumentFilter filter);
        public Task<ReceiptDocumentDto> Get(Guid id);
        public Task Delete(Guid id);
    }
}
