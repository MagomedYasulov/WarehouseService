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
    public class ReceiptDocumentService : IReceiptDocumentService
    {
        private readonly IMapper _mapper;
        private readonly ApplicationContext _dbContext;

        public ReceiptDocumentService(
            IMapper mapper,
            ApplicationContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public async Task<ReceiptDocumentDto> Create(CreateReceiptDocumentDto model)
        {
            if (await _dbContext.ReceiptDocuments.AnyAsync(rd => rd.DocumentNumber == model.DocumentNumber))
                throw new ServiceException("Document Number Conflict", $"Document with number {model.DocumentNumber} already exist", StatusCodes.Status409Conflict);

            var receiptDocument = _mapper.Map<ReceiptDocument>(model);

            await _dbContext.ReceiptDocuments.AddAsync(receiptDocument);
            foreach(var receiptResource in receiptDocument.ReceiptResources)
            {
                //TODO: проверить чот будет если несоклько одинаовых не существующих ресурсов поступления
                var inventoryBalance = await _dbContext.InventoryBalances.FirstOrDefaultAsync(
                                                ib => ib.ResourseId == receiptResource.ResourceId && ib.UnitId == receiptResource.UnitId);

                if(inventoryBalance != null)
                {
                    inventoryBalance.Quantity += receiptResource.Quantity;
                    continue;
                }    
                
                inventoryBalance = new InventoryBalance()
                {
                    UnitId = receiptResource.UnitId,
                    ResourseId = receiptResource.ResourceId,
                    Quantity = receiptResource.Quantity
                };

                await _dbContext.InventoryBalances.AddAsync(inventoryBalance);
            }
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<ReceiptDocumentDto>(receiptDocument);
        }

        public async Task<ReceiptDocumentDto[]> Get(ReceiptDocumentFilter filter)
        {
            var isStartTimeNull = filter.StartTime == null;
            var isEndTimeNull = filter.EndTime == null;
            var isNumberNull = filter.DocumentNumber == null;
            var isResourceIdNull = filter.ResourceId == null;
            var isUnitIdNull = filter.UnitId == null;


            Expression<Func<ReceiptDocument, bool>> rdFilter = (rd) => (isStartTimeNull || rd.ReceiptDate >= filter.StartTime) &&
                                                                       (isEndTimeNull || rd.ReceiptDate <= filter.EndTime) &&
                                                                       (isNumberNull || rd.DocumentNumber == filter.DocumentNumber);

            var receiptDocument = await _dbContext.ReceiptDocuments.AsNoTracking()
                                                             .Include(rd => rd.ReceiptResources)
                                                                 .ThenInclude(rr => rr.Resource)
                                                             .Include(rd => rd.ReceiptResources)
                                                                 .ThenInclude(rr => rr.Unit)
                                                             .Where(rdFilter).ToArrayAsync();

            Func<ReceiptResourse, bool> rrFilter = (rr) => (isResourceIdNull || rr.ResourceId == filter.ResourceId) &&
                                                           (isUnitIdNull || rr.UnitId == filter.UnitId);

            receiptDocument = receiptDocument.Where(rd => rd.ReceiptResources.Any(rrFilter)).ToArray();

            return _mapper.Map<ReceiptDocumentDto[]>(receiptDocument);
        }

        public async Task<ReceiptDocumentDto> Get(Guid id)
        {
            var receiptDocument = await _dbContext.ReceiptDocuments.AsNoTracking()
                                                             .Include(rd => rd.ReceiptResources)
                                                                 .ThenInclude(rr => rr.Resource)
                                                             .Include(rd => rd.ReceiptResources)
                                                                 .ThenInclude(rr => rr.Unit)
                                                             .FirstOrDefaultAsync(rd => rd.Id == id);
            if (receiptDocument == null)
                throw new ServiceException("Receipt Document Not Found", $"Receipt document with id {id} not found", StatusCodes.Status404NotFound);

            return _mapper.Map<ReceiptDocumentDto>(receiptDocument);
        }

        public async Task<ReceiptDocumentDto> Update(Guid id, UpdateReceiptDocumentDto model)
        {
            if (await _dbContext.ReceiptDocuments.AnyAsync(rd => rd.Id != id && rd.DocumentNumber == model.DocumentNumber))
                throw new ServiceException("Document Number Conflict", $"Document with number {model.DocumentNumber} already exist", StatusCodes.Status409Conflict);

            var receiptDocument = await _dbContext.ReceiptDocuments.Include(rd => rd.ReceiptResources).FirstOrDefaultAsync(rd => rd.Id == id);
            if (receiptDocument == null)
                throw new ServiceException("Receipt Document Not Found", $"Receipt document with id {id} not found", StatusCodes.Status404NotFound);

            receiptDocument.DocumentNumber = model.DocumentNumber;
            receiptDocument.ReceiptDate = model.ReceiptDate;

            var inventoryBalances = new List<InventoryBalance>();
            foreach (var receiptResource in receiptDocument.ReceiptResources)
            {
                var inventoryBalance = await _dbContext.InventoryBalances.FirstAsync(
                                                ib => ib.ResourseId == receiptResource.ResourceId && ib.UnitId == receiptResource.UnitId);

                inventoryBalance.Quantity -= receiptResource.Quantity;
                inventoryBalances.Add(inventoryBalance);
                _dbContext.ReceiptResourses.Remove(receiptResource);
            }

            foreach (var receiptResource in model.ReceiptResources!)
            {
                //TODO: проверить чот будет если несоклько одинаовых не существующих ресурсов поступления
                var inventoryBalance = await _dbContext.InventoryBalances.FirstOrDefaultAsync(
                                                ib => ib.ResourseId == receiptResource.ResourceId && ib.UnitId == receiptResource.UnitId);

                var newReceiptRecource = _mapper.Map<ReceiptResourse>(receiptResource);
                newReceiptRecource.ReceiptDocumentId = id;
                await _dbContext.ReceiptResourses.AddAsync(newReceiptRecource);

                if (inventoryBalance != null)
                {
                    inventoryBalance.Quantity += receiptResource.Quantity;
                    continue;
                }


                inventoryBalance = new InventoryBalance()
                {
                    UnitId = receiptResource.UnitId!.Value,
                    ResourseId = receiptResource.ResourceId!.Value,
                    Quantity = receiptResource.Quantity,
                };

                await _dbContext.InventoryBalances.AddAsync(inventoryBalance);
            }

            foreach(var inventoryBalance in inventoryBalances)
            {
                if (inventoryBalance.Quantity < 0)
                    throw new ServiceException("Not Enough Resources", "There are not enough resources in the warehouse to delete document", StatusCodes.Status409Conflict);
            }

            await _dbContext.SaveChangesAsync();

            return _mapper.Map<ReceiptDocumentDto>(receiptDocument);
        }

        public async Task Delete(Guid id)
        {
            var receiptDocument = await _dbContext.ReceiptDocuments.Include(rd => rd.ReceiptResources).FirstOrDefaultAsync(rd => rd.Id == id);
            if (receiptDocument == null)
                throw new ServiceException("Receipt Document Not Found", $"Receipt document with id {id} not found", StatusCodes.Status404NotFound);

            _dbContext.ReceiptDocuments.Remove(receiptDocument);
            foreach(var receiptResource in receiptDocument.ReceiptResources)
            {
                var inventoryBalance = await _dbContext.InventoryBalances.FirstAsync(
                                                ib => ib.ResourseId == receiptResource.ResourceId && ib.UnitId == receiptResource.UnitId);

                inventoryBalance.Quantity -= receiptResource.Quantity;
                if (inventoryBalance.Quantity < 0)
                    throw new ServiceException("Not Enough Resources", "There are not enough resources in the warehouse to delete document", StatusCodes.Status409Conflict);

                if (inventoryBalance.Quantity == 0)
                    _dbContext.InventoryBalances.Remove(inventoryBalance);
            }
            await _dbContext.SaveChangesAsync();
        }
    }
}
