using Microsoft.EntityFrameworkCore.Migrations;

namespace Relationships.Migrations.Menus
{
   public partial class InitMenus : Migration
   {
      protected override void Up(MigrationBuilder migrationBuilder)
      {
         migrationBuilder.EnsureSchema(
            "ms");

         migrationBuilder.CreateTable(
            "MenuItems",
            schema: "ms",
            columns: table => new
            {
               MenuItemId = table.Column<int>("int", nullable: false)
                  .Annotation("SqlServer:Identity", "1, 1"),
               Title = table.Column<string>("nvarchar(max)", nullable: false),
               Subtitle = table.Column<string>("nvarchar(max)", nullable: true),
               Price = table.Column<decimal>("decimal(18,2)", nullable: false),
               KitchenInfo = table.Column<string>("nvarchar(max)", nullable: true),
               MenusSold = table.Column<int>("int", nullable: true)
            },
            constraints: table => { table.PrimaryKey("PK_MenuItems", x => x.MenuItemId); });
      }

      protected override void Down(MigrationBuilder migrationBuilder)
      {
         migrationBuilder.DropTable(
            "MenuItems",
            "ms");
      }
   }
}