﻿// <auto-generated />
using System;
using InventoryMgmt.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace InventoryMgmt.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("InventoryMgmt.EmailLogs", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsSent")
                        .HasColumnType("bit");

                    b.Property<int>("ItemId")
                        .HasColumnType("int");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("User")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ItemId");

                    b.ToTable("email_logs", (string)null);
                });

            modelBuilder.Entity("InventoryMgmt.Model.ItemModel", b =>
                {
                    b.Property<int>("ItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ItemId"));

                    b.Property<string>("BrandName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("ItemCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ItemName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ItemNo")
                        .HasColumnType("int");

                    b.Property<decimal>("PurchaseRate")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("SalesRate")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("UnitOfMeasurement")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ItemId");

                    b.HasIndex("ItemCode")
                        .IsUnique();

                    b.ToTable("tbl_item", (string)null);
                });

            modelBuilder.Entity("InventoryMgmt.Model.SalesModel", b =>
                {
                    b.Property<int>("salesId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("salesId"));

                    b.Property<decimal>("Quantity")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("SalesPrice")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("TotalPrice")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("dateTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("itemId")
                        .HasColumnType("int");

                    b.Property<int>("storeId")
                        .HasColumnType("int");

                    b.HasKey("salesId");

                    b.HasIndex("itemId");

                    b.HasIndex("storeId");

                    b.ToTable("tbl_sales", (string)null);
                });

            modelBuilder.Entity("InventoryMgmt.Model.StockModel", b =>
                {
                    b.Property<int>("stockId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("stockId"));

                    b.Property<DateTime>("expiryDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("itemId")
                        .HasColumnType("int");

                    b.Property<decimal>("quantity")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("storeId")
                        .HasColumnType("int");

                    b.HasKey("stockId");

                    b.HasIndex("itemId");

                    b.HasIndex("storeId");

                    b.ToTable("tbl_stock", (string)null);
                });

            modelBuilder.Entity("InventoryMgmt.Model.StoreModel", b =>
                {
                    b.Property<int>("storeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("storeId"));

                    b.Property<bool>("isActive")
                        .HasColumnType("bit");

                    b.Property<string>("storeName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("storeId");

                    b.ToTable("tbl_store", (string)null);
                });

            modelBuilder.Entity("InventoryMgmt.Model.UserModel", b =>
                {
                    b.Property<int>("userId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("userId"));

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("fullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("isActive")
                        .HasColumnType("bit");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("userId");

                    b.ToTable("tbl_user", (string)null);

                    b.HasData(
                        new
                        {
                            userId = 1,
                            email = "lekhrajawasthi123@gmail.com",
                            fullName = "Admin Admin",
                            isActive = true,
                            password = "pass123",
                            role = "Admin",
                            username = "admin"
                        });
                });

            modelBuilder.Entity("InventoryMgmt.EmailLogs", b =>
                {
                    b.HasOne("InventoryMgmt.Model.ItemModel", "item")
                        .WithMany("emailLogs")
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("item");
                });

            modelBuilder.Entity("InventoryMgmt.Model.SalesModel", b =>
                {
                    b.HasOne("InventoryMgmt.Model.ItemModel", "item")
                        .WithMany("sales")
                        .HasForeignKey("itemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("InventoryMgmt.Model.StoreModel", "store")
                        .WithMany("sales")
                        .HasForeignKey("storeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("item");

                    b.Navigation("store");
                });

            modelBuilder.Entity("InventoryMgmt.Model.StockModel", b =>
                {
                    b.HasOne("InventoryMgmt.Model.ItemModel", "item")
                        .WithMany("stocks")
                        .HasForeignKey("itemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("InventoryMgmt.Model.StoreModel", "store")
                        .WithMany("stocks")
                        .HasForeignKey("storeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("item");

                    b.Navigation("store");
                });

            modelBuilder.Entity("InventoryMgmt.Model.ItemModel", b =>
                {
                    b.Navigation("emailLogs");

                    b.Navigation("sales");

                    b.Navigation("stocks");
                });

            modelBuilder.Entity("InventoryMgmt.Model.StoreModel", b =>
                {
                    b.Navigation("sales");

                    b.Navigation("stocks");
                });
#pragma warning restore 612, 618
        }
    }
}
