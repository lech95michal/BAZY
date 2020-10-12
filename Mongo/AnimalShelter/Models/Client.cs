using MongoDB.Bson.Serialization.Attributes;

namespace AnimalShelter.Models
{
    public class Client
    { 
        [BsonElement("pesel")] 
        public string Pesel { get; set; }
        [BsonElement("firstName")]
        public string FirstName { get; set; }
        [BsonElement("lastName")]
        public string LastName { get; set; }

        public Client() { }

        public Client(string pesel, string firstName, string lastName)
        {
            Pesel = pesel;
            FirstName = firstName;
            LastName = lastName;
        }
    }
}