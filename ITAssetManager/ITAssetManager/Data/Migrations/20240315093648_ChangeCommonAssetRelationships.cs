using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ITAssetManager.Migrations
{
    /// <inheritdoc />
    public partial class ChangeCommonAssetRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SecurityVulnerabilities_CommonAssetId",
                table: "SecurityVulnerabilities");

            migrationBuilder.DropIndex(
                name: "IX_RoutineChecks_CommonAssetId",
                table: "RoutineChecks");

            migrationBuilder.CreateIndex(
                name: "IX_SecurityVulnerabilities_CommonAssetId",
                table: "SecurityVulnerabilities",
                column: "CommonAssetId");

            migrationBuilder.CreateIndex(
                name: "IX_RoutineChecks_CommonAssetId",
                table: "RoutineChecks",
                column: "CommonAssetId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SecurityVulnerabilities_CommonAssetId",
                table: "SecurityVulnerabilities");

            migrationBuilder.DropIndex(
                name: "IX_RoutineChecks_CommonAssetId",
                table: "RoutineChecks");

            migrationBuilder.CreateIndex(
                name: "IX_SecurityVulnerabilities_CommonAssetId",
                table: "SecurityVulnerabilities",
                column: "CommonAssetId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RoutineChecks_CommonAssetId",
                table: "RoutineChecks",
                column: "CommonAssetId",
                unique: true);
        }
    }
}
