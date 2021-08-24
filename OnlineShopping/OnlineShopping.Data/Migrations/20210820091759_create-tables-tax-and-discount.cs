using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineShopping.Data.Migrations
{
    public partial class createtablestaxanddiscount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DiscountId",
                table: "Item",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TaxId",
                table: "Item",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "price",
                table: "Item",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateTable(
                name: "Discount",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    value = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Discount", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tax",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    value = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tax", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Item_DiscountId",
                table: "Item",
                column: "DiscountId");

            migrationBuilder.CreateIndex(
                name: "IX_Item_TaxId",
                table: "Item",
                column: "TaxId");

            migrationBuilder.AddForeignKey(
                name: "FK_Item_Discount_DiscountId",
                table: "Item",
                column: "DiscountId",
                principalTable: "Discount",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Item_Tax_TaxId",
                table: "Item",
                column: "TaxId",
                principalTable: "Tax",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Item_Discount_DiscountId",
                table: "Item");

            migrationBuilder.DropForeignKey(
                name: "FK_Item_Tax_TaxId",
                table: "Item");

            migrationBuilder.DropTable(
                name: "Discount");

            migrationBuilder.DropTable(
                name: "Tax");

            migrationBuilder.DropIndex(
                name: "IX_Item_DiscountId",
                table: "Item");

            migrationBuilder.DropIndex(
                name: "IX_Item_TaxId",
                table: "Item");

            migrationBuilder.DropColumn(
                name: "DiscountId",
                table: "Item");

            migrationBuilder.DropColumn(
                name: "TaxId",
                table: "Item");

            migrationBuilder.DropColumn(
                name: "price",
                table: "Item");
        }
    }
}
