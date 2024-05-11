using FoodUserAuth.BusinessLogic.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace FoodUserAuth.BusinessLogic.Utils;

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
        return String.Equals(hashedPassword, newHashed);
    }
}
