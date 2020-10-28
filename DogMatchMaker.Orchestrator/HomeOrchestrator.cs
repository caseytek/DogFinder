using CrossCutting;
using DogMatchMaker.Data;
using ImageProcessor;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;


namespace DogMatchMaker.Orchestrator
{
    public class HomeOrchestrator : IHomeOrchestrator
    {
        private IDogRepository dogRepository;

        public HomeOrchestrator(IDogRepository dogRepository)
        {
            this.dogRepository = dogRepository;
                //new DogRepository("DogMatchMaker");
        }
        public List<string> ValidateDogPhoto(HttpPostedFileBase file,
            string path)
        {
            List<string> errors = new List<string>();
            //Only validate file if they uploaded one
            if (file != null && file.ContentLength > 0)
            {
                try
                {
                    //Check that they uploaded a photo before trying to extract size data
                    if (file.ContentType == "image/jpeg" || file.ContentType == "image/png" || file.ContentType == "image/jpg")
                    {
                        int height;
                        int width;
                        //Using The ImageFactory Nuget package to obtain information
                        using (var imageFactory = new ImageFactory())
                        {
                            imageFactory
                                .Load(file.InputStream)
                                .AutoRotate();
                            height = imageFactory.Image.Height;
                            width = imageFactory.Image.Width;
                            //Check image size 
                        }
                        if (height < 200 || width < 200)
                        {
                            errors.Add("Image height and width must be more than 200 pixels.");
                            return errors;
                        } else
                        {
                           file.SaveAs(path);
                        }
                    }
                    else
                    {
                        errors.Add("File type not accepted.");
                        return errors;
                    }
                }
                catch (Exception)
                {
                    errors.Add("There was a problem saving your photo.");
                }
            }
            return errors;
        }

        public IEnumerable<DogDto> GetAllDogs()
        {
            return dogRepository.LoadRecords<DogDto>("Dog");
        }

        public DogDto GetDogById(Guid id)
        {
            return dogRepository.LoadRecordById<DogDto>("Dog", id);
        }

        public IEnumerable<string> GetDogColors()
        {
            return dogRepository.DogColors;
        }

        public IEnumerable<string> GetDogBreeds()
        {
            return dogRepository.DogBreeds;
        }

        public bool InsertDog(DogDto dog)
        {
            if (dog.Breeds != null)
            {
                dog.Breed = "Mixed Breed";
            }
            //Later return guid to determine success
            try
            {
                dogRepository.InsertRecord<DogDto>("Dog", dog);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
