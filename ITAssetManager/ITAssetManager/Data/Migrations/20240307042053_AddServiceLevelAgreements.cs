using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ITAssetManager.Migrations
{
    /// <inheritdoc />
    public partial class AddServiceLevelAgreements : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ServiceLevelAgreements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EndDateOfMonth = table.Column<DateOnly>(type: "date", nullable: false),
                    DisabilityHoursAverage = table.Column<double>(type: "float", nullable: false),
                    DisabilityHoursLevel = table.Column<int>(type: "int", nullable: false),
                    UptimeRate = table.Column<double>(type: "float", nullable: false),
                    UptimeLevel = table.Column<int>(type: "int", nullable: false),
                    InspectionComplianceRate = table.Column<double>(type: "float", nullable: false),
                    InspectionComplianceLevel = table.Column<int>(type: "int", nullable: false),
                    TechnicalSupportCompletionRate = table.Column<double>(type: "float", nullable: false),
                    TechnicalSupportLevel = table.Column<int>(type: "int", nullable: false),
                    SecurityIssues = table.Column<int>(type: "int", nullable: false),
                    SecurityIssuesLevel = table.Column<int>(type: "int", nullable: false),
                    Version = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceLevelAgreements", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ServiceLevelAgreements");
        }
    }
}
