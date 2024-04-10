using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodStorage.Infrastructure.EntityFramework.Contracts;

[Table("product_history")]
public sealed record ProductHistoryDto
{
    [Key, Column("id")]
    public Guid Id { get; set; }

    [Column("product_id")]
    [Required]
    public Guid ProductId { get; set; }

    [Column("state"), MaxLength(20)]
    [Required]
    public string State { get; set; }

    [Column("count")]
    [Required]
    public int Count { get; set; }

    [Column("created_by")]
    [Required]
    public Guid CreatedBy { get; set; }

    [Column("created_at")]
    [Required]
    public DateTime CreatedAt { get; set; }
}
