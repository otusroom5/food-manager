using System.Security.Cryptography;
using System.Text;
using static FoodUserAuth.BusinessLogic.Services.UserVerificationService;

namespace FoodUserAuth.BusinessLogic.Utils
{
    public class MD5PasswordHasher : IPasswordHasher
    {
        public string ComputeHash(string password)
        {
            var hasher = MD5.Create();
            byte[] hash = hasher.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hash);
        }

        public bool VerifyHash(string password, string hashedPassword)
        {
            string newHashed = ComputeHash(password);
            return String.Equals(password, newHashed, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
