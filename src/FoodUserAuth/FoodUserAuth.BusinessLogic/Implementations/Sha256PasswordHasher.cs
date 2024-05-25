using FoodUserAuth.BusinessLogic.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace FoodUserAuth.BusinessLogic.Implementations;

public class Sha256PasswordHasher : IPasswordHasher
{
    private const string PasswordSalt = "!FoOdAuTh1";

    public string ComputeHash(string password)
    {
        byte[] passWithSalt = Encoding.UTF8.GetBytes(password + PasswordSalt);
        SHA256 sha256 = SHA256.Create();
        byte[] hash = sha256.ComputeHash(passWithSalt);
        return Convert.ToBase64String(hash);
    }

    public bool VerifyHash(string password, string hashedPassword)
    {
        string newHashed = ComputeHash(password);
        return string.Equals(hashedPassword, newHashed);
    }
}
