using WarehouseService.Data.Entites;
using WarehouseService.Models;
using WarehouseService.ViewModels.Request;
using WarehouseService.ViewModels.Response;

namespace WarehouseService.Abstractions
{
    public interface IResourceService
    {
        public Task<ResourceDto> Create(CreateResourceDto model);
        public Task<ResourceDto> Update(Guid id, UpdateResourceDto model);
        public Task<ResourceDto[]> Get(ResourceFilter filter);
        public Task<ResourceDto> Get(Guid id);
        public Task Delete(Guid id, bool isSoft);
    }
}
