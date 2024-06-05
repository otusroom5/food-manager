using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodUserAuth.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class RemovedUnusedEmailColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Users");

            migrationBuilder.AlterColumn<int>(
                name: "ContactType",
                table: "UserContacts",
                type: "integer",
                nullable: false,
                oldClrType: typeof(byte),
                oldType: "smallint");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<byte>(
                name: "ContactType",
                table: "UserContacts",
                type: "smallint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");
        }
    }
}
