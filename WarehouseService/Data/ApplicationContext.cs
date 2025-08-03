using Microsoft.EntityFrameworkCore;

namespace WarehouseService.Data
{
    public class ApplicationContext : DbContext
    {


        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
      

        }
    }
}
