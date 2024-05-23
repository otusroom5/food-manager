using System.ComponentModel.DataAnnotations;

namespace FoodSupplier.WebAPI.Models;

public class ShopModel
{
    /// <summary>
    /// Shop Id
    /// </summary>
    [Required]
    public Guid Id { get; set; }

    /// <summary>
    /// Shop name
    /// </summary>
    [Required]
    public string Name { get; set; }

    /// <summary>
    /// Shop activity flag
    /// </summary>
    [Required]
    public bool IsActive { get; set; }

    /// <summary>
    /// Shop base Url
    /// </summary>
    [Required]
    public string BaseUrl { get; set; }
}