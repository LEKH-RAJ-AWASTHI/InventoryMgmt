using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryMgmt.Migrations
{
    /// <inheritdoc />
    public partial class AddedcolumnnametypeinNotificationtable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "tbl_notifications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "tbl_notifications");
        }
    }
}
