using Microsoft.EntityFrameworkCore.Migrations;

namespace Catalog.Api.Infrastructure.Migrations
{
    public partial class UpdateSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "catalog");

            migrationBuilder.RenameTable(
                name: "CatalogTypes",
                newName: "CatalogTypes",
                newSchema: "catalog");

            migrationBuilder.RenameTable(
                name: "CatalogItems",
                newName: "CatalogItems",
                newSchema: "catalog");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "CatalogTypes",
                schema: "catalog",
                newName: "CatalogTypes");

            migrationBuilder.RenameTable(
                name: "CatalogItems",
                schema: "catalog",
                newName: "CatalogItems");
        }
    }
}
