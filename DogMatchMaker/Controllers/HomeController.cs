using CrossCutting;
using DogMatchMaker.Orchestrator;
using DogMatchMaker.UI.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DogMatchMaker.UI.Controllers
{
    public class HomeController : Controller
    {
        private IHomeOrchestrator homeOrchestrator;
        public HomeController(IHomeOrchestrator homeOrchestrator)
        {
            this.homeOrchestrator = homeOrchestrator;
        }
        public ActionResult Index()
        {
            List<DogViewModel> dogModels = homeOrchestrator.GetAllDogs()
                                        .Select(d => { return new DogViewModel(d); }).ToList();
            ViewData["jsonDogs"] = JsonConvert.SerializeObject(dogModels);
            return View(dogModels);
        }

        public ActionResult Home()
        {
            return View();
        }

        public ActionResult Quiz()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ViewDogDetails(DogViewModel model)
        {
            DogViewModel dog = new DogViewModel(homeOrchestrator.GetDogById(model.Id));
            if (dog != null)
            {
                return PartialView("_DogDetailsPartial", dog);
            }
            else
            {
                return RedirectToAction("index", "home");
            }
        }

        public ActionResult Create()
        {
            ViewData["dogColors"] = homeOrchestrator.GetDogColors();
            ViewData["dogBreeds"] = homeOrchestrator.GetDogBreeds();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DogViewModel model, HttpPostedFileBase file)
        {
            bool saveSuccess = true;
            if (ModelState.IsValid)
            {
                string path = Path.Combine(Server.MapPath("~/dogimages"),
                                               model.PhotoPath);

                List<string> errors = homeOrchestrator.ValidateDogPhoto(file, path);

                if (errors.Count > 0)
                {
                    errors.ForEach(e => ModelState.AddModelError(string.Empty, e));
                    saveSuccess = false;
                }
                else
                {
                    saveSuccess = homeOrchestrator.InsertDog(MapModelToDto(model));
                }
            }

            if (saveSuccess)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                //Repopulate color and breed list for view
                ViewData["dogColors"] = homeOrchestrator.GetDogColors();
                ViewData["dogBreeds"] = homeOrchestrator.GetDogBreeds();
                return View(model);
            }
        }

        private DogDto MapModelToDto(DogViewModel model)
        {
            return new DogDto()
            {
                Birthday = model.Birthday,
                Breed = model.Breed,
                Breeds = model.Breeds,
                CityLocation = model.CityLocation,
                Color = model.Color,
                ContactEmail = model.ContactEmail,
                ContactName = model.ContactName,
                CrateTrained = model.CrateTrained,
                Description = model.Description,
                Gender = model.Gender,
                GoodWithCats = model.GoodWithCats,
                GoodWithKids = model.GoodWithKids,
                Id = model.Id,
                Name = model.Name,
                PottyTrained = model.PottyTrained,
                StateLocation = model.StateLocation,
                StreetAddress = model.StreetAddress,
                Zipcode = model.Zipcode
            };
        }
    }
}