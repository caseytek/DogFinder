using CrossCutting;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DogMatchMaker.Data
{
    public class DogRepository : IDogRepository
    {
        private IMongoDatabase db;
        public IEnumerable<string> DogColors { get; set; }
        public IEnumerable<string> DogBreeds { get; set; }

        public DogRepository(string databaseName)
        {
            var client = new MongoClient(
                "mongodb+srv://tekcasey:mongodb@caseyteknology.bl2ur.azure.mongodb.net/"
                + "DogMatchMaker?retryWrites=true&w=majority");
            db = client.GetDatabase(databaseName);
            DogColors = LoadRecords<DogColor>("DogColors").Select(c => { return c.Color; });
            DogBreeds = GetDogBreedList();
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

        public IEnumerable<T> LoadRecords<T>(string table)
        {
            var collection = db.GetCollection<T>(table);
            return collection.Find(new BsonDocument()).ToList();

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
