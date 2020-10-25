using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Catalog.Infrastructure.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "catalog");

            migrationBuilder.CreateTable(
                name: "CatalogTypes",
                schema: "catalog",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatalogTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CatalogItems",
                schema: "catalog",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CatalogTypeId = table.Column<Guid>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Stock = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatalogItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CatalogItems_CatalogTypes_CatalogTypeId",
                        column: x => x.CatalogTypeId,
                        principalSchema: "catalog",
                        principalTable: "CatalogTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CatalogItems_CatalogTypeId",
                schema: "catalog",
                table: "CatalogItems",
                column: "CatalogTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CatalogItems",
                schema: "catalog");

            migrationBuilder.DropTable(
                name: "CatalogTypes",
                schema: "catalog");
        }
    }
}
