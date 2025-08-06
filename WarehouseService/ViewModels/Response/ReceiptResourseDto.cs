using WarehouseService.Data.Entites;

namespace WarehouseService.ViewModels.Response
{
    public class ReceiptResourseDto
    {
        public Guid ResourceId { get; set; }
        public ResourceDto? Resource { get; set; }

        public Guid UnitId { get; set; }
        public UnitDto? Unit { get; set; }

        public decimal Quantity { get; set; }
    }
}
