using WarehouseService.Enums;

namespace WarehouseService.Data.Entites
{
    public class ShipmentDocument : BaseEntity
    {
        public int Number { get; set; }
        public DateTime ShipmentDate { get; set; }
        public DocumentStatus Status { get; set; }

        public Guid ClientId { get; set; }
        public Client Client { get; set; } = null!;


        public Guid? ShipmentResourseId { get; set; }
        public ShipmentResource? ShipmentResourse { get; set; }
    }
}
