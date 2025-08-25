using DocumentFormat.OpenXml.Spreadsheet;
using IsconGathiya.Common;
using IsconGathiya.Common.DependencyInjection;
using IsconGathiya.Domain.DataModels;
using IsconGathiya.Service.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace IsconGathiya.Service.Authentication
{
    [TransientDependency(ServiceType = typeof(IJwtTokenRepository))]
    public class JwtTokenRepository : IJwtTokenRepository
    {
        private readonly IConfiguration _config;

        public JwtTokenRepository(IConfiguration config)
        {
            _config = config;
        }
        public string GenerateJWTAuthetication(SecAdmin adminInfo)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, adminInfo.UserName),
                new Claim("Username", adminInfo.UserName),
                new Claim("AdminID", adminInfo.AdminId.ToString()),
                new Claim("BranchId", adminInfo.BranchId.ToString()),
                new Claim("AspNetUserId", adminInfo.AspNetUserId.ToString()),
                new Claim("RollType", adminInfo.RollType.ToString()),
                new Claim(JwtHeaderParameterNames.Kid, Guid.NewGuid().ToString()),
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(ConfigItems.JwtKey));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expires = DateTime.Now.AddDays(30);

            var token = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: expires,
                signingCredentials: creds
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public bool ValidateToken(string token, out JwtSecurityToken jwtSecurityToken)
        {
            jwtSecurityToken = null;
            if (token == null)
            {
                return false;
            }
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(ConfigItems.JwtKey);
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    ValidateAudience = false,
                    ValidIssuer = _config["Jwt:Issuer"],
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                jwtSecurityToken = (JwtSecurityToken)validatedToken;
                if (jwtSecurityToken != null)
                    return true;
                return false;
            }
            catch (Exception ex)
            {
                return (false);
            }
        }
    }
}