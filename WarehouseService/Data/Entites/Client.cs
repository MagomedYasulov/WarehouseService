namespace WarehouseService.Data.Entites
{
    public class Client : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;

        /// <summary>
        /// Состояние
        /// </summary>
        public bool Revoked { get; set; }
    }
}
