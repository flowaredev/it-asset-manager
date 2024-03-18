using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ITAssetManager.Migrations
{
    /// <inheritdoc />
    public partial class AddServiceLevelAgreementRoutineCheck : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "InspectionComplianceRate",
                table: "ServiceLevelAgreements",
                newName: "RoutineCheckRate");

            migrationBuilder.RenameColumn(
                name: "InspectionComplianceLevel",
                table: "ServiceLevelAgreements",
                newName: "RoutineCheckLevel");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RoutineCheckRate",
                table: "ServiceLevelAgreements",
                newName: "InspectionComplianceRate");

            migrationBuilder.RenameColumn(
                name: "RoutineCheckLevel",
                table: "ServiceLevelAgreements",
                newName: "InspectionComplianceLevel");
        }
    }
}
