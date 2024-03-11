using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ITAssetManager.Migrations
{
    /// <inheritdoc />
    public partial class AddCommonAssetOnSiteManager : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Category",
                table: "CommonAssets",
                newName: "OnSiteManagerPhone");

            migrationBuilder.AddColumn<string>(
                name: "OnSiteManager",
                table: "CommonAssets",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OnSiteManager",
                table: "CommonAssets");

            migrationBuilder.RenameColumn(
                name: "OnSiteManagerPhone",
                table: "CommonAssets",
                newName: "Category");
        }
    }
}
