using WarehouseService.Abstractions;
using WarehouseService.Models;
using WarehouseService.ViewModels.Response;

namespace WarehouseService.Services
{
    public class InventoryBalanceService : IInventoryBalanceService
    {
        public Task<InventoryBalanceDto> Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<InventoryBalanceDto[]> Get(InventoryBalanceFilter filter)
        {
            throw new NotImplementedException();
        }
    }
}
