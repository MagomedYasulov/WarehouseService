namespace WarehouseService.Data.Entites
{
    public class ReceiptDocument : BaseEntity
    {
        public string DocumentNumber { get; set; } = string.Empty;
        public DateTime ReceiptDate { get; set; }

        public ReceiptResourse[] ReceiptResources { get; set; } = [];
    }
}
