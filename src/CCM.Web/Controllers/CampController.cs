using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;


namespace CCM.Controllers
{
    [Authorize]
    [Route("camp/")]
    public class CampController : Controller
    {
        [Route("{id}")]
        public IActionResult Index(int id)
        {
            return View();
        }
    }
}
