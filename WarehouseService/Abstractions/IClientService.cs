using WarehouseService.Models;
using WarehouseService.ViewModels.Request;
using WarehouseService.ViewModels.Response;

namespace WarehouseService.Abstractions
{
    public interface IClientService
    {
        public Task<ClientDto> Create(CreateClientDto model);
        public Task<ClientDto> Update(Guid id, UpdateClientDto model);
        public Task<ClientDto[]> Get(ClientsFilter filter);
        public Task<ClientDto> Get(Guid id);
        public Task Delete(Guid id, bool isSoft);
    }
}
