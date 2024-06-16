using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class CorrectForeginKey : Migration
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

            migrationBuilder.AlterColumn<int>(
                name: "GeoLocationId",
                table: "Attractions",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

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
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attractions_GeoLocations_GeoLocationId",
                table: "Attractions");

            migrationBuilder.DropIndex(
                name: "IX_Attractions_GeoLocationId",
                table: "Attractions");

            migrationBuilder.AlterColumn<int>(
                name: "GeoLocationId",
                table: "Attractions",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Attractions_GeoLocationId",
                table: "Attractions",
                column: "GeoLocationId",
                unique: true);

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
