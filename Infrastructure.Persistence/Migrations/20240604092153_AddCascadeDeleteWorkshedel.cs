using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddCascadeDeleteWorkshedel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AttractionFeedback_Attractions_AttractionId",
                table: "AttractionFeedback");

            migrationBuilder.DropForeignKey(
                name: "FK_AttractionInRoutes_Attractions_AttractionId",
                table: "AttractionInRoutes");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkSchedules_Attractions_AttractionId",
                table: "WorkSchedules");

            migrationBuilder.AddForeignKey(
                name: "FK_AttractionFeedback_Attractions_AttractionId",
                table: "AttractionFeedback",
                column: "AttractionId",
                principalTable: "Attractions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AttractionInRoutes_Attractions_AttractionId",
                table: "AttractionInRoutes",
                column: "AttractionId",
                principalTable: "Attractions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkSchedules_Attractions_AttractionId",
                table: "WorkSchedules",
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

            migrationBuilder.DropForeignKey(
                name: "FK_AttractionInRoutes_Attractions_AttractionId",
                table: "AttractionInRoutes");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkSchedules_Attractions_AttractionId",
                table: "WorkSchedules");

            migrationBuilder.AddForeignKey(
                name: "FK_AttractionFeedback_Attractions_AttractionId",
                table: "AttractionFeedback",
                column: "AttractionId",
                principalTable: "Attractions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AttractionInRoutes_Attractions_AttractionId",
                table: "AttractionInRoutes",
                column: "AttractionId",
                principalTable: "Attractions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkSchedules_Attractions_AttractionId",
                table: "WorkSchedules",
                column: "AttractionId",
                principalTable: "Attractions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
