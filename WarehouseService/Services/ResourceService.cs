using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WarehouseService.Abstractions;
using WarehouseService.Data;
using WarehouseService.Data.Entites;
using WarehouseService.Exceptions;
using WarehouseService.Models;
using WarehouseService.ViewModels.Request;
using WarehouseService.ViewModels.Response;

namespace WarehouseService.Services
{
    public class ResourceService : IResourceService
    {
        private readonly IMapper _mapper;
        private readonly ApplicationContext _dbContex;

        public ResourceService(
            IMapper mapper,
            ApplicationContext dbContex)
        {
            _mapper = mapper;
            _dbContex = dbContex;
        }

        public async Task<ResourceDto> Create(CreateResourceDto model)
        {
            if (await _dbContex.Resources.AnyAsync(r => r.Name == model.Name))
                throw new ServiceException("Name conflict", $"Resource with name '{model.Name}' already exist", StatusCodes.Status409Conflict);

            var resource = new Resource() { Name = model.Name };

            await _dbContex.AddAsync(resource);
            await _dbContex.SaveChangesAsync();

            return _mapper.Map<ResourceDto>(resource);
        }

  
        public async Task<ResourceDto[]> Get(ResourceFilter filter)
        {
            var isRevokedNull = filter.Revoked == null;

            Expression<Func<Resource, bool>> exp = (r) => isRevokedNull || r.Revoked == filter.Revoked;
            var resources = await _dbContex.Resources.AsNoTracking().Where(exp).ToArrayAsync();

            return _mapper.Map<ResourceDto[]>(resources);
        }

        public async Task<ResourceDto> Get(Guid id)
        {
            var resource = await _dbContex.Resources.AsNoTracking().FirstOrDefaultAsync(r => r.Id == id);
            if(resource == null)
                throw new ServiceException("Resource Not Found", $"Resource with id '{id}' not found", StatusCodes.Status404NotFound);

            return _mapper.Map<ResourceDto>(resource);
        }

        public async Task<ResourceDto> Update(Guid id, UpdateResourceDto model)
        {
            if (await _dbContex.Resources.AnyAsync(r => r.Id != id && r.Name == model.Name))
                throw new ServiceException("Name conflict", $"Resource with name '{model.Name}' already exist", StatusCodes.Status409Conflict);

            var resource = await _dbContex.Resources.FirstOrDefaultAsync(r => r.Id == id);
            if (resource == null)
                throw new ServiceException("Resource Not Found", $"Resource with id '{id}' not found", StatusCodes.Status404NotFound);

            resource.Name = model.Name;
            await _dbContex.SaveChangesAsync();

            return _mapper.Map<ResourceDto>(resource);
        }


        public async Task Delete(Guid id, bool isSoft)
        {
            var resource = await _dbContex.Resources.FirstOrDefaultAsync(r => r.Id == id);
            if (resource == null)
                throw new ServiceException("Resource Not Found", $"Resource with id '{id}' not found", StatusCodes.Status404NotFound);

            if (isSoft)
            {
                resource.Revoked = true;
                await _dbContex.SaveChangesAsync();
                return;
            }

            if (await _dbContex.ReceiptResourses.AnyAsync(r => r.ResourceId == id))
                throw new ServiceException("Can`t Delete Resource", $"Can`t delete resource with id {id}, resource in use", StatusCodes.Status404NotFound);

            _dbContex.Resources.Remove(resource);
            await _dbContex.SaveChangesAsync();
        }
    }
}
