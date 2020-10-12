using MongoDB.Bson.Serialization.Attributes;

namespace AnimalShelter.Models
{
    public class Animal
    {
        [BsonElement("idAnimal")] 
        public int IdAnimal { get; set; }
        [BsonElement("name")]
        public string Name { get; set; }
        [BsonElement("type")]
        public string Type { get; set; }
        [BsonElement("age")]
        public int Age { get; set; }

        public Animal() { }

        public Animal(int idAnimal, string name, string type, int age)
        {
            IdAnimal = idAnimal;
            Name = name;
            Type = type;
            Age = age;
        }
    }
}