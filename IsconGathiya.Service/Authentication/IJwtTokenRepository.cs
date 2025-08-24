using IsconGathiya.Domain.DataModels;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsconGathiya.Service.Authentication
{
    public interface IJwtTokenRepository
    {
        string GenerateJWTAuthetication(SecAdmin addminInfo);
        bool ValidateToken(string token, out JwtSecurityToken jwtSecurityToken);

    }
}
