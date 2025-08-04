using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace IsconGathiya.Common
{
    public static class TokenGenerator
    {
        public static string GenerateJwt(string aspNetUserGUID, string email)
        {
            // create claims details based on the user information
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, ConfigItems.JwtSubject),
                new Claim(JwtRegisteredClaimNames.Jti, aspNetUserGUID),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                new Claim("Email", email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ConfigItems.JwtKey));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);
            var token = new JwtSecurityToken(ConfigItems.JwtIssuer, ConfigItems.JwtAudience, claims, expires: DateTime.UtcNow.AddMinutes(ConfigItems.SessionIdleTimeoutInDay), signingCredentials: signIn);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public static string GenerateIsconGathiyaSecret()
        {
            using (SHA512 shaM = new SHA512Managed())
            {
                return Convert.ToBase64String(shaM.ComputeHash(Encoding.UTF8.GetBytes(ConfigItems.IsconGathiyaSecretKey)));
            }
        }

        public static string GenerateIsconGathiyaSecretAnonymous()
        {
            using (SHA512 shaM = new SHA512Managed())
            {
                return Convert.ToBase64String(shaM.ComputeHash(Encoding.UTF8.GetBytes(ConfigItems.IsconGathiyaSecretAnonymousKey)));
            }
        }
    }
}
