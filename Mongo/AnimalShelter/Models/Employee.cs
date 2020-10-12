using MongoDB.Bson.Serialization.Attributes;

namespace AnimalShelter.Models
{
    public class Employee
    {
        [BsonElement("idEmployee")] 
        public int IdEmployee { get; set; }
        [BsonElement("firstName")]
        public string FirstName { get; set; }
        [BsonElement("lastName")]
        public string LastName { get; set; }

        public Employee() { }

        public Employee(int idEmployee, string firstName, string lastName)
        {
            IdEmployee = idEmployee;
            FirstName = firstName;
            LastName = lastName;
        }
    }
}