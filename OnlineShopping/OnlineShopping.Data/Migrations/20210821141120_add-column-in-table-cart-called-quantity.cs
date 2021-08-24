using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineShopping.Data.Migrations
{
    public partial class addcolumnintablecartcalledquantity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "CartItem",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "CartItem");
        }
    }
}
