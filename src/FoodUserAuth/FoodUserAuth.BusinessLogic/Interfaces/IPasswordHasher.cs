using System.Security.Cryptography;
using System.Text;

namespace FoodUserAuth.BusinessLogic.Services
{
    public partial class UserVerificationService
    {
        public interface IPasswordHasher
        {
            string ComputeHash(string password);
            bool VerifyHash(string password, string hashedPassword);
        }
    }
}
