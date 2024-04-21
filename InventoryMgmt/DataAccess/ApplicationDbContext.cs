
using InventoryMgmt.Model;
using Microsoft.EntityFrameworkCore;

namespace InventoryMgmt.DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {
            
        }
        public DbSet<ItemModel> items { get; set; }
        public DbSet<UserModel> users { get; set; }
        public DbSet<StoreModel> stores { get; set; }
        public DbSet<StockModel> stocks { get; set; }
        public DbSet<SalesModel> sales { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ItemModel>().ToTable("tbl_item");
            modelBuilder.Entity<ItemModel>()
                .HasMany(a => a.stocks)
                .WithOne(b => b.item)
                .HasForeignKey(b => b.itemId);
            modelBuilder.Entity<ItemModel>()
                .HasMany(a=> a.sales)
                .WithOne(b => b.item)
                .HasForeignKey(b => b.itemId);

            //making itemCode as unique attribute
            modelBuilder.Entity<ItemModel>()
                .HasIndex(u => u.ItemCode)
                .IsUnique();
            modelBuilder.Entity<UserModel>().ToTable("tbl_user");

            modelBuilder.Entity<StockModel>().ToTable("tbl_stock");

            modelBuilder.Entity<StoreModel>().ToTable("tbl_store");
            modelBuilder.Entity<StoreModel>()
                .HasMany(a => a.stocks)
                .WithOne(b => b.store)
                .HasForeignKey(b => b.storeId);
            //tbl_sales has foreign key of 
            modelBuilder.Entity<StoreModel>()
                .HasMany(a => a.sales)
                .WithOne(b=>b.store)
                .HasForeignKey(b => b.storeId);

            modelBuilder.Entity<SalesModel>().ToTable("tbl_sales");
            modelBuilder.Entity<SalesModel>().Property(x => x.Quantity).HasPrecision(18, 2);
            modelBuilder.Entity<SalesModel>().Property(x => x.SalesPrice).HasPrecision(18, 2);
            modelBuilder.Entity<SalesModel>().Property(x =>x.TotalPrice).HasPrecision(18, 2);

        }
    }
}
