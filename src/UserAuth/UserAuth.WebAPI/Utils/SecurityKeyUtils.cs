using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace UserAuth.WebApi.Utils
{
    public static class SecurityKeyUtils
    {
        /// <summary>
        /// This method generates symmetric security key from key
        /// </summary>
        /// <param name="key"></param>
        /// <returns>SecurityKey</returns>
        public static SecurityKey CreateSymmetricSecurityKey(string key)
        {
            byte[] password = Encoding.ASCII.GetBytes(key);
            return new SymmetricSecurityKey(password);
        }
    }
}
