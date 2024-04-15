using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodStorage.Infrastructure.EntityFramework.Contracts;

[Table("product_item")]
public sealed record ProductItemDto
{
    [Key, Column("id")]
    public Guid Id { get; set; }

    [Column("product_id")]
    [Required]
    public Guid ProductId { get; set; }

    [Column("amount")]
    [Required]
    public int Amount { get; set; }

    [Column("creating_date")]
    [Required]
    public DateTime CreatingDate { get; set; }

    // Navigation property
    public ProductDto Product { get; set; }
}
