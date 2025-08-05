namespace WarehouseService.ViewModels.Response
{
    public class UnitDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool Revoked { get; set; }
    }
}
