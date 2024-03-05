using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ITAssetManager.Migrations
{
    /// <inheritdoc />
    public partial class AddServersVersion2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServerDevices_CommonAssets_CommonAssetId",
                table: "ServerDevices");

            migrationBuilder.RenameColumn(
                name: "CommonAssetId",
                table: "ServerDevices",
                newName: "ServerId");

            migrationBuilder.RenameIndex(
                name: "IX_ServerDevices_CommonAssetId",
                table: "ServerDevices",
                newName: "IX_ServerDevices_ServerId");

            migrationBuilder.AddColumn<byte[]>(
                name: "Version",
                table: "ServerDevices",
                type: "rowversion",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.CreateTable(
                name: "Servers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Version = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    CommonAssetId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Servers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Servers_CommonAssets_CommonAssetId",
                        column: x => x.CommonAssetId,
                        principalTable: "CommonAssets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Servers_CommonAssetId",
                table: "Servers",
                column: "CommonAssetId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ServerDevices_Servers_ServerId",
                table: "ServerDevices",
                column: "ServerId",
                principalTable: "Servers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServerDevices_Servers_ServerId",
                table: "ServerDevices");

            migrationBuilder.DropTable(
                name: "Servers");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "ServerDevices");

            migrationBuilder.RenameColumn(
                name: "ServerId",
                table: "ServerDevices",
                newName: "CommonAssetId");

            migrationBuilder.RenameIndex(
                name: "IX_ServerDevices_ServerId",
                table: "ServerDevices",
                newName: "IX_ServerDevices_CommonAssetId");

            migrationBuilder.AddForeignKey(
                name: "FK_ServerDevices_CommonAssets_CommonAssetId",
                table: "ServerDevices",
                column: "CommonAssetId",
                principalTable: "CommonAssets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
