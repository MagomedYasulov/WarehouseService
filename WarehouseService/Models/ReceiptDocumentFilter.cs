using Microsoft.AspNetCore.Mvc;

namespace WarehouseService.Models
{
    public class ReceiptDocumentFilter
    {
        [FromQuery]
        public DateTime? StartTime { get; set; }
        
        [FromQuery]
        public DateTime? EndTime { get; set; }

        [FromQuery]
        public string? DocumentNumber { get; set; }
        
        [FromQuery]
        public Guid? ResourceId { get; set; }

        [FromQuery]
        public Guid? UnitId { get; set; }
    }
}
