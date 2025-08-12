using Microsoft.AspNetCore.Mvc;

namespace WarehouseService.Models
{
    public class InventoryBalanceFilter
    {
        [FromQuery]
        public Guid? ResourceId { get; set; }

        [FromQuery]
        public Guid? UnitId { get; set; }
    }
}
