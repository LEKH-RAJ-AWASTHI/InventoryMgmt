using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryMgmt.Migrations
{
    /// <inheritdoc />
    public partial class columnNamechangeinSalesModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "quantity",
                table: "tbl_sales",
                newName: "Quantity");

            migrationBuilder.RenameColumn(
                name: "total_price",
                table: "tbl_sales",
                newName: "TotalPrice");

            migrationBuilder.RenameColumn(
                name: "sales_price",
                table: "tbl_sales",
                newName: "SalesPrice");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "tbl_sales",
                newName: "quantity");

            migrationBuilder.RenameColumn(
                name: "TotalPrice",
                table: "tbl_sales",
                newName: "total_price");

            migrationBuilder.RenameColumn(
                name: "SalesPrice",
                table: "tbl_sales",
                newName: "sales_price");
        }
    }
}
