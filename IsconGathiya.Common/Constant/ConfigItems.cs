using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace IsconGathiya.Common
{
    public class ConfigItems
    {
        public static string Domain;
        private static IHttpContextAccessor _httpContextAccessor;
        private static HttpContext HttpContext;
        private static IConfiguration Configuration;

        public static void Initialize(IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            _httpContextAccessor = httpContextAccessor;
            HttpContext = httpContextAccessor.HttpContext;
            Domain = _httpContextAccessor.HttpContext?.Request.Host.ToString(); // Correct assignment
           
            Configuration = configuration;
        }

        public static String DBConnectionString()
        {
           
            return ConnectionString;

        }

        #region Default Connection

        public static string ConnectionString => Configuration["Data:DefaultConnection:ConnectionString"];

        public static string IpAddress => _httpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString();

        public static string? Domainmain => FnDomainmain();

        public static String FnDomainmain()
        {

            try
            {
                if (_httpContextAccessor.HttpContext == null)
                {
                    Console.WriteLine("FnDomainmain in _httpContextAccessor.HttpContext null");
                    return string.Empty;
                }
                else
                {
                    Console.WriteLine("FnDomainmain in _httpContextAccessor.HttpContext not null");
                    return _httpContextAccessor.HttpContext?.Request.Host.ToUriComponent();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("FnDomainmain in Error Msg" + ex.ToString());
                return string.Empty;
            }

        }

        #endregion


        public static string CryptographyKey => Configuration["Data:CryptographyKey"];

        public static string JwtKey => Configuration["Jwt:Key"];
        public static string StorageBaseLink => Configuration["Data:StorageBaseLink"];
        public static string JwtIssuer => Configuration["Jwt:Issuer"];
        public static string JwtAudience => Configuration["Jwt:Audience"];
        public static int SessionIdleTimeoutInDay => Convert.ToInt32(Configuration["Data:SessionIdleTimeoutInDay"]);


        #region Email Configuration details

        public static bool IsSendEmail => Convert.ToBoolean(Configuration["Data:Email:IsSendEmail"]);
        public static string SMTPHost => Convert.ToString(Configuration["Data:Email:SMTPHost"]);
        public static int SMTPPort => Convert.ToInt32(Configuration["Data:Email:SMTPPort"]);
        public static bool IsSMTPEnableSsl => Convert.ToBoolean(Configuration["Data:Email:IsSMTPEnableSsl"]);
        public static string DisplayEmailSender => Convert.ToString(Configuration["Data:Email:DisplayEmailSender"]);
        public static string EmailSender => Convert.ToString(Configuration["Data:Email:Sender"]);
        public static string EmailPassword => Convert.ToString(Configuration["Data:Email:Password"]);
        public static string AdminEmail => Convert.ToString(Configuration["Data:AdminEmail:Email"]);

        #endregion

        #region
        public static bool CacheWithRemoveOtherSite => Convert.ToBoolean(Configuration["Data:CacheWithRemoveOtherSite"]);
        public static bool IsLockCustomCacheMethods => Convert.ToBoolean(Configuration["Data:IsLockCustomCacheMethods"]);
        public static bool IsCacheActive => Convert.ToBoolean(Configuration["Data:IsCacheActive"]);
        public static bool CacheInSleepWhileRequestIsInProgress => Convert.ToBoolean(Configuration["Data:CacheInSleepWhileRequestIsInProgress"]);
        #endregion


        public static string RegisterOTPTemplate => Convert.ToString(Configuration["Data:SmsMessages:RegisterOTPTemplate"]);


        #region Default Variables
        public static int DefaultPageNumber => Convert.ToInt32(Configuration["Data:DefaultPageNumber"]);
        public static string DefaultPageNumberT => Configuration["Data:DefaultPageNumber"];
        public static int DefaultPageSize => Convert.ToInt32(Configuration["Data:DefaultPageSize"]);
        public static int DefaultMenuId => Convert.ToInt32(Configuration["Data:DefaultMenuId"]);
        public static int DefaultInCorrectPasswordAttempt => Convert.ToInt32(Configuration["Data:DefaultInCorrectPasswordAttempt"]);
        public static int AfterHoursToUnBlock => Convert.ToInt32(Configuration["Data:AfterHoursToUnBlock"]);
        #endregion

        #region JWT

        public static string JwtSubject => Convert.ToString(Configuration["Data:Jwt:Subject"]);
        public static string IsconGathiyaSecretAnonymousKey => Convert.ToString(Configuration["Data:IsconGathiyaSecretAnonymousKey"]);
        public static string IsconGathiyaSecretKey => Convert.ToString(Configuration["Data:IsconGathiyaSecretKey"]);
        #endregion

        #region ApiCalling
        public static string Oauth2TokenUrl => Configuration["Data:ApiUrl:Oauth2TokenUrl"];
        public static string BaseUrlForDomain => Configuration["Data:ApiUrl:BaseUrlForDomain"];
        public static string ClientId => Configuration["Data:ApiUrl:ClientId"];
        public static string ClientSecret => Configuration["Data:ApiUrl:ClientSecret"];
        public static string UserName => Configuration["Data:ApiUrl:UserName"];
        public static string Password => Configuration["Data:ApiUrl:Password"];
        public static string GrantType => Configuration["Data:ApiUrl:GrantType"];

        #endregion
    }
}
