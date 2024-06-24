using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodStorage.Infrastructure.EntityFramework.Contracts;

[Table("product")]
public sealed record ProductDto
{
    [Key, Column("id")]
    public Guid Id { get; set; }

    [Column("name"), MaxLength(100)]
    [Required]
    public string Name { get; set; }

    [Column("unit_type"), MaxLength(20)]
    [Required]
    public string UnitType { get; set; }

    [Column("min_amount_per_day")]
    [Required]
    public int MinAmountPerDay { get; set; }

    [Column("best_before_date")]
    [Required]
    public double BestBeforeDate { get; set; }

    // Navigation property
    public ICollection<ProductItemDto> ProductItems { get; set; }
}
