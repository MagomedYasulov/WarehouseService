using Microsoft.AspNetCore.Mvc;

namespace WarehouseService.Models
{
    public class ResourceFilter
    {
        [FromQuery]
        public bool? Revoked { get; set; }
    }
}
