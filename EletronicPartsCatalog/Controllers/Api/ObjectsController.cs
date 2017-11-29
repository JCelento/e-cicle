using EletronicPartsCatalog.Contracts.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EletronicPartsCatalog.Web.Controllers.Api
{
    [Route("api/[controller]")]
    [Authorize]
    public class ObjectsController : Controller
    {
        private IObjectsService _objectsService;

        public ObjectsController(IObjectsService objectsService) {
            _objectsService = objectsService;
        }

        [HttpGet]
        public IActionResult Get() {
            return Json(_objectsService.GetAll());
        }
    }
}
