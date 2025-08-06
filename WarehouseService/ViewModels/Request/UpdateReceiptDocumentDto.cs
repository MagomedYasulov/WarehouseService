namespace WarehouseService.ViewModels.Request
{
    public class UpdateReceiptDocumentDto
    {
        public string DocumentNumber { get; set; } = string.Empty;
        public DateTime ReceiptDate { get; set; }

        public UpdateReceiptResourceDto[]? ReceiptResources { get; set; } = [];
    }
}
