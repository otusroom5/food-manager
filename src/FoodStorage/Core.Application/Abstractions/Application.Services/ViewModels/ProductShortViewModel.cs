using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodStorage.Application.Services.ViewModels;

/// <summary>
/// Укороченная модель продукта
/// </summary>
public sealed record ProductShortViewModel
{
    /// <summary>
    /// Идентификатор продукта
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Наименование продукта
    /// </summary>
    public string Name { get; set; }
}
