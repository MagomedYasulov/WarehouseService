using Microsoft.EntityFrameworkCore;
using WarehouseService.Data.Entites;

namespace WarehouseService.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Resource> Resources => Set<Resource>();
        public DbSet<Unit> Units => Set<Unit>();
        public DbSet<Client> Clients => Set<Client>();
        public DbSet<InventoryBalance> InventoryBalances => Set<InventoryBalance>();
        public DbSet<ReceiptDocument> ReceiptDocuments => Set<ReceiptDocument>();
        public DbSet<ReceiptResourse> ReceiptResourses => Set<ReceiptResourse>();
        public DbSet<ShipmentDocument> ShipmentDocuments => Set<ShipmentDocument>();
        public DbSet<ShipmentResource> ShipmentResources => Set<ShipmentResource>();


        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<InventoryBalance>().HasIndex(ib => new { ib.ResourseId, ib.UnitId }).IsUnique();

            modelBuilder.Entity<Resource>().HasIndex(r => r.Name).IsUnique();
            modelBuilder.Entity<Unit>().HasIndex(r => r.Name).IsUnique();

        }
    }
}
