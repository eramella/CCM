using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CCM.ViewModels;
using CCM.Data.Enums;
using CCM.Business.Repositories;
using CCM.Data.Models;
using AutoMapper;
using CCM.Business.Utilities;
using Microsoft.Extensions.Logging;
using System.Linq;
using Microsoft.AspNetCore.Identity;

namespace CCM.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/Speaker")]
    public class SpeakerController : Controller
    {
        private CCMContext _context;
        private IMapper _mapper;
        private ILogger<SpeakerController> _logger;
        private UserManager<CCMUser> _userManager;

        public SpeakerController(CCMContext context, IMapper mapper, ILogger<SpeakerController> logger, UserManager<CCMUser> userManager)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
            _userManager = userManager;

        }

        [HttpGet]
        [Route("/api/Speakers")]
        public IEnumerable<SpeakerVm> GetSpeakers(int campId)
        {
            var speakers = _userManager.Users.Where(u => u.Sessions.Any(s => s.CampId == campId));
            
            return _mapper.Map<IEnumerable<SpeakerVm>>(speakers);
        }

        [HttpGet]
        [Route("{userId}", Name = "SpeakerGet")]
        public async Task<IActionResult> GetSpeaker(string userId)
        {
            CCMUser user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound($"Could not find Speaker of id: {userId}");
            }
            return new ObjectResult(_mapper.Map<SpeakerVm>(user));
        }        
    }
}