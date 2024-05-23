using System.ComponentModel.DataAnnotations;

namespace FoodSupplier.WebAPI.Models;

public class PriceEntryGetModel
{
    /// <summary>
    /// Price entry Id
    /// </summary>
    [Required]
    public Guid Id { get; set; }
}