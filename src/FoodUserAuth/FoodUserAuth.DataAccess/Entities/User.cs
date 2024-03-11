namespace FoodUserAuth.DataAccess.Entities
{
    public class User
    {
        public Guid Id { get; set; }

        public string Password { get; set; } = null!;
    }
}
