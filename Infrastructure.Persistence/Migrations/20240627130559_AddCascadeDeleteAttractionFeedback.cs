using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddCascadeDeleteAttractionFeedback : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AttractionFeedback_Attractions_AttractionId",
                table: "AttractionFeedback");

            migrationBuilder.AddForeignKey(
                name: "FK_AttractionFeedback_Attractions_AttractionId",
                table: "AttractionFeedback",
                column: "AttractionId",
                principalTable: "Attractions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AttractionFeedback_Attractions_AttractionId",
                table: "AttractionFeedback");

            migrationBuilder.AddForeignKey(
                name: "FK_AttractionFeedback_Attractions_AttractionId",
                table: "AttractionFeedback",
                column: "AttractionId",
                principalTable: "Attractions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
