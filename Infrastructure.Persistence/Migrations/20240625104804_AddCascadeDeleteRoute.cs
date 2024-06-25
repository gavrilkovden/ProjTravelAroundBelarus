using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddCascadeDeleteRoute : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AttractionInRoutes_Routes_RouteId",
                table: "AttractionInRoutes");

            migrationBuilder.AddForeignKey(
                name: "FK_AttractionInRoutes_Routes_RouteId",
                table: "AttractionInRoutes",
                column: "RouteId",
                principalTable: "Routes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AttractionInRoutes_Routes_RouteId",
                table: "AttractionInRoutes");

            migrationBuilder.AddForeignKey(
                name: "FK_AttractionInRoutes_Routes_RouteId",
                table: "AttractionInRoutes",
                column: "RouteId",
                principalTable: "Routes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
