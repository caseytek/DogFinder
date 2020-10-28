using System;
using System.Collections.Generic;

namespace DogMatchMaker.Data
{
    public interface IDogRepository
    {
        IEnumerable<string> DogBreeds { get; set; }
        IEnumerable<string> DogColors { get; set; }

        List<string> GetDogBreedList();
        void InsertRecord<T>(string table, T record);
        T LoadRecordById<T>(string table, Guid id);
        IEnumerable<T> LoadRecords<T>(string table);
    }
}