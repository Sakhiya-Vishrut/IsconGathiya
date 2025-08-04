using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace IsconGathiya.Common
{
    public class PasswordGenerator
    {
        public static string GetMD5(string str)
        {
            string strReturn = "";
            byte[] data = System.Text.Encoding.Unicode.GetBytes(str);
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] result = md5.ComputeHash(data);
            strReturn = BitConverter.ToString(result).Replace("-", "");
            return strReturn;
        }

        public static string GetSHA512(string str)
        {
            string strReturn = string.Empty;
            byte[] data = System.Text.Encoding.Unicode.GetBytes(str);
            SHA512 sHA512 = new SHA512CryptoServiceProvider();
            byte[] result = sHA512.ComputeHash(data);
            strReturn = BitConverter.ToString(result).Replace("-", "");
            return strReturn;
        }
    }
}
