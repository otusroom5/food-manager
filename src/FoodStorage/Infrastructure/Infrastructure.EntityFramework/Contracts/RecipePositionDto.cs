using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodStorage.Infrastructure.EntityFramework.Contracts;

[Table("recipe_position")]
[PrimaryKey(nameof(RecipeId), nameof(ProductId))] // Составной первичный ключ: идентификатор рецепта + идентификатор продукта
public sealed record RecipePositionDto
{
    [Column("recipe_id")]
    [Required]
    public Guid RecipeId { get; set; }

    [Column("product_id")]
    [Required]
    public Guid ProductId { get; set; }

    [Column("product_count")]
    [Required]
    public double ProductCount { get; set; }

    [Column("unit_id")]
    [Required]
    public string UnitId { get; set; }
}
