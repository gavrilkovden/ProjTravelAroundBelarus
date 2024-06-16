using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddGeoLocationToAttraction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GeoLocationId",
                table: "Attractions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "GeoLocations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Latitude = table.Column<double>(type: "float", maxLength: 50, nullable: true),
                    Longitude = table.Column<double>(type: "float", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeoLocations", x => x.Id);
                });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attractions_GeoLocations_GeoLocationId",
                table: "Attractions");

            migrationBuilder.DropTable(
                name: "GeoLocations");

            migrationBuilder.DropIndex(
                name: "IX_Attractions_GeoLocationId",
                table: "Attractions");

            migrationBuilder.DropColumn(
                name: "GeoLocationId",
                table: "Attractions");
        }
    }
}
