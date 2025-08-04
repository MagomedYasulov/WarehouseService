using WarehouseService.Models;
using WarehouseService.ViewModels.Response;

namespace WarehouseService.Abstractions
{
    public interface IInventoryBalanceService
    {
        public Task<InventoryBalanceDto[]> Get(InventoryBalanceFilter filter);
        public Task<InventoryBalanceDto> Get(Guid id);
    }
}
