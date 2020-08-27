using DogMatchMaker.Data;
using DogMatchMaker.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DogMatchMaker.Controllers
{
    public class HomeController : Controller
    {
        private static DogRepository dogRepository = new DogRepository();
        public ActionResult Index()
        {
            List<DogViewModel> dogs = dogRepository.GetAllDogs();
            ViewData["jsonDogs"] = JsonConvert.SerializeObject(dogs);
            Console.WriteLine(ViewData["jsonDogs"]);
            return View(dogs);
        }

        public ActionResult Home()
        {
            return View();
        }

        public ActionResult Details(int id)
        {
            DogViewModel dog = dogRepository.GetDogById(id);
            if(dog != null)
            {
                return View(dog);
            } else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult Create()
        {
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
                        model.Id = dogRepository.Count;

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
                dogRepository.Add(model);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View(model);
            }
        }
    }
}