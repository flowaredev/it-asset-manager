using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ITAssetManager.Migrations
{
    /// <inheritdoc />
    public partial class AddForeignKeys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ServerDevices_CommonAssetId",
                table: "ServerDevices",
                column: "CommonAssetId");

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
        }
    }
}
