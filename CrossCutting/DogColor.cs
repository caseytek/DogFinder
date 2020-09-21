using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossCutting
{
    public class DogColor
    {
       
            public ObjectId _id { get; set; }
            public string Color { get; set; }
    }
}
