using EletronicPartsCatalog.Contracts.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EletronicPartsCatalog.Web.Controllers.Api
{
    [Route("api/[controller]")]
    [Authorize]
    public class ProjectsController : Controller
    {
        private IProjectsService _projectsService;

        public ProjectsController(IProjectsService projectsService) {
            _projectsService = projectsService;
        }

        [HttpGet]
        public IActionResult Get() {
            return Json(_projectsService.GetAll());
        }
    }
}
