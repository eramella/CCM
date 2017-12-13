using AutoMapper;
using CCM.Business.Repositories;
using CCM.Data.Models;
using CCM.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CCM.ViewComponents
{
    public class TeamViewComponent : ViewComponent
    {
        private IMapper _mapper;
        private UserManager<CCMUser> _userManager;

        public TeamViewComponent(CCMContext context, IMapper mapper, UserManager<CCMUser> userManager)
        {
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var users = await _userManager.GetUsersInRoleAsync("team");
            var model = _mapper.Map<List<UserVm>>(users);
            
            return View(model.OrderBy(o => o.LastName));
        }
    }
}
