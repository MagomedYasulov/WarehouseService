namespace WarehouseService.Data.Entites
{
    public class ReceiptDocument : BaseEntity
    {
        public int Number { get; set; }
        public DateTime ReceiptDate { get; set; }

        public ReceiptResourse[] ReceiptResourses { get; set; } = [];
    }
}
