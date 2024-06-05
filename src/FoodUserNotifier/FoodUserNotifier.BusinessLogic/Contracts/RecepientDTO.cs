using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodUserNotifier.BusinessLogic.Contracts
{
    [Table("recepient")]
    public class RecepientDTO
    {
        [Key, Column("id")]
        public Guid Id { get; set; }

        [Column("role"), MaxLength(100)]
        [Required]
        public string RoleEnum { get; set; }

        [Column("email"), MaxLength(100)]
        [Required]
        public string Email { get; set; }

        [Column("telegram"), MaxLength(100)]
        [Required]
        public string Telegram { get; set; }


    }
}
