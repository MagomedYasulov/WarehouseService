using WarehouseService.Abstractions;
using WarehouseService.Models;
using WarehouseService.ViewModels.Request;
using WarehouseService.ViewModels.Response;

namespace WarehouseService.Services
{
    public class ReceiptDocumentService : IReceiptDocumentService
    {
        public Task<ResourceDto> Create(CreateReceiptDocumentDto model)
        {
            throw new NotImplementedException();
        }

        public Task Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<ResourceDto[]> Get(ReceiptDocumentFilter filter)
        {
            throw new NotImplementedException();
        }

        public Task<ResourceDto> Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<ResourceDto> Update(Guid id, UpdateReceiptDocumentDto model)
        {
            throw new NotImplementedException();
        }
    }
}
