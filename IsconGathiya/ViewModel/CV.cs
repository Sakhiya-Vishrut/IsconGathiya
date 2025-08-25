using IsconGathiya.Domain.DataModels;
using IsconGathiya.ViewModel;
using System.IdentityModel.Tokens.Jwt;

namespace IsconGathiya.ViewModel
{
    public class CV
    {
        private static IHttpContextAccessor _contextAccessor;

        static CV()
        {
            _contextAccessor = new HttpContextAccessor();
        }
        public static string? AdminId()
        {
            string cookieValue;
            string UserID = null;

            if (_contextAccessor.HttpContext.Request.Cookies["AuthValidator"] != null)
            {
                cookieValue = _contextAccessor.HttpContext.Request.Cookies["AuthValidator"].ToString();

                UserID = DecodeToken.DecodeJwt(DecodeToken.ConvertJwtStringToJwtSecurityToken(cookieValue)).claims.FirstOrDefault(t => t.Key == "AdminID").Value;
            }

            return UserID;
        }
        public static string? Roll()
        {
            string cookieValue;
            string roll = null;

            if (_contextAccessor.HttpContext.Request.Cookies["AuthValidator"] != null)
            {
                cookieValue = _contextAccessor.HttpContext.Request.Cookies["AuthValidator"].ToString();

                roll = DecodeToken.DecodeJwt(DecodeToken.ConvertJwtStringToJwtSecurityToken(cookieValue)).claims.FirstOrDefault(t => t.Key == "RollType").Value;
            }

            return roll;
        }
        public static string? Branch()
        {
            string cookieValue;
            string branch = null;

            if (_contextAccessor.HttpContext.Request.Cookies["AuthValidator"] != null)
            {
                cookieValue = _contextAccessor.HttpContext.Request.Cookies["AuthValidator"].ToString();

                branch = DecodeToken.DecodeJwt(DecodeToken.ConvertJwtStringToJwtSecurityToken(cookieValue)).claims.FirstOrDefault(t => t.Key == "BranchId").Value;
            }

            return branch;
        }
        public static string? Email()
        {
            string cookieValue;
            string UserID = null;

            if (_contextAccessor.HttpContext.Request.Cookies["AuthValidator"] != null)
            {
                cookieValue = _contextAccessor.HttpContext.Request.Cookies["AuthValidator"].ToString();

                UserID = DecodeToken.DecodeJwt(DecodeToken.ConvertJwtStringToJwtSecurityToken(cookieValue)).claims.FirstOrDefault(t => t.Key == "Email").Value;
            }

            return UserID;
        }
        public static string? Username()
        {
            string cookieValue;
            string UserID = null;

            if (_contextAccessor.HttpContext.Request.Cookies["AuthValidator"] != null)
            {
                cookieValue = _contextAccessor.HttpContext.Request.Cookies["AuthValidator"].ToString();

                UserID = DecodeToken.DecodeJwt(DecodeToken.ConvertJwtStringToJwtSecurityToken(cookieValue)).claims.FirstOrDefault(t => t.Key == "Username").Value;
            }

            return UserID;
        }
        public static string? AspNetUserId()
        {
            string cookieValue;
            string UserID = null;

            if (_contextAccessor.HttpContext.Request.Cookies["AuthValidator"] != null)
            {
                cookieValue = _contextAccessor.HttpContext.Request.Cookies["AuthValidator"].ToString();

                UserID = DecodeToken.DecodeJwt(DecodeToken.ConvertJwtStringToJwtSecurityToken(cookieValue)).claims.FirstOrDefault(t => t.Key == "AspNetUserId").Value;
            }

            return UserID;
        }
    }
}
