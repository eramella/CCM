using CCM.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCM.Business.Repositories
{
    public class SponsorTypeRepository
    {
        private CCMContext _context;

        public SponsorTypeRepository(CCMContext context)
        {
            _context = context;
        }

        public async Task<List<SponsorType>> GetSponsorsTypes()
        {
            return await _context.SponsorTypes.ToListAsync();
        }

        public async Task<SponsorType> GetSponsorTypeById(int id)
        {
            return await _context.SponsorTypes.FirstOrDefaultAsync(s => s.Id == id);
        }

        public void UpdateSponsorType(SponsorType sponsorType)
        {
            _context.Entry<SponsorType>(sponsorType).State = EntityState.Modified;
        }

        public void AddSponsorType(SponsorType sponsorType)
        {
            _context.SponsorTypes.Add(sponsorType);
        }

        public void DeleteSponsorType(SponsorType sponsorType)
        {
            _context.Remove(sponsorType);
        }        
    }
}
