using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodStorage.Infrastructure.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class ProductCountIsDouble : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "product_count",
                table: "recipe_position",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "product_count",
                table: "recipe_position",
                type: "integer",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");
        }
    }
}
