using System;
using MongoDB.Bson.Serialization.Attributes;

namespace AnimalShelter.Models
{
    public class Adoption
    {
        [BsonElement("client")]
        public Client Client { get; set; }
        [BsonElement("employee")]
        public Employee Employee { get; set; }
        [BsonElement("animal")]
        public Animal Animal { get; set; }
        [BsonElement("date")]
        public DateTime Date { get; set; }
        [BsonElement("remarks")]
        public string Remarks { get; set; }

        public Adoption() { }

        public Adoption(Client client, Employee employee, Animal animal, DateTime date, string remarks)
        {
            Client = client;
            Employee = employee;
            Animal = animal;
            Date = date;
            Remarks = remarks;
        }
    }
}