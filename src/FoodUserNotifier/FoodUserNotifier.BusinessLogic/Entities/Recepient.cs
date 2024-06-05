using FoodUserNotifier.BusinessLogic.Contracts;
using FoodUserNotifier.BusinessLogic.Types;

namespace FoodUserNotifier.BusinessLogic.Entities
{
    public class Recepient
    {
        public Role RoleEnum { get; set; }
        public string Email { get; set; }
        public string Telegram { get; set; }

        public RecepientDTO ToDTO(Recepient recepient)
        {
            return new RecepientDTO() { RoleEnum = recepient.RoleEnum.ToString(), Email = recepient.Email, Telegram = recepient.Telegram };
        }

    }
}
