using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodStorage.Infrastructure.EntityFramework.Contracts;

[Table("unit")]
public sealed record UnitDto
{
    [Key, Column("id"), MaxLength(4)]
    public string Id { get; set; }

    [Column("name"), MaxLength(100)]
    [Required]
    public string Name { get; set; }

    [Column("unit_type"), MaxLength(20)]
    [Required]
    public string UnitType { get; set; }

    [Column("coefficient")]
    [Required]
    public double Coefficient { get; set; }
}