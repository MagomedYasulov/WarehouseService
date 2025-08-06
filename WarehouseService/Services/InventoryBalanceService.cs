using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WarehouseService.Abstractions;
using WarehouseService.Data;
using WarehouseService.Exceptions;
using WarehouseService.Models;
using WarehouseService.ViewModels.Response;

namespace WarehouseService.Services
{
    public class InventoryBalanceService : IInventoryBalanceService
    {
        private readonly IMapper _mapper;
        private readonly ApplicationContext _dbContext;

        public InventoryBalanceService(
            IMapper mapper,
            ApplicationContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public async Task<InventoryBalanceDto> Get(Guid id)
        {
            var balance = await _dbContext.InventoryBalances.Include(b => b.Resource).Include(b => b.Unit).AsNoTracking().FirstOrDefaultAsync(ib => ib.Id == id);
            if (balance == null)
                throw new ServiceException("Inventory Balance Not Found", $"Inventory balance with id {id} not found", StatusCodes.Status404NotFound);

           return _mapper.Map<InventoryBalanceDto>(balance);
        }

        public async Task<InventoryBalanceDto[]> Get(InventoryBalanceFilter filter)
        {
            var balances = await _dbContext.InventoryBalances.AsNoTracking().Include(b => b.Resource).Include(b => b.Unit).ToArrayAsync();
            return _mapper.Map<InventoryBalanceDto[]>(balances);
        }
    }
}
