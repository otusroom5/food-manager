using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodStorage.Infrastructure.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class DeleteCascade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_producthistory_productid",
                table: "product_history");

            migrationBuilder.DropForeignKey(
                name: "fk_productitem_productid",
                table: "product_item");

            migrationBuilder.DropForeignKey(
                name: "fk_recipeposition_productid",
                table: "recipe_position");

            migrationBuilder.DropForeignKey(
                name: "fk_recipeposition_unitid",
                table: "recipe_position");

            migrationBuilder.AddForeignKey(
                name: "fk_producthistory_productid",
                table: "product_history",
                column: "product_id",
                principalTable: "product",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_productitem_productid",
                table: "product_item",
                column: "product_id",
                principalTable: "product",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_recipeposition_productid",
                table: "recipe_position",
                column: "product_id",
                principalTable: "product",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_recipeposition_unitid",
                table: "recipe_position",
                column: "unit_id",
                principalTable: "unit",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_producthistory_productid",
                table: "product_history");

            migrationBuilder.DropForeignKey(
                name: "fk_productitem_productid",
                table: "product_item");

            migrationBuilder.DropForeignKey(
                name: "fk_recipeposition_productid",
                table: "recipe_position");

            migrationBuilder.DropForeignKey(
                name: "fk_recipeposition_unitid",
                table: "recipe_position");

            migrationBuilder.AddForeignKey(
                name: "fk_producthistory_productid",
                table: "product_history",
                column: "product_id",
                principalTable: "product",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_productitem_productid",
                table: "product_item",
                column: "product_id",
                principalTable: "product",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_recipeposition_productid",
                table: "recipe_position",
                column: "product_id",
                principalTable: "product",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_recipeposition_unitid",
                table: "recipe_position",
                column: "unit_id",
                principalTable: "unit",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
