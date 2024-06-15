using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodUserNotifier.Infrastructure.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "deliveryreport",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    NotificationId = table.Column<Guid>(type: "uuid", maxLength: 50, nullable: false),
                    Message = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    Success = table.Column<bool>(type: "boolean", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("deliveryreport", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "recepient",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RecepientId = table.Column<Guid>(type: "uuid", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Telegram = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("recepientid", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "deliveryreport");

            migrationBuilder.DropTable(
                name: "recepient");
        }
    }
}
