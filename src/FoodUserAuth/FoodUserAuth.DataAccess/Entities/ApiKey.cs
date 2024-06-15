using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodUserAuth.DataAccess.Entities;

public sealed class ApiKey
{
    public Guid Id { get; set; }
    public string Token { get; set; }

    [Column(TypeName = "text")]
    public DateTime ExpiryDate {  get; set; }
}
