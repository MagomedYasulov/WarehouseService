using AutoMapper;
using System.Linq.Expressions;
using WarehouseService.Abstractions;
using WarehouseService.Data.Entites;
using WarehouseService.Data;
using WarehouseService.Exceptions;
using WarehouseService.Models;
using WarehouseService.ViewModels.Request;
using WarehouseService.ViewModels.Response;
using Microsoft.EntityFrameworkCore;

namespace WarehouseService.Services
{
    public class UnitService : IUnitService
    {
        private readonly IMapper _mapper;
        private readonly ApplicationContext _dbContex;

        public UnitService(
            IMapper mapper,
            ApplicationContext dbContex)
        {
            _mapper = mapper;
            _dbContex = dbContex;
        }

        public async Task<UnitDto> Create(CreateUnitDto model)
        {
            if (await _dbContex.Units.AnyAsync(r => r.Name == model.Name))
                throw new ServiceException("Name conflict", $"Unit with name '{model.Name}' already exist", StatusCodes.Status409Conflict);

            var unit = new Unit() { Name = model.Name };

            await _dbContex.AddAsync(unit);
            await _dbContex.SaveChangesAsync();

            return _mapper.Map<UnitDto>(unit);
        }


        public async Task<UnitDto[]> Get(UnitsFilter filter)
        {
            var isRevokedNull = filter.Revoked == null;

            Expression<Func<Unit, bool>> exp = (r) => isRevokedNull || r.Revoked == filter.Revoked;
            var units = await _dbContex.Units.AsNoTracking().Where(exp).ToArrayAsync();

            return _mapper.Map<UnitDto[]>(units);
        }

        public async Task<UnitDto> Get(Guid id)
        {
            var unit = await _dbContex.Units.AsNoTracking().FirstOrDefaultAsync(r => r.Id == id);
            if (unit == null)
                throw new ServiceException("Unit Not Found", $"Unit with id '{id}' not found", StatusCodes.Status404NotFound);

            return _mapper.Map<UnitDto>(unit);
        }

        public async Task<UnitDto> Update(Guid id, UpdateUnitDto model)
        {
            if (await _dbContex.Units.AnyAsync(r => r.Id != id && r.Name == model.Name))
                throw new ServiceException("Name conflict", $"Unit with name '{model.Name}' already exist", StatusCodes.Status409Conflict);

            var unit = await _dbContex.Units.FirstOrDefaultAsync(r => r.Id == id);
            if (unit == null)
                throw new ServiceException("Unit Not Found", $"Unit with id '{id}' not found", StatusCodes.Status404NotFound);

            unit.Name = model.Name;
            await _dbContex.SaveChangesAsync();

            return _mapper.Map<UnitDto>(unit);
        }


        public async Task Delete(Guid id, bool isSoft)
        {
            var unit = await _dbContex.Units.FirstOrDefaultAsync(r => r.Id == id);
            if (unit == null)
                throw new ServiceException("Unit Not Found", $"Unit with id '{id}' not found", StatusCodes.Status404NotFound);

            if (isSoft)
            {
                unit.Revoked = true;
                await _dbContex.SaveChangesAsync();
                return;
            }

            if (await _dbContex.ReceiptResourses.AnyAsync(r => r.UnitId == id))
                throw new ServiceException("Can`t Delete Unit", $"Can`t delete unit with id {id}, unit in use", StatusCodes.Status404NotFound);

            _dbContex.Units.Remove(unit);
            await _dbContex.SaveChangesAsync();
        }
    }
}
