using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ITAssetManager.Migrations
{
    /// <inheritdoc />
    public partial class AddRoutineChecksSecurityVulnerabilities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RoutineChecks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Detail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Version = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    CommonAssetId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoutineChecks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoutineChecks_CommonAssets_CommonAssetId",
                        column: x => x.CommonAssetId,
                        principalTable: "CommonAssets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SecurityVulnerabilities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DiscoveryDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VulnerabilityDetail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VisitDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ResolveDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TaskDetail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsResolved = table.Column<bool>(type: "bit", nullable: false),
                    Version = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    CommonAssetId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SecurityVulnerabilities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SecurityVulnerabilities_CommonAssets_CommonAssetId",
                        column: x => x.CommonAssetId,
                        principalTable: "CommonAssets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RoutineChecks_CommonAssetId",
                table: "RoutineChecks",
                column: "CommonAssetId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SecurityVulnerabilities_CommonAssetId",
                table: "SecurityVulnerabilities",
                column: "CommonAssetId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoutineChecks");

            migrationBuilder.DropTable(
                name: "SecurityVulnerabilities");
        }
    }
}
