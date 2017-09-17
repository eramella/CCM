using CCM.Data.DynamicQuery;
using CCM.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCM.Business.Repositories
{
    public class UsersRepository
    {
        private CCMContext _context;

        public UsersRepository(CCMContext context)
        {
            _context = context;
        }

        public DynamicResult<CCMUser> GetUsers(DynamicRequest requst)
        {
            var users = _context.Users.AsQueryable();

            if (!String.IsNullOrWhiteSpace(requst.Token))
            {
                users = users.Where(u => u.UserName.Contains(requst.Token) || u.FirstName.Contains(requst.Token) || u.LastName.Contains(requst.Token));
            }

            var total = users.Count();

            if (requst.Skip > 0)
            {
                users = users.Skip(requst.Skip);
            }

            users = users.Take(requst.PageSize);

            var result = new DynamicResult<CCMUser>()
            {
                Data = users,
                Total = total
            };

            return result;
        }
    }
}
