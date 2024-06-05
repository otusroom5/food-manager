using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodUserAuth.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddUserContactsReleationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_UserContacts_UserId",
                table: "UserContacts",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserContacts_Users_UserId",
                table: "UserContacts",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserContacts_Users_UserId",
                table: "UserContacts");

            migrationBuilder.DropIndex(
                name: "IX_UserContacts_UserId",
                table: "UserContacts");
        }
    }
}
