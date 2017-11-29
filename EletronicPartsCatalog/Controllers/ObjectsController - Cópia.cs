using EletronicPartsCatalog.Contracts.DataContracts;
using EletronicPartsCatalog.Contracts.Services;
using EletronicPartsCatalog.Web.Pages.Models;
using EletronicPartsCatalog.Web.Pages.Projects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EletronicPartsCatalog.Web.Controllers
{
    [Authorize]
    public class ObjectsController : Controller
    {
        private IObjectsService _objectsService;

        public ObjectsController(IObjectsService objectsService) {
            _objectsService = objectsService;
        }
        [HttpGet]
        public IActionResult Index() {
            return View();
        }

        [HttpGet]
        public IActionResult Add() {
            return View(new ObjectCreateViewModel());
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(ObjectCreateViewModel viewModel) {
            var result = _objectsService.Add(new AddObjectDto {
                Name = viewModel.Name,
                Description = viewModel.Description,
                CreatedBy = User.Identity.Name
            });

            if (result.IsSuccess) {
                return RedirectToAction(nameof(ObjectsController.Index), "Objects");
            } else {
                viewModel.ErrorMessage = result.ErrorMessage;
                return View(viewModel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id) {
            _objectsService.Delete(id);

            return RedirectToAction(nameof(ObjectsController.Index), "Objects");
        }

        [HttpGet]
        public IActionResult DetailsObj(int id) {
            var result = _objectsService.GetById(id);

            return View(new ObjectViewModel() {
                Id = result.Item.Id,
                Name = result.Item.Name,
                Description = result.Item.Description,
                CreationDate = result.Item.CreationDate.Date.ToString("dd-MM-yyyy"),
                CreatedBy = result.Item.CreatedBy
            });
        }

    }
}
