using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DogMatchMaker.Models
{
    public class DogViewModel
    {

        public int Id { get; set; }
        [Required]
        [Display(Name = "Dog's Name")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Dog's Birthday")]
        public DateTime Birthday { get; set; }
        [Required]
        [Display(Name = "Dog's Gender")]
        public string Gender { get; set; }
        public string Breed { get; set; }
        public string Color { get; set; }
        [Display(Name = "Contact Person Email")]
        public string ContactEmail { get; set; }
        [Display(Name = "Contact Person Name")]
        public string ContactName { get; set; }
        public string[] Breeds { get; set; }
        [Display(Name = "Is the dog good with kids?")]
        public bool GoodWithKids { get; set; }
        [Display(Name = "Is the dog good with Cats?")]
        public bool GoodWithCats { get; set; }
        [Display(Name = "Is the dog potty trained?")]
        public bool PottyTrained { get; set; }
        [Display(Name = "Is the dog crate trained?")]
        public bool CrateTrained { get; set; }
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        [Display(Name = "City")]
        public string CityLocation { get; set; }
        [Required]
        [Display(Name = "Dog Street Address")]
        public string StreetAddress { get; set; }
        [Required]
        [Display(Name = "Dog ZipCode")]
        public string Zipcode { get; set; }
        [Display(Name = "State")]
        public string StateLocation { get; set; }
        public string Age
        {
            get { 
            DateTime Age = GetAgeInDateTime();
            return string.Format(" {0} Years {1} Month", Age.Year - 1, Age.Month - 1);
            }
        }

        public string AgeGroup
        {
            get
            {
            DateTime Age = GetAgeInDateTime();
            if(Age.Year-1 < 1)
            {
                return "Puppy";
            } else if (Age.Year - 1 < 3)
            {
                return "Young";
            } else if(Age.Year - 1 < 8)
            {
                return "Adult";
            }
            else
            {
                return "Senior";
            }

            }
        }

        private DateTime GetAgeInDateTime()
        {
            DateTime PresentYear = DateTime.Now;
            TimeSpan ts = PresentYear - Birthday;
            DateTime Age = DateTime.MinValue.AddDays(ts.Days);
            return Age;
        }

        public string PhotoPath
        {
            get
            {
                return $"{Name.ToLower()}{Id}.jpg";
            }
        }
        
    }
}