using DogMatchMaker.Data;
using DogMatchMaker.UI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace DogMatchMaker.UI.Controllers
{
    public class HomeController : Controller
    {
        private static DogRepository dogRepository;

        public HomeController()
        {
            dogRepository = new DogRepository("DogMatchMaker");
        }
        public ActionResult Index()
        {
            List<DogDto> dogDtos = dogRepository.LoadRecords<DogDto>("Dog");
            List<DogViewModel> dogModels = new List<DogViewModel>();
            dogDtos.ForEach(d => { dogModels.Add(new DogViewModel(d)); });
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
            DogViewModel dog = new DogViewModel(dogRepository.LoadRecordById<DogDto>("Dog", model.Id));
            if (dog != null)
            {
                return PartialView( "_DogDetailsPartial", dog);
            }
            else
            {
                return RedirectToAction("index", "home");
            }
        }

        public ActionResult Create()
        {
            ViewData["dogColors"] = dogRepository.GetDogColors();
            ViewData["dogBreeds"] = dogRepository.GetDogBreedList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DogViewModel model, HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
                try
                {
                    var img = System.Drawing.Image.FromStream(file.InputStream, true, true);
                    int w = img.Width;
                    int h = img.Height;
                    if(h < 200 || w < 200)
                    {
                        throw new Exception(message: "Image is too small");
                    }
                    //Only accepts jpeg, jpg, and png
                    if (file.ContentType == "image/jpeg" || file.ContentType == "image/png" || file.ContentType == "image/jpg")
                    {
                        //model.Id = dogRepository.Count;

                        //get full path
                        string path = Path.Combine(Server.MapPath("~/dogimages"),
                                                   model.PhotoPath);
                        //save the uploaded file to this path
                        file.SaveAs(path);
                        ViewBag.Message = "File uploaded successfully";
                    }
                    else
                    {
                        //If file is the wrong type, throw exception
                        throw new Exception(message: "File type not accepted");
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Message = ex.Message;
                    //If exception was thrown, return view with error message
                    ViewData["dogColors"] = dogRepository.GetDogColors();
                    ViewData["dogBreeds"] = dogRepository.GetDogBreedList();
                    return View(model);
                }

            if (ModelState.IsValid)
            {
                if(model.Breeds != null)
                {
                    model.Breed = "Mixed Breed";
                }

                dogRepository.InsertRecord<DogDto>("Dog", MapModelToDto(model));

                return RedirectToAction("Index", "Home");
            }
            else
            {
                //Repopulate color and breed list for view
                ViewData["dogColors"] = dogRepository.GetDogColors();
                ViewData["dogBreeds"] = dogRepository.GetDogBreedList();
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