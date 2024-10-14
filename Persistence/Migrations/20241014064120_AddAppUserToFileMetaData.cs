using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddAppUserToFileMetaData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "Files",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Files_AppUserId",
                table: "Files",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Files_AspNetUsers_AppUserId",
                table: "Files",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Files_AspNetUsers_AppUserId",
                table: "Files");

            migrationBuilder.DropIndex(
                name: "IX_Files_AppUserId",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Files");
        }
    }
}
