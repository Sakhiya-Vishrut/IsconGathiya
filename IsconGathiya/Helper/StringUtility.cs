using System.Security.Cryptography;
using System.Text;

namespace IsconGathiya.Helper
{
	public static class StringUtility
	{
		public static string Encode(this int userId)
		{
			if (userId == default)
			{
				throw new ArgumentNullException(nameof(userId), "userId cannot be null");
			}

			byte[] userIdBytes = BitConverter.GetBytes(userId);
			return Convert.ToBase64String(userIdBytes);
		}

		public static int? Decode(this string encodedUserId)
		{
			if (string.IsNullOrEmpty(encodedUserId))
			{
				throw new ArgumentNullException(nameof(encodedUserId), "encodedUserId cannot be null or empty");
			}
			try
			{
				byte[] userIdBytes = Convert.FromBase64String(encodedUserId);
				return BitConverter.ToInt32(userIdBytes, 0);
			}
			catch (FormatException ex)
			{
				return null;
			}
		}

		public static string key = "b14ca5898a4e4133bbce2ea2315a1916";
		/// <summary>
		/// Not Decode By Base64 online
		/// </summary>
		/// <param name="plainText"></param>
		/// <returns></returns>
		public static string EncryptStringStong(string plainText)
		{
			byte[] iv = new byte[16];
			byte[] array;

			using (Aes aes = Aes.Create())
			{
				aes.Key = Encoding.UTF8.GetBytes(key);
				aes.IV = iv;

				ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

				using (MemoryStream memoryStream = new MemoryStream())
				{
					using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, encryptor, CryptoStreamMode.Write))
					{
						using (StreamWriter streamWriter = new StreamWriter((Stream)cryptoStream))
						{
							streamWriter.Write(plainText);
						}

						array = memoryStream.ToArray();
					}
				}
			}

			return Convert.ToBase64String(array);
		}

		public static string DecryptStringStrong(string cipherText)
		{
			byte[] iv = new byte[16];
			byte[] buffer = Convert.FromBase64String(cipherText);

			using (Aes aes = Aes.Create())
			{
				aes.Key = Encoding.UTF8.GetBytes(key);
				aes.IV = iv;
				ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

				using (MemoryStream memoryStream = new MemoryStream(buffer))
				{
					using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, decryptor, CryptoStreamMode.Read))
					{
						using (StreamReader streamReader = new StreamReader((Stream)cryptoStream))
						{
							return streamReader.ReadToEnd();
						}
					}
				}
			}
		}

        /// <summary>
        /// Decrypts a base64-encoded string.
        /// </summary>
        /// <param name="encrString">The base64-encoded string to decrypt.</param>
        /// <returns>The decrypted string.</returns>
        public static string DecryptString(string encrString)
        {
            byte[] b;
            string decrypted;
            try
            {
                if (encrString == null || encrString == "")
                {
                    return "";
                }
                b = Convert.FromBase64String(encrString);
                decrypted = System.Text.ASCIIEncoding.ASCII.GetString(b);
            }
            catch (FormatException)
            {
                decrypted = "";
            }
            return decrypted;
        }

        /// <summary>
        /// Encrypts a string to base64 format.
        /// </summary>
        /// <param name="strEncrypted">The string to encrypt.</param>
        /// <returns>The base64-encoded encrypted string.</returns>
        public static string EncryptString(string strEncrypted)
        {
            string encrypted;
            try
            {
				if(strEncrypted == null || strEncrypted == "")
				{
                    return "";
                }
                byte[] b = System.Text.ASCIIEncoding.ASCII.GetBytes(strEncrypted);
                encrypted = Convert.ToBase64String(b);
            }
            catch (FormatException)
            {
                encrypted = "";
            }
            return encrypted;
        }
    }
}
