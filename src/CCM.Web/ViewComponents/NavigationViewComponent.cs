using AutoMapper;
using CCM.Business.Repositories;
using CCM.Data.Models;
using CCM.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CCM.ViewComponents
{
    public class NavigationViewComponent : ViewComponent
    {
        private CCMContext _context;
        private IMapper _mapper;

        public NavigationViewComponent(CCMContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            UnitOfWork uow = new UnitOfWork(_context);
            var activeCamp = await uow.CampRepository.GetActiveCamp();
            var model = _mapper.Map<CampVm>(activeCamp);
            if (model == null)
            {
                model = new CampVm();
            }
            return View(model);
        }
    }
}
