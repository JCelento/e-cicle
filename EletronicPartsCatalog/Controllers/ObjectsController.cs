using System.Collections.Generic;
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
        private IPartsService _partsService;

        public ObjectsController(IObjectsService objectsService, IPartsService partsService) {
            _objectsService = objectsService;
            _partsService = partsService;
        }
        [HttpGet]
        public IActionResult Index() {
            return View();
        }

        [HttpGet]
        public IActionResult Add() {
            List<PartDto> parts = new List<PartDto>();
            parts = _partsService.GetAll();
            return View(new ObjectCreateViewModel
            {
                Parts = parts      
            });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(ObjectCreateViewModel viewModel)
        {
            var objectParts = new List<ObjectPartDto>();
            foreach(var part in viewModel.Parts)
            {
                if (part.Selected)
                {
                    objectParts.Add(new ObjectPartDto
                    {
                        Part = part
                    });
                }
            }

            var newObject = new ObjectDto
            {
                Name = viewModel.Name,
                Description = viewModel.Description,
                CreatedBy = User.Identity.Name
            };

            foreach (var objectPart in objectParts)
            {
                objectPart.Object = newObject;
            }

            var result = _objectsService.Add(new AddObjectDto
            {
                Name = viewModel.Name,
                Description = viewModel.Description,
                CreatedBy = User.Identity.Name,
                ObjectParts = objectParts
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
            var parts = new List<PartDto>();
            foreach (var part in result.Item.ObjectParts)
            {
                parts.Add(_partsService.GetById(part.PartId).Item);
            }

            return View(new ObjectViewModel() {
                Id = result.Item.Id,
                Name = result.Item.Name,
                Description = result.Item.Description,
                CreationDate = result.Item.CreationDate.Date.ToString("dd-MM-yyyy"),
                CreatedBy = result.Item.CreatedBy,
                Parts = parts
            });
        }

    }
}
