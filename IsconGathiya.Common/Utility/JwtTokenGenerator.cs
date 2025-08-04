using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace IsconGathiya.Common
{
    public class JwtTokenGenerator
    {
        public string GenerateJwtToken(string userId, string email)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ConfigItems.JwtKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // create a list of claims with id and exp
            var claims = new List<Claim>
            {
                new Claim("UserId", userId),
                new Claim("Email", email),
            };

            var token = new JwtSecurityToken(
                claims: claims,
                signingCredentials: credentials);


            return new JwtSecurityTokenHandler().WriteToken(token);

        }
        public string GenerateJwtSignature(string id, string exp,string secret)
        {
            //var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ConfigItems.CheckBookAPISecret));
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // create a list of claims with id and exp
            var claims = new List<Claim>
            {
                new Claim("id", id),
            };

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMonths(1),
                signingCredentials: credentials);


            return new JwtSecurityTokenHandler().WriteToken(token);

        }

    }
}
