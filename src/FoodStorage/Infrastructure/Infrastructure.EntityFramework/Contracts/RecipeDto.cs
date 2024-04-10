using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodStorage.Infrastructure.EntityFramework.Contracts;

[Table("recipe")]
public sealed record RecipeDto
{
    [Key, Column("id")]
    public Guid Id { get; set; }

    [Column("name"), MaxLength(100)]
    [Required]
    public string Name { get; set; }

    // Navigation property
    public ICollection<RecipePositionDto> Positions { get; set; }
}
