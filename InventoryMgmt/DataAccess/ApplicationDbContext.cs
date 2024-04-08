
using InventoryMgmt.Model;
using Microsoft.EntityFrameworkCore;

namespace InventoryMgmt.DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<ItemModel> items { get; set; }
        public DbSet<UserModel> users { get; set; }
        public DbSet<StoreModel> stores { get; set; }
        public DbSet<StockModel> stocks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseSqlServer(@"Server =.\SQLEXPRESS; Database = Inventory_DB; Trusted_Connection = True; MultipleActiveResultSets = true; TrustServerCertificate = true");

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ItemModel>().ToTable("tbl_item");
            modelBuilder.Entity<UserModel>().ToTable("tbl_user");
            modelBuilder.Entity<StockModel>().ToTable("tbl_stock");
            modelBuilder.Entity<StoreModel>().ToTable("tbl_store");
        }

    }
}
