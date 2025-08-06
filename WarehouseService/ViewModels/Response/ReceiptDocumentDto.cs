using WarehouseService.Data.Entites;

namespace WarehouseService.ViewModels.Response
{
    public class ReceiptDocumentDto
    {
        public string DocumentNumber { get; set; } = string.Empty;
        public DateTime ReceiptDate { get; set; }

        public ReceiptResourseDto[] ReceiptResourses { get; set; } = [];
    }
}
