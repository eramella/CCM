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
using CCM.Data.DynamicQuery;
using Microsoft.AspNetCore.Authorization;

namespace CCM.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/Team")]
    public class TeamController : Controller
    {
        private CCMContext _context;
        private IMapper _mapper;
        private ILogger<TeamController> _logger;
        private UserManager<CCMUser> _userManager;

        public TeamController(CCMContext context, IMapper mapper, ILogger<TeamController> logger, UserManager<CCMUser> userManager)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
            _userManager = userManager;

        }

        [HttpPost]
        [Route("/api/TeamMembers")]
        public IActionResult PostFindMembers([FromBody]DynamicRequest request)
        {
            if (ModelState.IsValid)
            {
                UnitOfWork uow = new UnitOfWork(_context);
                var queryResult = uow.UsersRepository.GetUsers(request);
                var result = new DynamicResult<TeamMemberVm>()
                {
                    Data = _mapper.Map<IEnumerable<CCMUser>, IEnumerable<TeamMemberVm>>(queryResult.Data),
                    Total = queryResult.Total
                };
                return new ObjectResult(result);
            }

            return BadRequest("Could not perform search. Incorrect request");
        }

        //[HttpGet]
        //[Route("{userId}", Name = "SpeakerGet")]
        //public async Task<IActionResult> GetSpeaker(string userId)
        //{
        //    CCMUser user = await _userManager.FindByIdAsync(userId);
        //    if (user == null)
        //    {
        //        return NotFound($"Could not find Speaker of id: {userId}");
        //    }
        //    return new ObjectResult(_mapper.Map<SpeakerVm>(user));
        //}        
    }
}