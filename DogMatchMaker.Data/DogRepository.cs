using CrossCutting;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;


namespace DogMatchMaker.Data
{
    public class DogRepository
    {
        private IMongoDatabase db;
        public static List<string> DogColors { get; set; }
        public static List<string> DogBreeds { get; set; }

        public DogRepository(string database)
        {
            var client = new MongoClient(
                "mongodb+srv://tekcasey:mongodb@caseyteknology.bl2ur.azure.mongodb.net/"
                + "DogMatchMaker?retryWrites=true&w=majority");
            db = client.GetDatabase(database);

        }

        public T LoadRecordById<T>(string table, Guid id)
        {
            var collection = db.GetCollection<T>(table);
            var filter = Builders<T>.Filter.Eq("Id", id);

            return collection.Find(filter).First();

        }
        public void InsertRecord<T>(string table, T record)
        {
            var collection = db.GetCollection<T>(table);
            collection.InsertOne(record);
        }

        public List<T> LoadRecords<T>(string table)
        {
            var collection = db.GetCollection<T>(table);
            return collection.Find(new BsonDocument()).ToList();

        }

        public List<DogDto> LoadAllDogs(string table)
        {
            var collection = db.GetCollection<DogDto>(table);
            return collection.Find(new BsonDocument()).ToList();
        }

        public List<string> GetDogColors()
        {
            var collection = db.GetCollection<DogColor>("DogColors");
            List<DogColor> newCollection = collection.Find(new BsonDocument()).ToList();
            List<string> dogColors = new List<string>();
            newCollection.ForEach(c => { dogColors.Add(c.Color); });
            return dogColors;
        }

        public List<string> GetDogBreedList()
        {
            List<string> dogBreeds = new List<string>();
            var client = new RestClient("https://api.thedogapi.com/v1/breeds")
            {
                Timeout = -1
            };
            var request = new RestRequest(Method.GET);
            request.AddHeader("Content-Type", "application/json");
            request.AlwaysMultipartFormData = true;
            IRestResponse response = client.Execute(request);

            dynamic dynJson = JsonConvert.DeserializeObject(response.Content);

            foreach (dynamic breed in dynJson)
            {
                string name = breed.name;
                dogBreeds.Add(name);
            }
            dogBreeds.Add("Labradoodle");
            dogBreeds.Add("Goldendoodle");
            dogBreeds.Add("Mixed Breed");
            dogBreeds.Add("Other");
            dogBreeds.Sort();
            return dogBreeds;
        }
    }
}
