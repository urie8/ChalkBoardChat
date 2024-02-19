using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChalkboardChat.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MessageModel_AspNetUsers_UserId1",
                table: "MessageModel");

            migrationBuilder.DropIndex(
                name: "IX_MessageModel_UserId1",
                table: "MessageModel");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "MessageModel");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "MessageModel",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_MessageModel_UserId",
                table: "MessageModel",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_MessageModel_AspNetUsers_UserId",
                table: "MessageModel",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MessageModel_AspNetUsers_UserId",
                table: "MessageModel");

            migrationBuilder.DropIndex(
                name: "IX_MessageModel_UserId",
                table: "MessageModel");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "MessageModel",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "MessageModel",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_MessageModel_UserId1",
                table: "MessageModel",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_MessageModel_AspNetUsers_UserId1",
                table: "MessageModel",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
