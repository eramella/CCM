using CCM.Data.Enums;
using CCM.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCM.Business.Repositories
{
    public class CampRepository
    {
        private CCMContext _context;

        public CampRepository(CCMContext context)
        {
            _context = context;
        }

        public async Task<List<Camp>> GetCamps(CampState? state)
        {
            if (state != null)
            {
                return await _context.Camps.Where(q => q.State == state).AsNoTracking<Camp>().ToListAsync();
            }
            return await _context.Camps.AsNoTracking<Camp>().ToListAsync();
        }

        public async Task<Camp> GetActiveCamp()
        {
            var activeCamp = await _context.Camps.AsNoTracking<Camp>().FirstOrDefaultAsync(q => q.State == CampState.Active);

            if (activeCamp == null)
            {
                var upcomingCamp = await _context.Camps.Where(q => q.State == CampState.Upcoming).AsNoTracking<Camp>().OrderBy(o => o.DateFrom).FirstOrDefaultAsync();
                return upcomingCamp;
            }

            return activeCamp;
        }

        public async Task<Camp> GetCampById(int id)
        {
            return await _context.Camps.FindAsync(id);
        }

        public void AddCamp(Camp ev)
        {
            _context.Camps.Add(ev);
        }

        public void UpdateCamp(Camp ev)
        {
            _context.Entry<Camp>(ev).State = EntityState.Modified;
            if (ev.State == CampState.Active)
            {
                var activeCamps = _context.Camps.Where(e => e.State == CampState.Active && e.Id != ev.Id);
                foreach (var camp in activeCamps)
                {
                    if (camp.DateTo < DateTime.Today.ToUniversalTime())
                    {
                        camp.State = CampState.Past;
                    }
                    else
                    {
                        camp.State = CampState.Upcoming;
                    }
                }
            }
        }

        public void DeleteCamp(Camp camp)
        {
            if (camp.State != CampState.Past)
            {
                _context.Remove(camp);
            }
        }
    }
}
