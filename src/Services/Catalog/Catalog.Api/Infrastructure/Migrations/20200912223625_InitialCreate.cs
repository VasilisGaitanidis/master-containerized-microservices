using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Catalog.Api.Infrastructure.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "catalog_types",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_catalog_types", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "catalog_items",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    catalog_type_id = table.Column<Guid>(nullable: false),
                    description = table.Column<string>(nullable: true),
                    name = table.Column<string>(nullable: false),
                    price = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    stock = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_catalog_items", x => x.id);
                    table.ForeignKey(
                        name: "FK_catalog_items_catalog_types_catalog_type_id",
                        column: x => x.catalog_type_id,
                        principalTable: "catalog_types",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_catalog_items_catalog_type_id",
                table: "catalog_items",
                column: "catalog_type_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "catalog_items");

            migrationBuilder.DropTable(
                name: "catalog_types");
        }
    }
}
