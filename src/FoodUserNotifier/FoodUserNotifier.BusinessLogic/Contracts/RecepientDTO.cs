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
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string RoleEnum { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Telegram { get; set; }

    }
}
