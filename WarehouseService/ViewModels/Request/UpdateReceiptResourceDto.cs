namespace WarehouseService.ViewModels.Request
{
    public class UpdateReceiptResourceDto
    {
        public Guid? ResourceId { get; set; }
        public Guid? UnitId { get; set; }
        public decimal Quantity { get; set; }
    }
}
