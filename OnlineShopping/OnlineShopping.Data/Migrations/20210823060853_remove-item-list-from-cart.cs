using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineShopping.Data.Migrations
{
    public partial class removeitemlistfromcart : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Item_Cart_CartId",
                table: "Item");

            migrationBuilder.DropIndex(
                name: "IX_Item_CartId",
                table: "Item");

            migrationBuilder.DropColumn(
                name: "CartId",
                table: "Item");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CartId",
                table: "Item",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Item_CartId",
                table: "Item",
                column: "CartId");

            migrationBuilder.AddForeignKey(
                name: "FK_Item_Cart_CartId",
                table: "Item",
                column: "CartId",
                principalTable: "Cart",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
