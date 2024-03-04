using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ITAssetManager.Migrations
{
    /// <inheritdoc />
    public partial class AddServerDevice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ServerDevices_CommonAssetId",
                table: "ServerDevices");

            migrationBuilder.DropColumn(
                name: "Manufacturer",
                table: "CommonAssets");

            migrationBuilder.DropColumn(
                name: "Model",
                table: "CommonAssets");

            migrationBuilder.AddColumn<string>(
                name: "Manufacturer",
                table: "ServerDevices",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Model",
                table: "ServerDevices",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_ServerDevices_CommonAssetId",
                table: "ServerDevices",
                column: "CommonAssetId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ServerDevices_CommonAssetId",
                table: "ServerDevices");

            migrationBuilder.DropColumn(
                name: "Manufacturer",
                table: "ServerDevices");

            migrationBuilder.DropColumn(
                name: "Model",
                table: "ServerDevices");

            migrationBuilder.AddColumn<string>(
                name: "Manufacturer",
                table: "CommonAssets",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Model",
                table: "CommonAssets",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_ServerDevices_CommonAssetId",
                table: "ServerDevices",
                column: "CommonAssetId",
                unique: true);
        }
    }
}
