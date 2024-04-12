using FoodUserAuth.DataAccess.Types;

namespace FoodUserAuth.DataAccess.Entities
{
    public class User
    {
        public Guid Id { get; set; }

        public string LoginName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public UserState State { get; set; }

        public string Password { get; set; }

    }
}
