using DogMatchMaker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DogMatchMaker.Data
{
    public class DogRepository
    {
        private static List<DogViewModel> Dogs = new List<DogViewModel>();
        private static int count;
        public DogRepository()
        {
            Dogs = new List<DogViewModel>();
            count = 1;
            Dogs.Add(
                new DogViewModel()
                {
                    Name = "Tucker",
                    Id = count,
                    Birthday = new DateTime(2020, 05, 08),
                    Breed = "Golden Retriever",
                    Color = "Golden",
                    Gender= "Male",
                    ContactEmail = "ctek@uwm.com",
                    ContactName = "Jane Doe",
                    GoodWithKids = true,
                    GoodWithCats = true,
                    CrateTrained = false,
                    PottyTrained = true,
                    Description = "A very good boy",
                    CityLocation = "Troy",
                    StateLocation = "MI",
                    Zipcode = "48084",
                    StreetAddress = "1820 Axtell Dr"
                });
            count++;

            Dogs.Add(
                new DogViewModel()
                {
                    Name = "Buck",
                    Id = count++,
                    Birthday = new DateTime(2020, 06, 12),
                    Breed = "Labrador Retriever",
                    Gender = "Male",
                    Color = "Chocolate",
                    ContactEmail = "ctek@uwm.com",
                    ContactName = "Jane Doe",
                    GoodWithKids = true,
                    GoodWithCats = true,
                    CrateTrained = true,
                    PottyTrained = true,
                    Description = "A very good boy",
                    CityLocation = "Ann Arbor",
                    StateLocation = "MI",
                    Zipcode = "48108",
                    StreetAddress = "4718 Wildflower Ct"
                });
            Dogs.Add(
               new DogViewModel()
               {
                   Name = "Cooper",
                   Id = count++,
                   Birthday = new DateTime(2020, 06, 01),
                   Breed = "Corgi",
                   Gender = "Male",
                   Color = "Red and White",
                   ContactEmail = "ctek@uwm.com",
                   ContactName = "Jane Doe",
                   GoodWithKids = true,
                   GoodWithCats = true,
                   CrateTrained = false,
                   PottyTrained = true,
                   Description = "A very good boy",
                   CityLocation = "Pontiac",
                   StateLocation = "MI",
                   Zipcode = "48341",
                   StreetAddress = "585 S Blvd E"
               });
            Dogs.Add(
               new DogViewModel()
               {
                   Name = "Rex",
                   Id = count++,
                   Birthday = new DateTime(2020, 05, 28),
                   Breed = "Boxer",
                   Gender = "Male",
                   Color = "Brindle",
                   ContactEmail = "ctek@uwm.com",
                   ContactName = "Jane Doe",
                   GoodWithKids = true,
                   GoodWithCats = true,
                   CrateTrained = false,
                   PottyTrained = true,
                   Description = "A very good boy",
                   CityLocation = "Toledo",
                   StateLocation = "OH",
                   StreetAddress = " 2801 Bancroft St",
                   Zipcode = "43606"
               });
            Dogs.Add(
               new DogViewModel()
               {
                   Name = "Pickles",
                   Id = count++,
                   Birthday = new DateTime(2020, 03, 29),
                   Breed = "German Sheperd",
                   Gender = "Female",
                   Color = "Black and Red",
                   ContactEmail = "ctek@uwm.com",
                   ContactName = "Jane Doe",
                   GoodWithKids = true,
                   GoodWithCats = false,
                   CrateTrained = true,
                   PottyTrained = false,
                   Description = "A very good girl",
                   CityLocation = "Troy",
                   StateLocation = "MI",
                   StreetAddress = "2645 Woodward Ave",
                   Zipcode = "48201"
               });
            Dogs.Add(
                new DogViewModel()
                {
                    Name = "Wanda",
                    Id = count++,
                    Birthday = new DateTime(2020, 06, 15),
                    Breed = "Yorkshire Terrier",
                    Color = "Black and Tan",
                    Gender = "Female",
                    ContactEmail = "ctek@uwm.com",
                    ContactName = "Jane Doe",
                    GoodWithKids = true,
                    GoodWithCats = false,
                    CrateTrained = false,
                    PottyTrained = false,
                    Description = "A very good girl",
                    CityLocation = "Detroit",
                    StateLocation = "MI",
                    StreetAddress = "1300 Campus Pkwy",
                    Zipcode = "48176"
                });
            
            Dogs.Add(
                new DogViewModel()
                {
                    Name = "Frosty",
                    Id = count++,
                    Birthday = new DateTime(2020, 04, 10),
                    Breed = "Samoyed",
                    Color = "White",
                    Gender="Female",
                    ContactEmail = "ctek@uwm.com",
                    ContactName = "Jane Doe",
                    GoodWithKids = true,
                    GoodWithCats = true,
                    CrateTrained = false,
                    PottyTrained = true,
                    Description = "A very good girl",
                    CityLocation = "Saline",
                    StateLocation = "MI",
                    StreetAddress = "1300 Campus Pkwy",
                    Zipcode = "48176"
                });
            Dogs.Add(
               new DogViewModel()
               {
                   Name = "Roo",
                   Id = count++,
                   Birthday = new DateTime(2020, 06, 15),
                   Breed = "Mixed",
                   Color = "Tan",
                   Gender = "Female",
                   ContactEmail = "ctek@uwm.com",
                   ContactName = "Jane Doe",
                   GoodWithKids = false,
                   GoodWithCats = false,
                   CrateTrained = true,
                   PottyTrained = false,
                   Description = "She is a huge couch potato.",
                   CityLocation = "Troy",
                   StateLocation = "MI",
                    Zipcode = "48084",
                   StreetAddress = "1830 Axtell Dr"
               });

        }

        public void Add(DogViewModel model)
        {
            model.Id = count++;
            Dogs.Add(model);
        }
        public int Count {
            get {
                return count;
                }
        }

        public DogViewModel GetDogById(int id)
        {
            if (id < 1)
            {
                return null;
            }

            foreach (DogViewModel dog in Dogs)
            {
                if (dog.Id == id)
                {
                    return dog;
                }
            }
            return null;
        }

        public List<DogViewModel> GetAllDogs()
        {
            return Dogs;
        }
    }
}  