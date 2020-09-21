using DogMatchMaker.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DogMatchMaker.UI.Models
{
    public class DogViewModel
    {

        public DogViewModel(DogDto dog)
        {
            Id = dog.Id.ToString();
            Name = dog.Name;
            Birthday = dog.Birthday;
            Gender = dog.Gender;
            Breed = dog.Breed;
            Color = dog.Color;
            ContactEmail = dog.ContactEmail;
            ContactName = dog.ContactName;
            GoodWithCats = dog.GoodWithCats;
            GoodWithKids = dog.GoodWithKids;
            CrateTrained = dog.CrateTrained;
            Zipcode = dog.Zipcode;
            Description = dog.Description;
            StateLocation = dog.StateLocation;
            CityLocation = dog.CityLocation;
            StreetAddress = dog.StreetAddress;
        }

        public string Id { get; set; }
        [Required]
        [Display(Name = "Dog's Name")]
        public string Name { get; set; }
        [Required]
        [DateValidation(ErrorMessage = "Birthday must be in the past 20 years")]
        [Display(Name = "Dog's Birthday")]
        public DateTime Birthday { get; set; }
        [Required]
        [Display(Name = "Dog's Gender")]
        public string Gender { get; set; }
        [Required]
        public string Breed { get; set; }
        [Required]
        public string Color { get; set; }
        [Required]
        [RegularExpression("^[a-z0-9_\\+-]+(\\.[a-z0-9_\\+-]+)*@[a-z0-9-]+(\\.[a-z0-9]+)*\\.([a-z]{2,4})$", ErrorMessage = "Invalid email format.")]
        [Display(Name = "Contact Person Email")]
        public string ContactEmail { get; set; }
        [Required]
        [Display(Name = "Contact Person Name")]
        public string ContactName { get; set; }
        public string[] Breeds { get; set; }
        [Required]
        [Display(Name = "Is the dog good with kids?")]
        public bool GoodWithKids { get; set; }
        [Required]
        [Display(Name = "Is the dog good with Cats?")]
        public bool GoodWithCats { get; set; }
        [Required]
        [Display(Name = "Is the dog potty trained?")]
        public bool PottyTrained { get; set; }
        [Required]
        [Display(Name = "Is the dog crate trained?")]
        public bool CrateTrained { get; set; }
        [DataType(DataType.MultilineText)]
        [Required]
        public string Description { get; set; }
        [Required]
        [Display(Name = "City")]
        public string CityLocation { get; set; }
        [Required]
        [Display(Name = "Dog Street Address")]
        public string StreetAddress { get; set; }
        [Required]
        [Display(Name = "Dog ZipCode")]
        public string Zipcode { get; set; }
        [Required]
        [Display(Name = "State")]
        public string StateLocation { get; set; }

        public class DateValidation : ValidationAttribute
        {
            public override bool IsValid(object value)
            {
                return DateTime.TryParse(value.ToString(), out DateTime dt) ?
                dt < DateTime.Now && dt.Year > DateTime.Now.Year - 20 : false;
            }
        }
        public string Age
        {
            get
            {
                int monthAge = GetMonthsExisted();
                return string.Format(" {0} Years {1} Month", monthAge / 12, monthAge % 12);
            }
        }

        public string AgeGroup
        {
            get
            {
                int monthAge = GetMonthsExisted();
                if (monthAge / 12 < 1)
                {
                    return "Puppy";
                }
                else if (monthAge / 12 - 1 < 3)
                {
                    return "Young";
                }
                else if (monthAge / 12 - 1 < 8)
                {
                    return "Adult";
                }
                else
                {
                    return "Senior";
                }

            }
        }

        private int GetMonthsExisted()
        {
            double monthsBetween = DateTime.Now.Subtract(Birthday).Days / (365.25 / 12);
            return (int)monthsBetween;
        }

        public string PhotoPath
        {
            get
            {
                return $"{Name.ToLower()}.jpg";
            }
        }

    }
}