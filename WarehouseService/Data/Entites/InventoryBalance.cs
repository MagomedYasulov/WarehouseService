namespace WarehouseService.Data.Entites
{
    public class InventoryBalance : BaseEntity
    {
        public decimal Quantity { get; set; }

        public Guid ResourseId { get; set; }
        public Resource Resource { get; set; } = null!;
        public Guid UnitId { get; set; }
        public Unit Unit { get; set; } = null!;
    }
}
