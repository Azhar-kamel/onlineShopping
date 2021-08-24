using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineShopping.Data.Migrations
{
    public partial class add_attribute_image_in_item_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageId",
                table: "Item",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "Item");
        }
    }
}
