using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodUserNotifier.DataAcess.Infrastructure.EntityFramework.Contracts
{
    [Table("recepient")]
    public record RecepientDTO
    {
        [Key, Column("id")]
        public Guid Id { get; set; }

        [Column("role"), MaxLength(100)]
        [Required]
        public string RoleEnum { get; set; }

        [Column("adress"), MaxLength(100)]
        [Required]
        public string Address { get; set; }
    }
}
