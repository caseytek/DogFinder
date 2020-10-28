using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossCutting
{
    public class DogDto
    {
        [BsonId]
        public Guid Id { get; set; }
        public string Name { get; set; }

        public DateTime Birthday { get; set; }

        public string Gender { get; set; }

        public string Breed { get; set; }

        public string Color { get; set; }

        public string ContactEmail { get; set; }

        public string ContactName { get; set; }
        public string[] Breeds { get; set; }

        public bool GoodWithKids { get; set; }

        public bool GoodWithCats { get; set; }

        public bool PottyTrained { get; set; }

        public bool CrateTrained { get; set; }

        public string Description { get; set; }

        public string CityLocation { get; set; }

        public string StreetAddress { get; set; }

        public string Zipcode { get; set; }

        public string StateLocation { get; set; }

    }
}
