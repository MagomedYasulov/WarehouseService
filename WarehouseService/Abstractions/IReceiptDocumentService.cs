using WarehouseService.Models;
using WarehouseService.ViewModels.Request;
using WarehouseService.ViewModels.Response;

namespace WarehouseService.Abstractions
{
    public interface IReceiptDocumentService
    {
        public Task<ResourceDto> Create(CreateReceiptDocumentDto model);
        public Task<ResourceDto> Update(Guid id, UpdateReceiptDocumentDto model);
        public Task<ResourceDto[]> Get(ReceiptDocumentFilter filter);
        public Task<ResourceDto> Get(Guid id);
        public Task Delete(Guid id);
    }
}
