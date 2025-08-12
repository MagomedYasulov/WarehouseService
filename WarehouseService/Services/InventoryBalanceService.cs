using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;
using WarehouseService.Abstractions;
using WarehouseService.Data;
using WarehouseService.Data.Entites;
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
            var isResourceIdNull = filter.ResourceId == null;
            var isUnitIdNull = filter.UnitId == null;

            Expression<Func<InventoryBalance, bool>> predicate = (balance) => (isResourceIdNull || balance.ResourceId == filter.ResourceId) &&
                                                                              (isUnitIdNull || balance.UnitId == filter.UnitId);


            var balances = await _dbContext.InventoryBalances.AsNoTracking().Where(predicate).Include(b => b.Resource).Include(b => b.Unit).ToArrayAsync();
            return _mapper.Map<InventoryBalanceDto[]>(balances);
        }
    }
}
