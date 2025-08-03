namespace WarehouseService.Data.Entites
{
    public class ReceiptResourse : BaseEntity
    {
        public Guid ResourceId { get; set; }
        public Resource Resource { get; set; } = null!;

        public Guid UnitId { get; set; }
        public Unit Unit { get; set; } = null!;

        public decimal Quantity { get; set; }
    }
}
