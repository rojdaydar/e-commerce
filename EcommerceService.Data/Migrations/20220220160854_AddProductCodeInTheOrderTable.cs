using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcommerceService.Data.Migrations
{
    public partial class AddProductCodeInTheOrderTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProductCode",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductCode",
                table: "Orders");
        }
    }
}
