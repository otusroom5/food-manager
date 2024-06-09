using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodUserNotifier.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "recepient",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RoleEnum = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Telegram = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    RecepientDTOId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("recepientid", x => x.Id);
                    table.ForeignKey(
                        name: "FK_recepient_recepient_RecepientDTOId",
                        column: x => x.RecepientDTOId,
                        principalTable: "recepient",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_recepient_RecepientDTOId",
                table: "recepient",
                column: "RecepientDTOId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "recepient");
        }
    }
}
