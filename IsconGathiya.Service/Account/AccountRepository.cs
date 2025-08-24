using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IsconGathiya.Common;
using IsconGathiya.Domain.DataContext;
using IsconGathiya.Domain.DataModels;
using IsconGathiya.Service.Account;
using IsconGathiya.Common.DependencyInjection;

namespace IsconGathiya.Service.Account
{
    [TransientDependency(ServiceType = typeof(IAccountRepository))]
    public class AccountRepository : IAccountRepository
    {
        private readonly ApplicationDbContext _context;
        public AccountRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        #region GetUserDetailsByEmail
        public SecAdmin GetUserDetailsByEmail(string email)
        {
            return _context.SecAdmins.Where(x => x.Email == email && x.Deleted == null).FirstOrDefault();
        }
        #endregion

        #region SaveUserToken
        public void SaveUserToken(Guid aspNetUserId, string token)
        {
            var aspNetUser = _context.AspAspNetUsers.FirstOrDefault(x => x.AspNetUserId == aspNetUserId);
            if (aspNetUser != null)
            {
                aspNetUser.Token = token;
                _context.Update(aspNetUser);
                _context.SaveChanges();
            }
        }
        #endregion
        public AspAspNetUser GetAspNetUserById(Guid? aspNetUserId)
        {
            return _context.AspAspNetUsers.FirstOrDefault(x => x.AspNetUserId == aspNetUserId);
        }
    }
}