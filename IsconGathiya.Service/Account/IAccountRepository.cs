using IsconGathiya.Domain.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsconGathiya.Service.Account
{
    public interface IAccountRepository
    {
        SecAdmin GetUserDetailsByEmail(string email);
        AspAspNetUser GetAspNetUserById(Guid? aspNetUserId);
        void SaveUserToken(Guid aspNetUserId, string token);
    }
}
