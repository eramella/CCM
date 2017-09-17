using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using CCM.Data.Contracts;

namespace CCM.Services
{
    public class UserResolverService : IUserResolverService
    {
        private IHttpContextAccessor _context;

        public UserResolverService(IHttpContextAccessor context)
        {
            _context = context;

        }

        public String GetUserId()
        {
            if (_context.HttpContext == null)
            {
                return "";
            }
            return (_context.HttpContext.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value??"").ToString();
        }
    }
}
