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
        private RoleManager<IdentityRole> _roleManager;

        public TeamController(CCMContext context, IMapper mapper, ILogger<TeamController> logger, UserManager<CCMUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
            _userManager = userManager;
            _roleManager = roleManager;

        }

        [HttpPost]
        [Route("/api/TeamMembers")]
        public async Task<IActionResult> PostFindMembers([FromBody]DynamicRequest request)
        {
            if (ModelState.IsValid)
            {
                UnitOfWork uow = new UnitOfWork(_context);
                var queryResult = uow.UsersRepository.GetUsers(request);
                DynamicResult<TeamMemberVm> result = new DynamicResult<TeamMemberVm>();
                List<TeamMemberVm> data = new List<TeamMemberVm>();
                foreach (var user in queryResult.Data)
                {
                    TeamMemberVm memberVm = new TeamMemberVm()
                    {
                        FirstName = user.FirstName,
                        Id = user.Id,
                        isTeamMember = await _userManager.IsInRoleAsync(user, "Team"),
                        LastName = user.LastName,
                        Username = user.UserName
                    };
                    data.Add(memberVm);
                }
                result.Data = data;
                result.PageSize = queryResult.PageSize;
                result.Skip = queryResult.Skip;
                result.Total = queryResult.Total;

                //var result = new DynamicResult<TeamMemberVm>()
                //{
                //    Data = _mapper.Map<IEnumerable<CCMUser>, IEnumerable<TeamMemberVm>>(queryResult.Data),
                //    Total = queryResult.Total,
                //    PageSize = queryResult.PageSize,
                //    Skip = queryResult.Skip
                //};

                return new ObjectResult(result);
            }

            return BadRequest("Could not perform search. Incorrect request");
        }

        [HttpPut]
        [Route("/api/MakeMember/{id}")]
        public async Task<IActionResult> PutMakeMember(string id)
        {
            var user = await _userManager.FindByIdAsync(id);            
            var result = await _userManager.AddToRoleAsync(user, "Team");
            if (result.Succeeded)
            {
                return Ok("Added");
            }

            return NotFound(result.Errors);
        }

        [HttpPut]
        [Route("/api/RemoveMember/{id}")]
        public async Task<IActionResult> PutRemoveMember(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var result = await _userManager.RemoveFromRoleAsync(user, "Team");
            if (result.Succeeded)
            {
                return Ok("Removed");
            }

            return NotFound(result.Errors);
        }
    }
}