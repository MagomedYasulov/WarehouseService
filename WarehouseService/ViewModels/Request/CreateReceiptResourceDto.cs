using WarehouseService.Data.Entites;

namespace WarehouseService.ViewModels.Request
{
    public class CreateReceiptResourceDto
    {
        public Guid? ResourceId { get; set; }
        public Guid? UnitId { get; set; }
        public decimal Quantity { get; set; }
    }
}
