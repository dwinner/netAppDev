using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UnitTests.Migrations
{
   public partial class InitialVersion : Migration
   {
      protected override void Up(MigrationBuilder migrationBuilder)
      {
         migrationBuilder.CreateTable(
            "Customers",
            table => new
            {
               CustomerId = table.Column<int>("int", nullable: false)
                  .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
               Name = table.Column<string>("nvarchar(max)", nullable: false),
               Contact_Email = table.Column<string>("nvarchar(max)", nullable: true),
               Contact_Phone = table.Column<string>("nvarchar(max)", nullable: true)
            },
            constraints: table => { table.PrimaryKey("PK_Customers", x => x.CustomerId); });

         migrationBuilder.CreateTable(
            "Projects",
            table => new
            {
               ProjectId = table.Column<int>("int", nullable: false)
                  .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
               CustomerId = table.Column<int>("int", nullable: false),
               Description = table.Column<string>("nvarchar(max)", nullable: true),
               End = table.Column<DateTime>("datetime2", nullable: true),
               Name = table.Column<string>("nvarchar(max)", nullable: false),
               Start = table.Column<DateTime>("datetime2", nullable: false)
            },
            constraints: table =>
            {
               table.PrimaryKey("PK_Projects", x => x.ProjectId);
               table.ForeignKey(
                  "FK_Projects_Customers_CustomerId",
                  x => x.CustomerId,
                  "Customers",
                  "CustomerId",
                  onDelete: ReferentialAction.Cascade);
            });

         migrationBuilder.CreateTable(
            "ProjectDetail",
            table => new
            {
               ProjectId = table.Column<int>("int", nullable: false),
               Budget = table.Column<decimal>("decimal(18, 2)", nullable: false),
               Critical = table.Column<bool>("bit", nullable: false)
            },
            constraints: table =>
            {
               table.PrimaryKey("PK_ProjectDetail", x => x.ProjectId);
               table.ForeignKey(
                  "FK_ProjectDetail_Projects_ProjectId",
                  x => x.ProjectId,
                  "Projects",
                  "ProjectId",
                  onDelete: ReferentialAction.Cascade);
            });

         migrationBuilder.CreateTable(
            "ProjectResource",
            table => new
            {
               ProjectResourceId = table.Column<int>("int", nullable: false)
                  .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
               ProjectId = table.Column<int>("int", nullable: false),
               ResourceId = table.Column<int>("int", nullable: true),
               Role = table.Column<int>("int", nullable: false)
            },
            constraints: table =>
            {
               table.PrimaryKey("PK_ProjectResource", x => x.ProjectResourceId);
               table.ForeignKey(
                  "FK_ProjectResource_Projects_ProjectId",
                  x => x.ProjectId,
                  "Projects",
                  "ProjectId",
                  onDelete: ReferentialAction.Cascade);
            });

         migrationBuilder.CreateTable(
            "Technologies",
            table => new
            {
               TechnologyId = table.Column<int>("int", nullable: false)
                  .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
               Name = table.Column<string>("nvarchar(50)", maxLength: 50, nullable: false),
               ResourceId = table.Column<int>("int", nullable: true)
            },
            constraints: table => { table.PrimaryKey("PK_Technologies", x => x.TechnologyId); });

         migrationBuilder.CreateTable(
            "Resources",
            table => new
            {
               ResourceId = table.Column<int>("int", nullable: false)
                  .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
               Name = table.Column<string>("nvarchar(50)", maxLength: 50, nullable: false),
               TechnologyId = table.Column<int>("int", nullable: true),
               Contact_Email = table.Column<string>("nvarchar(max)", nullable: true),
               Contact_Phone = table.Column<string>("nvarchar(max)", nullable: true)
            },
            constraints: table =>
            {
               table.PrimaryKey("PK_Resources", x => x.ResourceId);
               table.ForeignKey(
                  "FK_Resources_Technologies_TechnologyId",
                  x => x.TechnologyId,
                  "Technologies",
                  "TechnologyId",
                  onDelete: ReferentialAction.Restrict);
            });

         migrationBuilder.CreateIndex(
            "IX_ProjectResource_ProjectId",
            "ProjectResource",
            "ProjectId");

         migrationBuilder.CreateIndex(
            "IX_ProjectResource_ResourceId",
            "ProjectResource",
            "ResourceId");

         migrationBuilder.CreateIndex(
            "IX_Projects_CustomerId",
            "Projects",
            "CustomerId");

         migrationBuilder.CreateIndex(
            "IX_Resources_TechnologyId",
            "Resources",
            "TechnologyId");

         migrationBuilder.CreateIndex(
            "IX_Technologies_ResourceId",
            "Technologies",
            "ResourceId");

         migrationBuilder.AddForeignKey(
            "FK_ProjectResource_Resources_ResourceId",
            "ProjectResource",
            "ResourceId",
            "Resources",
            principalColumn: "ResourceId",
            onDelete: ReferentialAction.Restrict);

         migrationBuilder.AddForeignKey(
            "FK_Technologies_Resources_ResourceId",
            "Technologies",
            "ResourceId",
            "Resources",
            principalColumn: "ResourceId",
            onDelete: ReferentialAction.Restrict);
      }

      protected override void Down(MigrationBuilder migrationBuilder)
      {
         migrationBuilder.DropForeignKey(
            "FK_Technologies_Resources_ResourceId",
            "Technologies");

         migrationBuilder.DropTable(
            "ProjectDetail");

         migrationBuilder.DropTable(
            "ProjectResource");

         migrationBuilder.DropTable(
            "Projects");

         migrationBuilder.DropTable(
            "Customers");

         migrationBuilder.DropTable(
            "Resources");

         migrationBuilder.DropTable(
            "Technologies");
      }
   }
}