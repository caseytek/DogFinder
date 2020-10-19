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
            List<string> dogColors = dogRepository.GetDogColors();
            List<string> dogBreeds = dogRepository.GetDogBreedList();
            ViewData["dogColors"] = dogColors;
            ViewData["dogBreeds"] = dogBreeds;
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
                    return View(model);
                }
            else
            {
                //If file was empty then this message will be displayed if model is not valid
                ViewBag.Message = "You have not specified a file.";
            }

            if (ModelState.IsValid)
            {
                if(model.Breeds != null)
                {
                    model.Breed = "Mixed Breed";
                }
                //dogRepository.Add(model);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                List<string> dogColors = DogRepository.DogColors;
                List<string> dogBreeds = DogRepository.DogBreeds;
                ViewData["dogColors"] = dogColors;
                ViewData["dogBreeds"] = dogBreeds;
                return View(model);
            }
        }
    }
}