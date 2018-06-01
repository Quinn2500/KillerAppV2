using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Login
{
    public static class Security
    {
        public static string CreateHash(string unHashed)
        {
            System.Security.Cryptography.MD5CryptoServiceProvider x = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] data = System.Text.Encoding.ASCII.GetBytes(unHashed);
            data = x.ComputeHash(data);
            return System.Text.Encoding.ASCII.GetString(data);
        }

        public static bool MatchHash(string HashedPassword, string UnHashesPassword)
        {
            UnHashesPassword = CreateHash(UnHashesPassword);
            if (UnHashesPassword == HashedPassword)
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
