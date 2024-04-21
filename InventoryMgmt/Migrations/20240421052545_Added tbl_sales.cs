using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryMgmt.Migrations
{
    /// <inheritdoc />
    public partial class Addedtbl_sales : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_sales",
                columns: table => new
                {
                    salesId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    dateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    itemId = table.Column<int>(type: "int", nullable: false),
                    quantity = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    sales_price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    total_price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    storeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_sales", x => x.salesId);
                    table.ForeignKey(
                        name: "FK_tbl_sales_tbl_item_itemId",
                        column: x => x.itemId,
                        principalTable: "tbl_item",
                        principalColumn: "ItemId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_sales_tbl_store_storeId",
                        column: x => x.storeId,
                        principalTable: "tbl_store",
                        principalColumn: "storeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_sales_itemId",
                table: "tbl_sales",
                column: "itemId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_sales_storeId",
                table: "tbl_sales",
                column: "storeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_sales");
        }
    }
}
