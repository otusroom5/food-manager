using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodStorage.Infrastructure.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class AddedUnit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "unit",
                table: "product",
                newName: "unit_type");

            migrationBuilder.AddColumn<string>(
                name: "unit_id",
                table: "recipe_position",
                type: "character varying(4)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<double>(
                name: "amount",
                table: "product_item",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<double>(
                name: "count",
                table: "product_history",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.CreateTable(
                name: "unit",
                columns: table => new
                {
                    id = table.Column<string>(type: "character varying(4)", maxLength: 4, nullable: false),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    unit_type = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    coefficient = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_unit", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_recipe_position_unit_id",
                table: "recipe_position",
                column: "unit_id");

            migrationBuilder.AddForeignKey(
                name: "fk_recipeposition_unitid",
                table: "recipe_position",
                column: "unit_id",
                principalTable: "unit",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_recipeposition_unitid",
                table: "recipe_position");

            migrationBuilder.DropTable(
                name: "unit");

            migrationBuilder.DropIndex(
                name: "IX_recipe_position_unit_id",
                table: "recipe_position");

            migrationBuilder.DropColumn(
                name: "unit_id",
                table: "recipe_position");

            migrationBuilder.RenameColumn(
                name: "unit_type",
                table: "product",
                newName: "unit");

            migrationBuilder.AlterColumn<int>(
                name: "amount",
                table: "product_item",
                type: "integer",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<int>(
                name: "count",
                table: "product_history",
                type: "integer",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");
        }
    }
}
