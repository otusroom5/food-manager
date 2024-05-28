using System.ComponentModel.DataAnnotations;

namespace FoodSupplier.WebAPI.Models;

public class ProductGetModel
{
    /// <summary>
    /// Product Id
    /// </summary>
    [Required]
    public Guid Id { get; set; }
}