using WarehouseService.Models;
using WarehouseService.ViewModels.Request;
using WarehouseService.ViewModels.Response;

namespace WarehouseService.Abstractions
{
    public interface IUnitService
    {
        public Task<UnitDto> Create(CreateUnitDto model);
        public Task<UnitDto> Update(Guid id, UpdateUnitDto model);
        public Task<UnitDto[]> Get(UnitsFilter filter);
        public Task<UnitDto> Get(Guid id);
        public Task Delete(Guid id, bool isSoft);
    }
}
