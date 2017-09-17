using CCM.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCM.Business.Repositories
{
    public class SessionRepository
    {
        private CCMContext _context;

        public SessionRepository(CCMContext context)
        {
            _context = context;
        }

        public async Task<List<Session>> GetSessions(int campId)
        {
            return await _context.Sessions
                .Include(session => session.User)
                .Include(session => session.TagSessions)
                    .ThenInclude(ts => ts.Tag)
                    .Where(s => s.CampId == campId)
                .ToListAsync();
        }

        public async Task<Session> GetSessionById(string id)
        {
            return await _context.Sessions
                .Include(session => session.TagSessions)
                .ThenInclude(ts => ts.Tag)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public void UpdateSession(Session session)
        {
            _context.Entry<Session>(session).State = EntityState.Modified;
        }

        public void AddSession(Session session)
        {
            _context.Sessions.Add(session);
        }

        public void DeleteSession(Session session)
        {
            _context.Remove(session);
        }        
    }
}
