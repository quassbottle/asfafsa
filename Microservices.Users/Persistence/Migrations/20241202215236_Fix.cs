using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Microservices.Users.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Fix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserLanguageEntity_Users_UserId",
                table: "UserLanguageEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserLanguageEntity",
                table: "UserLanguageEntity");

            migrationBuilder.RenameTable(
                name: "UserLanguageEntity",
                newName: "UserLanguages");

            migrationBuilder.RenameIndex(
                name: "IX_UserLanguageEntity_UserId",
                table: "UserLanguages",
                newName: "IX_UserLanguages_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserLanguages",
                table: "UserLanguages",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserLanguages_Users_UserId",
                table: "UserLanguages",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserLanguages_Users_UserId",
                table: "UserLanguages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserLanguages",
                table: "UserLanguages");

            migrationBuilder.RenameTable(
                name: "UserLanguages",
                newName: "UserLanguageEntity");

            migrationBuilder.RenameIndex(
                name: "IX_UserLanguages_UserId",
                table: "UserLanguageEntity",
                newName: "IX_UserLanguageEntity_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserLanguageEntity",
                table: "UserLanguageEntity",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserLanguageEntity_Users_UserId",
                table: "UserLanguageEntity",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
