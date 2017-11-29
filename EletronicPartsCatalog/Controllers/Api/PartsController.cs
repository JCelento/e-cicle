using EletronicPartsCatalog.Contracts.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EletronicPartsCatalog.Web.Controllers.Api
{
    [Route("api/[controller]")]
    [Authorize]
    public class PartsController : Controller
    {
        private IPartsService _partsService;

        public PartsController(IPartsService partsService) {
            _partsService = partsService;
        }

        [HttpGet]
        public IActionResult Get() {
            return Json(_partsService.GetAll());
        }
    }
}
