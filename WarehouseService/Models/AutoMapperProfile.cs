using AutoMapper;
using WarehouseService.Data.Entites;
using WarehouseService.ViewModels.Request;
using WarehouseService.ViewModels.Response;

namespace WarehouseService.Models
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Resource, ResourceDto>();
            CreateMap<Unit, UnitDto>();
            CreateMap<CreateReceiptDocumentDto, ReceiptDocument>();
            CreateMap<CreateReceiptResourceDto, ReceiptResourse>();
            CreateMap<UpdateReceiptDocumentDto, ReceiptDocument>();
            CreateMap<UpdateReceiptResourceDto, ReceiptResourse>();

            CreateMap<ReceiptDocument, ReceiptDocumentDto>();
            CreateMap<ReceiptResourse, ReceiptResourseDto>();

            CreateMap<InventoryBalance, InventoryBalanceDto>();
        }
    }
}
