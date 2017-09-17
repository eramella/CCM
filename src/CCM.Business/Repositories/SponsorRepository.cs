using CCM.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCM.Business.Repositories
{
    public class SponsorRepository
    {
        private CCMContext _context;

        public SponsorRepository(CCMContext context)
        {
            _context = context;
        }

        public async Task<List<Sponsor>> GetSponsors(int campId)
        {
            return await _context.Sponsors
                .Include(sponsor => sponsor.SponsorType)
                .Where(sponsor => sponsor.CampId == campId)
                .ToListAsync();
        }

        public async Task<Sponsor> GetSponsorById(string id)
        {
            return await _context.Sponsors
                .Include(sponsor => sponsor.SponsorType)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public void UpdateSponsor(Sponsor sponsor)
        {
            _context.Entry<Sponsor>(sponsor).State = EntityState.Modified;
        }

        public void AddSponsor(Sponsor sponsor)
        {
            _context.Sponsors.Add(sponsor);
        }

        public void DeleteSponsor(Sponsor sponsor)
        {
            _context.Remove(sponsor);
        }        
    }
}
