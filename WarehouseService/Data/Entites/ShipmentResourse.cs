namespace WarehouseService.Data.Entites
{
    public class ShipmentResourse : BaseEntity
    {
        public Guid ResourceId { get; set; }
        public Resource Resource { get; set; } = null!;

        public Guid UnitId { get; set; }
        public Unit Unit { get; set; } = null!;

        public decimal Quantity { get; set; }
    }
}
