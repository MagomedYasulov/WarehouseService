
namespace WarehouseService.Data.Entites
{
    public class Resource : BaseEntity
    {
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Состояние
        /// </summary>
        public bool Revoked { get; set; }
    }
}
