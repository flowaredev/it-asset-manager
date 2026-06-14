using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ITAssetManager.Migrations
{
    /// <inheritdoc />
    public partial class MakeOperationDocumentCommentDependent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OperationDocumentId",
                table: "OperationDocumentComments",
                type: "int",
                nullable: true);

            migrationBuilder.Sql(@"
                UPDATE OperationDocumentComments c
                JOIN (
                    SELECT c2.Id AS CommentId, d.Id AS DocumentId
                    FROM OperationDocumentComments c2
                    JOIN OperationDocuments d ON d.DocumentType = c2.DocumentType
                    LEFT JOIN OperationDocuments newer ON newer.DocumentType = c2.DocumentType
                        AND (newer.UpdatedAt > d.UpdatedAt OR (newer.UpdatedAt = d.UpdatedAt AND newer.Id > d.Id))
                    WHERE newer.Id IS NULL
                ) mapped ON mapped.CommentId = c.Id
                SET c.OperationDocumentId = mapped.DocumentId;
            ");

            migrationBuilder.Sql("DELETE FROM OperationDocumentComments WHERE OperationDocumentId IS NULL;");

            migrationBuilder.DropColumn(
                name: "DocumentType",
                table: "OperationDocumentComments");

            migrationBuilder.AlterColumn<int>(
                name: "OperationDocumentId",
                table: "OperationDocumentComments",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OperationDocumentComments_OperationDocumentId",
                table: "OperationDocumentComments",
                column: "OperationDocumentId");

            migrationBuilder.AddForeignKey(
                name: "FK_OperationDocumentComments_OperationDocuments_OperationDocumentId",
                table: "OperationDocumentComments",
                column: "OperationDocumentId",
                principalTable: "OperationDocuments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OperationDocumentComments_OperationDocuments_OperationDocumentId",
                table: "OperationDocumentComments");

            migrationBuilder.DropIndex(
                name: "IX_OperationDocumentComments_OperationDocumentId",
                table: "OperationDocumentComments");

            migrationBuilder.AddColumn<string>(
                name: "DocumentType",
                table: "OperationDocumentComments",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.Sql(@"
                UPDATE OperationDocumentComments c
                JOIN OperationDocuments d ON d.Id = c.OperationDocumentId
                SET c.DocumentType = d.DocumentType;
            ");

            migrationBuilder.DropColumn(
                name: "OperationDocumentId",
                table: "OperationDocumentComments");
        }
    }
}
