using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Coronfiguration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attractions_GeoLocations_GeoLocationId",
                table: "Attractions");

            migrationBuilder.DropIndex(
                name: "IX_Attractions_GeoLocationId",
                table: "Attractions");

            migrationBuilder.AddColumn<int>(
                name: "AttractionId",
                table: "GeoLocations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_GeoLocations_AttractionId",
                table: "GeoLocations",
                column: "AttractionId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_GeoLocations_Attractions_AttractionId",
                table: "GeoLocations",
                column: "AttractionId",
                principalTable: "Attractions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GeoLocations_Attractions_AttractionId",
                table: "GeoLocations");

            migrationBuilder.DropIndex(
                name: "IX_GeoLocations_AttractionId",
                table: "GeoLocations");

            migrationBuilder.DropColumn(
                name: "AttractionId",
                table: "GeoLocations");

            migrationBuilder.CreateIndex(
                name: "IX_Attractions_GeoLocationId",
                table: "Attractions",
                column: "GeoLocationId",
                unique: true,
                filter: "[GeoLocationId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Attractions_GeoLocations_GeoLocationId",
                table: "Attractions",
                column: "GeoLocationId",
                principalTable: "GeoLocations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
