using Microsoft.AspNetCore.Mvc;

namespace WarehouseService.Models
{
    public class UnitsFilter
    {
        [FromQuery]
        public bool? Revoked { get; set; }
    }
}
