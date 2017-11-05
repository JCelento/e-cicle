using EletronicPartsCatalog.Contracts.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EletronicPartsCatalog.Web.Controllers
{
    [Authorize]
    public class ProjectsController : Controller
    {
        private IProjectsService _projectsService;

        public ProjectsController(IProjectsService projectsService) {
            _projectsService = projectsService;
        }
        [HttpGet]
        public IActionResult Index() {
            return View();
        }
    }
}
