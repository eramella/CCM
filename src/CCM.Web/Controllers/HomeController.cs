using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using CCM.Data.Models;
using AutoMapper;
using Microsoft.Extensions.Logging;
using CCM.Business.Repositories;
using System.Threading.Tasks;
using CCM.ViewModels;

namespace CCM.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private CCMContext _context;
        private IMapper _mapper;
        private ILogger<HomeController> _logger;

        public HomeController(IMapper mapper, CCMContext context, ILogger<HomeController> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            UnitOfWork uow = new UnitOfWork(_context);
            var settings = await uow.AppSettingsRepository.GetAppSettings();
            Camp upcomingCamp = null;
            if (settings.NextCampId != null)
            {
                int id = settings.NextCampId.GetValueOrDefault();
                upcomingCamp = await uow.CampRepository.GetCampById(id);
            }
            var homeSettings = new HomeSettingsVm()
            {
                Settings = _mapper.Map<AppSettingsVm>(settings),
                UpcomingCamp = upcomingCamp != null ? _mapper.Map<CampVm>(upcomingCamp) : null
            };
        
            return View(homeSettings);
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
