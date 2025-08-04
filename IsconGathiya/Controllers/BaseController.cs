using Microsoft.AspNetCore.Mvc;
using QRCoder;
using System.Security.Cryptography;
using System.Text;

namespace IsconGathiya.Controllers
{
    public class BaseController : Controller
    {
        public static string GetMenuName(string key)
        {
            var start = key.IndexOf('[') + 1;
            var end = key.IndexOf(']');
            return key.Substring(start, end - start);
        }

        public static string GetAccessType(string key)
        {
            var lastDot = key.LastIndexOf('.');
            return key.Substring(lastDot + 1);
        }


        private static Random random = new Random();
        /// <summary>
        ///  Create Random String
        /// </summary>
        public static string GetRandomPassword(int length)
        {
            Random random = new Random();
            const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$";

            return new string(Enumerable.Repeat(chars, length)
                                         .Select(s => s[random.Next(s.Length)])
                                         .ToArray());
        }

        /// <summary>
        /// 
        /// </summary>
        public static string GenerateTwoFactorKey(string email)
        {
            // Combine email and a random number to generate a unique key
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ234567"; // Base32 characters
            return new string(Enumerable.Repeat(chars, 10).Select(s => s[random.Next(s.Length)]).ToArray());

        }

        public static string GenerateTwoFactorKeyWithSHA512(string email)
        {
            // Combine email and a random number to generate a unique base string
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ234567"; // Base32 characters
            Random random = new Random();
            string randomKey = new string(Enumerable.Repeat(chars, 10).Select(s => s[random.Next(s.Length)]).ToArray());

            // Concatenate email and random key for hashing
            string keyToHash = email + randomKey;

            // Generate SHA512 hash
            using (SHA512 sha512 = SHA512.Create())
            {
                byte[] hashBytes = sha512.ComputeHash(Encoding.UTF8.GetBytes(keyToHash));
                // Convert the hash to a base32 encoded string
                string base32Hash = ToBase32String(hashBytes);
                return base32Hash;
            }
        }

        // Helper method to convert byte array to Base32 string
        private static string ToBase32String(byte[] bytes)
        {
            const string base32Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ234567";
            StringBuilder result = new StringBuilder();
            int buffer = bytes[0], nextBits = 8, count = 1;

            for (int i = 1; i < bytes.Length; i++)
            {
                buffer = (buffer << 8) | bytes[i];
                nextBits += 8;
                while (nextBits >= 5)
                {
                    nextBits -= 5;
                    result.Append(base32Chars[(buffer >> nextBits) & 31]);
                }
            }

            if (nextBits > 0)
            {
                result.Append(base32Chars[(buffer << (5 - nextBits)) & 31]);
            }

            return result.ToString();
        }
        /// <summary>
        /// 
        /// </summary>
        public string GenerateQrCodeUri(string email, string key)
        {
            var qrGenerator = new QRCodeGenerator();
            var qrCodeData = qrGenerator.CreateQrCode($"otpauth://totp/{email}?secret={key}&issuer=Housmart", QRCodeGenerator.ECCLevel.Q);
            var qrCode = new PngByteQRCode(qrCodeData);
            var qrCodeBytes = qrCode.GetGraphic(20);
            return $"data:image/png;base64,{Convert.ToBase64String(qrCodeBytes)}";
        }
        public byte[] GenerateQrCodeAsByteArray(string email, string key)
        {
            var qrGenerator = new QRCodeGenerator();
            var qrCodeData = qrGenerator.CreateQrCode($"otpauth://totp/{email}?secret={key}&issuer=Housmart", QRCodeGenerator.ECCLevel.Q);
            var qrCode = new PngByteQRCode(qrCodeData);
            return qrCode.GetGraphic(20);
        }

        /// <summary>
        /// Sweet alert - error message
        /// </summary>
        /// <param name="message">Message</param>
        protected virtual void AddSweetAlertErrorPopup(string message)
        {
            string dataKey = string.Format("Housmart.sweet.error.alert.popup");
            TempData[dataKey] = message;
        }

        /// <summary>
        /// Sweet alert - success message
        /// </summary>
        /// <param name="message">Message</param>
        protected virtual void AddSweetAlertSuccessPopup(string message)
        {
            string dataKey = string.Format("Housmart.sweet.success.alert.popup");
            TempData[dataKey] = message;
        }

        /// <summary>
        /// Sweet alert - warning message
        /// </summary>
        /// <param name="message">Message</param>
        protected virtual void AddSweetAlertWarningPopup(string message)
        {
            string dataKey = string.Format("Housmart.sweet.warning.alert.popup");
            TempData[dataKey] = message;
        }

        /// <summary>
        /// Gets the content of a mail template file.
        /// </summary>
        /// <param name="templateFileName">The file name of the template.</param>
        /// <returns>The content of the mail template.</returns>
        public static string GetMailTemplate(string path, string templateFileName)
        {
            // Initialize an empty string to store the mail template content
            string strMailTemplet = string.Empty;

            // Read the contents of the mail template file
            using (StreamReader sr = new StreamReader("wwwroot/assets/emailtemplate/" + path + "/" + templateFileName))
            {
                // Read each line of the file until the end
                string sLine;
                while ((sLine = sr.ReadLine()) != null)
                {
                    // Append the line to the mail template content
                    strMailTemplet += sLine;
                }
            }
            // Return the mail template content
            return strMailTemplet;
        }

        public static string GetMailTemplate(string templateFileName)
        {
            // Initialize an empty string to store the mail template content
            string strMailTemplet = string.Empty;

            // Read the contents of the mail template file
            using (StreamReader sr = new StreamReader("wwwroot/EmailTemplates/" + templateFileName))
            {
                // Read each line of the file until the end
                string sLine;
                while ((sLine = sr.ReadLine()) != null)
                {
                    // Append the line to the mail template content
                    strMailTemplet += sLine;
                }
            }

            // Return the mail template content
            return strMailTemplet;
        }
    }

}
