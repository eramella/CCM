using CCM.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCM.Business.Repositories
{
    public class AppSettingsRepository
    {
        private CCMContext _context;

        public AppSettingsRepository(CCMContext context)
        {
            _context = context;
        }

        public async Task<AppSettings> GetAppSettings()
        {
            return await _context.AppSettings.FirstOrDefaultAsync(a => a.Id == true);
        }

        public void UpdateAppSettings(AppSettings appSettings)
        {
            _context.Entry(appSettings).State = EntityState.Modified;
        }        
    }
}
