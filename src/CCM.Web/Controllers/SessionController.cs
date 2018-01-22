using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CCM.Business.Repositories;
using CCM.Data.Models;
using CCM.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CCM.Controllers
{
    [AllowAnonymous]
    public class SessionController : Controller
    {
        private IMapper _mapper;
        private CCMContext _context;
        private UserManager<CCMUser> _userManager;
        private ILogger<ProfileController> _logger;

        public SessionController(IMapper mapper,
            CCMContext context,
            UserManager<CCMUser> userManager,
            ILogger<ProfileController> logger)
        {
            _mapper = mapper;
            _context = context;
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<IActionResult> Index(int id)
        {
            var uow = new UnitOfWork(_context);
            var sessions = await uow.SessionRepository.GetSessions(id);
            return View(_mapper.Map<List<Session>, List<SessionVm>>(sessions));
        }
    }
}