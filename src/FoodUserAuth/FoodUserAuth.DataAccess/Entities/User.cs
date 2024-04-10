using FoodUserAuth.DataAccess.Types;

namespace FoodUserAuth.DataAccess.Entities
{
    public class User
    {
        public Guid Id { get; set; }

        public string UserName { get; set; } = null!;

        public string FullName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public UserState State { get; set; }

        public string Password { get; set; } = null!;

    }
}
