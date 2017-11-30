using EletronicPartsCatalog.Contracts.DataContracts;
using EletronicPartsCatalog.Contracts.Services;
using EletronicPartsCatalog.Web.Pages.Models;
using EletronicPartsCatalog.Web.Pages.Projects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EletronicPartsCatalog.Web.Controllers
{
    [Authorize]
    public class PartsController : Controller
    {
        private IPartsService _partsService;

        public PartsController(IPartsService partsService) {
            _partsService = partsService;
        }
        [HttpGet]
        public IActionResult Index() {
            return View();
        }

        [HttpGet]
        public IActionResult Add() {
            return View(new PartCreateViewModel());
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(PartCreateViewModel viewModel) {
            var result = _partsService.Add(new AddPartDto {
                Name = viewModel.Name,
                Description = viewModel.Description,
                CreatedBy = User.Identity.Name
            });

            if (result.IsSuccess) {
                return RedirectToAction(nameof(PartsController.Index), "Parts");
            } else {
                viewModel.ErrorMessage = result.ErrorMessage;
                return View(viewModel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id) {
            _partsService.Delete(id);

            return RedirectToAction(nameof(PartsController.Index), "Parts");
        }

        [HttpGet]
        public IActionResult DetailsPart(int id) {
            var result = _partsService.GetById(id);

            return View(new PartViewModel() {
                Id = result.Item.Id,
                Name = result.Item.Name,
                Description = result.Item.Description,
                CreationDate = result.Item.CreationDate.Date.ToString("dd-MM-yyyy"),
                CreatedBy = result.Item.CreatedBy
            });
        }

    }
}
