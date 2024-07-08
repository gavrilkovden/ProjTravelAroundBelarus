using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddCascadeDeleteAddressandLocation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attractions_Address_AddressId1",
                table: "Attractions");

            migrationBuilder.DropIndex(
                name: "IX_Attractions_AddressId1",
                table: "Attractions");

            migrationBuilder.DropColumn(
                name: "AddressId1",
                table: "Attractions");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AddressId1",
                table: "Attractions",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Attractions_AddressId1",
                table: "Attractions",
                column: "AddressId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Attractions_Address_AddressId1",
                table: "Attractions",
                column: "AddressId1",
                principalTable: "Address",
                principalColumn: "Id");
        }
    }
}
