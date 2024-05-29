using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryMgmt.Migrations
{
    /// <inheritdoc />
    public partial class seeddataaddedforuser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "tbl_user",
                columns: new[] { "userId", "email", "fullName", "isActive", "password", "role", "username" },
                values: new object[] { 1, "lekhrajawasthi123@gmail.com", "Admin Admin", true, "pass123", "Admin", "admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "tbl_user",
                keyColumn: "userId",
                keyValue: 1);
        }
    }
}
