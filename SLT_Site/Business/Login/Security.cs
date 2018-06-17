using System.Security.Cryptography;
using System.Text;

namespace Business.Login
{
    public static class Security
    {
        public static string CreateHash(string unHashed)
        {
           MD5CryptoServiceProvider x = new MD5CryptoServiceProvider();
            byte[] data = Encoding.ASCII.GetBytes(unHashed);
            data = x.ComputeHash(data);
            return Encoding.ASCII.GetString(data);
        }

        public static bool MatchHash(string hashedPassword, string unHashedPassword)
        {
            unHashedPassword = CreateHash(unHashedPassword);
            if (unHashedPassword == hashedPassword)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
