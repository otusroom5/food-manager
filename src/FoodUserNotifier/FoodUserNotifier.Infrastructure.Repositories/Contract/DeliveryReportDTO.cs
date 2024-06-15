using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodUserNotifier.Infrastructure.Repositories.Contract
{
    [Table("deliveryreport")]
    public record DeliveryReportDTO
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public Guid NotificationId { get; set; }
        [Required]
        public string Message { get; set; }
        [Required]
        public bool Success { get; set; }
    }
}
