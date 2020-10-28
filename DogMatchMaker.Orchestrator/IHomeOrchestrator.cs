using System;
using System.Collections.Generic;
using System.Web;
using CrossCutting;

namespace DogMatchMaker.Orchestrator
{
    public interface IHomeOrchestrator
    {
        IEnumerable<DogDto> GetAllDogs();
        IEnumerable<string> GetDogBreeds();
        DogDto GetDogById(Guid id);
        IEnumerable<string> GetDogColors();
        bool InsertDog(DogDto dog);
        List<string> ValidateDogPhoto(HttpPostedFileBase file, string path);
    }
}