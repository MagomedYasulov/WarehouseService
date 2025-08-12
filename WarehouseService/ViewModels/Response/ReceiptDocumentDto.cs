using WarehouseService.Data.Entites;

namespace WarehouseService.ViewModels.Response
{
    public class ReceiptDocumentDto
    {
        public Guid Id { get; set; }
        public string DocumentNumber { get; set; } = string.Empty;
        public DateTime ReceiptDate { get; set; }

        public ReceiptResourseDto[] ReceiptResources { get; set; } = [];
    }
}
