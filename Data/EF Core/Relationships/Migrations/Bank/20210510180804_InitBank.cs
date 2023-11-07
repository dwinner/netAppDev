using Microsoft.EntityFrameworkCore.Migrations;

namespace Relationships.Migrations.Bank
{
   public partial class InitBank : Migration
   {
      protected override void Up(MigrationBuilder migrationBuilder)
      {
         migrationBuilder.EnsureSchema(
            "bank");

         migrationBuilder.CreateTable(
            "Payments",
            schema: "bank",
            columns: table => new
            {
               PaymentId = table.Column<int>("int", nullable: false)
                  .Annotation("SqlServer:Identity", "1, 1"),
               Name = table.Column<string>("nvarchar(20)", maxLength: 20, nullable: false),
               Amount = table.Column<decimal>("Money", nullable: false),
               Type = table.Column<string>("nvarchar(max)", nullable: false),
               CreditcardNumber = table.Column<string>("nvarchar(max)", nullable: true)
            },
            constraints: table => { table.PrimaryKey("PK_Payments", x => x.PaymentId); });
      }

      protected override void Down(MigrationBuilder migrationBuilder)
      {
         migrationBuilder.DropTable(
            "Payments",
            "bank");
      }
   }
}