using Homvork_asp.DTO;
using Homvork_asp.Service;
using Microsoft.AspNetCore.Mvc;

namespace Homvork_asp.Controllers
{
    public class InsuranceController : Controller
    {
        private readonly IInsuranceService _insuranceService;

        public InsuranceController(IInsuranceService insuranceService)
        {
            _insuranceService = insuranceService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(new InsuranceViewModel());
        }

        [HttpPost]
        public IActionResult Index(InsuranceViewModel model)
        {
            var result = _insuranceService.Calculate(model);
            return View(result);
        }
    }
}
