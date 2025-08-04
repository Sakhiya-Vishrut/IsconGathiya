using Newtonsoft.Json;

namespace IsconGathiya.Common.Utility
{
    public static class UrlHelper
    {
        public static string GenerateEncryptResetPasswordParameters(int userId)
        {

            string timeStamp = string.Empty;
            string signature = string.Empty;

            DateTime currentTimeinUTC = DateTime.UtcNow;
            timeStamp = currentTimeinUTC.ToString("yyyyMMddHHmmssfff");
            signature = CommonHelper.CalculateSignature(timeStamp);

            var parameters = new ResetPasswordParams
            {
                Userid = EncryptionDecryption.GetEncrypt(Convert.ToString(userId)),
                TimeStamp = timeStamp,
                Signature = signature
            };
            var strparameters = JsonConvert.SerializeObject(parameters);
            var encryptedParams = EncryptionDecryption.GetEncrypt(strparameters);
            return encryptedParams;
        }
        public class ResetPasswordParams
        {
            public string Userid { get; set; }
            public string TimeStamp { get; set; }
            public string Signature { get; set; }
        }

    }
}
