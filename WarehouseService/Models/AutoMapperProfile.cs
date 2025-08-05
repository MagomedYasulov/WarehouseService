using AutoMapper;
using WarehouseService.Data.Entites;
using WarehouseService.ViewModels.Response;

namespace WarehouseService.Models
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Resource, ResourceDto>();
            CreateMap<Unit, UnitDto>();
        }
    }
}
