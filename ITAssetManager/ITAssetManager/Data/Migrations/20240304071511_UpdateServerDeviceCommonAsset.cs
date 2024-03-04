using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ITAssetManager.Migrations
{
    /// <inheritdoc />
    public partial class UpdateServerDeviceCommonAsset : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServerDevices_CommonAssets_CommonAssetsId",
                table: "ServerDevices");

            migrationBuilder.DropIndex(
                name: "IX_ServerDevices_CommonAssetsId",
                table: "ServerDevices");

            migrationBuilder.RenameColumn(
                name: "CommonAssetsId",
                table: "ServerDevices",
                newName: "CommonAssetId");

            migrationBuilder.CreateIndex(
                name: "IX_ServerDevices_CommonAssetId",
                table: "ServerDevices",
                column: "CommonAssetId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ServerDevices_CommonAssets_CommonAssetId",
                table: "ServerDevices",
                column: "CommonAssetId",
                principalTable: "CommonAssets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServerDevices_CommonAssets_CommonAssetId",
                table: "ServerDevices");

            migrationBuilder.DropIndex(
                name: "IX_ServerDevices_CommonAssetId",
                table: "ServerDevices");

            migrationBuilder.RenameColumn(
                name: "CommonAssetId",
                table: "ServerDevices",
                newName: "CommonAssetsId");

            migrationBuilder.CreateIndex(
                name: "IX_ServerDevices_CommonAssetsId",
                table: "ServerDevices",
                column: "CommonAssetsId");

            migrationBuilder.AddForeignKey(
                name: "FK_ServerDevices_CommonAssets_CommonAssetsId",
                table: "ServerDevices",
                column: "CommonAssetsId",
                principalTable: "CommonAssets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
