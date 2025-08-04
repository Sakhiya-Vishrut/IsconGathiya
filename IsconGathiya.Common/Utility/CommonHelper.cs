using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using OtpNet;
using System.ComponentModel;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace IsconGathiya.Common.Utility
{
    public static class CommonHelper
    {
        public static string FormatFileName(string fileNameWithSpaces)
        {
            if (string.IsNullOrWhiteSpace(fileNameWithSpaces))
                return string.Empty;

            return fileNameWithSpaces.Replace(" ", "_");
        }
        public static decimal? StringToDecimal(string numberString)
        {
            if (!string.IsNullOrEmpty(numberString))
            {
                if (decimal.TryParse(numberString, out decimal d))
                    return d;
                return null;
            }
            return null;
        }

        public static float? StringToDecimalNull(string numberString)
        {
            if (!string.IsNullOrEmpty(numberString))
            {
                if (float.TryParse(numberString, out float d))
                    return d;
                return null;
            }
            return null;
        }

        //public static string GetRandomPassword(int character)
        //{
        //    string randomPassword = GetRandomPassword(8);
        //    var hasher = new PasswordHasher<string>();
        //    string passwordHash = hasher.HashPassword(null, randomPassword);
        //    return passwordHash;
        //}
        public static string GetRandomPassword(int length)
        {
            Random random = new Random();
            const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$";

            return new string(Enumerable.Repeat(chars, length)
                                         .Select(s => s[random.Next(s.Length)])
                                         .ToArray());
        }

        public static (string RandomPassword, string PasswordHash) GetPassword(int character)
        {
            // Generate a random password with the specified length
            string randomPassword = GetRandomPassword(character);

            // Hash the generated password
            var hasher = new PasswordHasher<string>();
            string passwordHash = hasher.HashPassword(null, randomPassword);

            // Return both the plain password and the hash
            return (randomPassword, passwordHash);
        }

        public static bool IsValidEmail(string email)
        {
            return Regex.IsMatch(email, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
        }

        public static bool IsValidPhoneNumber(string phoneNumber)
        {
            var IsValid = Regex.IsMatch(phoneNumber, @"^\d{10}$");
            return IsValid;
        }

        public static string? DecimalToString(decimal? number)
        {
            string? value = null;

            if (number != 0)
            {
                value = number?.ToString("n2", new CultureInfo("en-US"));
            }

            return value;
        }

        public static string DisplayDateTime(DateTime dateTime, bool isDisplayTime = false)
        {
            if (dateTime == DateTime.MinValue)
                return "";

            if (isDisplayTime)
                return dateTime.ToString("MM-dd-yyyy hh:mm tt", CultureInfo.InvariantCulture);

            return dateTime.ToString("MM-dd-yyyy", CultureInfo.InvariantCulture);
        }

        public static string GetTemplatefromEnum(int templateEnum)
        {
            if (templateEnum == Enums.EmailSmsTemplate.Registartion.GetHashCode())
            {
                return Convert.ToString(ConfigItems.RegisterOTPTemplate);
            }
            else
            {
                return String.Empty;
            }
        }

        public static string CalculateSignature(string timestampString)
        {
            string data = $"{timestampString}-{"IsconGathiyaSecretKey"}";
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(data);
            byte[] hashBytes = new System.Security.Cryptography.SHA256Managed().ComputeHash(bytes);
            string hashString = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            return hashString;
        }

        public static bool VerifySignature(string timestampString, string signature)
        {
            string expectedSignature = CalculateSignature(timestampString);
            return signature == expectedSignature;
        }

        public static string GetExtenssionAndBase64(string image)
        {
            string extenssion = "";
            if (image != "")
            {
                if (image.Contains("data:image/jpeg;base64,"))
                {
                    image = image.Replace("data:image/jpeg;base64,", "").Replace(" ", "+");
                    extenssion = ".jpeg";
                }
                if (image.Contains("data:image/jpg;base64,"))
                {
                    image = image.Replace("data:image/jpg;base64,", "").Replace(" ", "+");
                    extenssion = ".jpg";
                }
                if (image.Contains("data:image/png;base64,"))
                {
                    image = image.Replace("data:image/png;base64,", "").Replace(" ", "+");
                    extenssion = ".png";
                }
                if (image.Contains("data:image/gif;base64,"))
                {
                    image = image.Replace("data:image/gif;base64,", "").Replace(" ", "+");
                    extenssion = ".gif";
                }
                if (image.Contains("data:image/bmp;base64,"))
                {
                    image = image.Replace("data:image/bmp;base64,", "").Replace(" ", "+");
                    extenssion = ".bmp";
                }

            }
            return image + extenssion;
        }

        public static int GenerateOTP(int otpLength = 4)
        {
            Random random = new Random();
            int min = (int)Math.Pow(10, otpLength - 1);
            int max = (int)Math.Pow(10, otpLength) - 1;

            return random.Next(min, max);
        }

        public static string GetFilterPropertyValue(string filterObj, string key)
        {
            string value = string.Empty;
            var result = JsonSerializer.Deserialize<List<SelectListItem>>(filterObj);
            if (result != null && result.Count() > 0)
            {
                foreach (var item in result)
                {
                    if (item.Text.Equals(key))
                        value = item.Value;
                }
            }
            return value;
        }

        public static int? GetFilterPropertyValueInt(string filterObj, string key)
        {
            int? value = null;
            var result = JsonSerializer.Deserialize<List<SelectListItem>>(filterObj);
            if (result != null && result.Any())
            {
                foreach (var item in result)
                {
                    if (item.Text.Equals(key))
                    {
                        if (int.TryParse(item.Value, out int parsedValue))
                        {
                            value = parsedValue;
                        }
                        else
                        {
                            value = null; // or handle parsing failure as needed
                        }
                        break; // Exit loop once the matching item is found
                    }
                }
            }
            return value;
        }


        public static string GetUserType(int userType)
        {
            if (userType == Enums.UserType.ShopKeeper.GetHashCode())
                return "Partner";
            else if (userType == Enums.UserType.EndUser.GetHashCode())
                return "Customer";
            else if (userType == Enums.UserType.SuperAdmin.GetHashCode())
                return "Super Admin";
            else if (userType == Enums.UserType.SubAdmin.GetHashCode())
                return "Sub Admin";
            else if (userType == Enums.UserType.ApiService.GetHashCode())
                return "Api Service";

            return "";
        }

        public static string GetIsconGathiyaNumberString(string IsconGathiyaNumber)
        {
            if (string.IsNullOrEmpty(IsconGathiyaNumber) == false && IsconGathiyaNumber.Length == 16)
            {
                var f4 = IsconGathiyaNumber.Substring(0, 4);
                var f8 = f4 + " " + IsconGathiyaNumber.Substring(4, 4);
                var f12 = f8 + " " + IsconGathiyaNumber.Substring(8, 4);
                var f16 = f12 + " " + IsconGathiyaNumber.Substring(12, 4);

                return f16;
            }

            return IsconGathiyaNumber;
        }

        public static string GetTransactionType(int? transactionType)
        {
            if (transactionType == Enums.TransactionType.Purchase.GetHashCode())
                return "Purchase";
            else if (transactionType == Enums.TransactionType.Credit.GetHashCode())
                return "Credit";
            else if (transactionType == Enums.TransactionType.Debit.GetHashCode())
                return "Debit";
            else if (transactionType == Enums.TransactionType.CreditForActivation.GetHashCode())
                return "Credit for activation";
            else if (transactionType == Enums.TransactionType.CreditForSellCard.GetHashCode())
                return "Credit for sell card";

            return "";
        }

        public static string GetBankAccountTypeDescription(int? bankAccountType)
        {
            if (bankAccountType == Enums.BankAccountType.Checking.GetHashCode())
                return "CHECKING";
            else if (bankAccountType == Enums.BankAccountType.Business.GetHashCode())
                return "BUSINESS";
            else if (bankAccountType == Enums.BankAccountType.Savings.GetHashCode())
                return "SAVINGS";
            return "";
        }

        public static string GetImageThumbnailPathName(string filePath, int width, int height = -1, bool isLarge = false)
        {
            var fileInfo = new FileInfo(filePath);
            string fileName = fileInfo.Name;

            var extension = fileInfo.Extension;
            string orignalextension = extension.ToLower();

            // verify extension
            string lExtension = extension.ToLower();
            if (lExtension != ".jpg" && lExtension != ".jpeg" && lExtension != ".png" && lExtension != ".gif" && lExtension != ".bmp")
                return "";

            if (lExtension != ".jpg" && lExtension != ".jpeg")
                orignalextension = extension.ToLower();

            // image thumb name
            string thumbName = fileName.Replace(extension, "") + "_thumb_" + width + (height != -1 ? "_" + height : "") + orignalextension;
            string thumbFilePath = filePath.Replace(fileName, thumbName);

            return thumbFilePath;
        }

        public static string GetCardHistoryOperation(int? cardHistoryOperation)
        {
            if (cardHistoryOperation == Enums.CardHistoryOperation.Insert.GetHashCode())
                return "Insert";
            else if (cardHistoryOperation == Enums.CardHistoryOperation.Update.GetHashCode())
                return "Update";
            else if (cardHistoryOperation == Enums.CardHistoryOperation.Delete.GetHashCode())
                return "Delete";
            else if (cardHistoryOperation == Enums.CardHistoryOperation.PaymentApproved.GetHashCode())
                return "Payment approved";
            else if (cardHistoryOperation == Enums.CardHistoryOperation.PaymentCanceled.GetHashCode())
                return "Payment canceled";
            return "";
        }

        public static DateTime? GetFilterPropertyValueDateTime(string filterObj, string key)
        {
            DateTime? value = null;
            var result = JsonSerializer.Deserialize<List<SelectListItem>>(filterObj);
            if (result != null && result.Any())
            {
                foreach (var item in result)
                {
                    if (item.Text.Equals(key))
                    {
                        if (DateTime.TryParse(item.Value, out DateTime parsedValue))
                        {
                            value = parsedValue;
                        }
                        else
                        {
                            value = null; // or handle parsing failure as needed
                        }
                        break; // Exit loop once the matching item is found
                    }
                }
            }
            return value;
        }


        public static string ToDateTimeString(this DateTime dt)
        {
            return dt.ToString("yyyy-MM-ddTHH:mm:ss");
        }

        public static long ConvertDateToUnixTime(this string dt)
        {
            DateTime dateTime = DateTime.Parse(dt);
            long unixTimestamp = ToUnixTimestamp(dateTime);
            return unixTimestamp;
        }

        public static int ToUnixTimestamp(DateTime dateTime)
        {
            return (int)(dateTime.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds;
        }

        private static bool ValidateTwoFactorCode(string key, string code)
        {
            var totp = new Totp(Base32Encoding.ToBytes(key));
            return totp.VerifyTotp(code, out _, new VerificationWindow(2, 2)); // Allow for some time skew
        }

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
                if (strEncrypted == null || strEncrypted == "")
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

        //public static int? GetUserIdFromJwt(HttpContext context)
        //{
        //    // Retrieve the JWT token from the cookie
        //    var jwtToken = context.Request.Cookies["AuthValidator"];
        //    if (string.IsNullOrEmpty(jwtToken))
        //    {
        //        return null; // Token not found, return null
        //    }

        //    try
        //    {
        //        // Decode the JWT token and extract UserId from claims
        //        var handler = new JwtSecurityTokenHandler();
        //        var jwtSecurityToken = handler.ReadToken(jwtToken) as JwtSecurityToken;

        //        var userIdClaim = jwtSecurityToken?.Claims.FirstOrDefault(c => c.Type == "UserId");
        //        if (userIdClaim == null)
        //        {
        //            return null; // UserId claim not found
        //        }

        //        return Convert.ToInt32(userIdClaim.Value); // Return the UserId
        //    }
        //    catch
        //    {
        //        return null; // In case of any error, return null
        //    }
        //}

        public static string GetMailTemplate(string path, string templateFileName)
        {
            // Initialize an empty string to store the mail template content
            string strMailTemplet = string.Empty;

            // Read the contents of the mail template file
            using (StreamReader sr = new StreamReader("wwwroot/emailtemplate/" + path + "/" + templateFileName))
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
        public static class EnumExtensions
        {
            /// <summary>
            /// Retrieves the description attribute value of the specified enum value, if available.
            /// </summary>
            /// <param name="enumValue">The enum value whose description is to be retrieved.</param>
            /// <returns>
            /// The description attribute value of the enum value, if available; otherwise, the string representation of the enum value.
            /// </returns>
            public static string GetEnumDescription(this Enum enumValue)
            {
                FieldInfo? fi = enumValue.GetType().GetField(enumValue.ToString());

                if (fi == null)
                    return enumValue.ToString();

                DescriptionAttribute[] attributes =
                    (DescriptionAttribute[])fi.GetCustomAttributes(
                    typeof(DescriptionAttribute),
                    false);

                if (attributes != null &&
                    attributes.Length > 0)
                    return attributes[0].Description;
                else
                    return enumValue.ToString();
            }
        }

    

}
