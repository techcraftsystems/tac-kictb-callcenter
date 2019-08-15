using Core.Service;
using Core.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Core.Controllers
{
    [Authorize]
    public class FacilityController : Controller
    {
        [Route("/facilities")]
        public IActionResult Index(FacilityViewModel model, CoreService service) {
            model.Facilities = service.GetFacilities();
            return View(model);
        }

        [Route("/facilities/{code}")]
        public IActionResult Facility(string code, FacilityViewModel model, CoreService service) {
            model.Facility = service.GetFacility(code);
            return View(model);
        }

    }
}
