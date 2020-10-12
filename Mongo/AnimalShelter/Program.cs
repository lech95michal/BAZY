using System;
using System.Diagnostics;
using System.Threading;
using AnimalShelter.Models;
using MongoDB.Driver;
using System.Linq;

namespace AnimalShelter
{
    public class Program
    {
        public static void Main(string[] args)
        {
            #region Config

            var config = new MongoClientSettings
            {
                Servers = new[]
                {
                    new MongoServerAddress("node1", 27017),
                    new MongoServerAddress("node2", 27017)
                },
                ConnectionMode = ConnectionMode.ReplicaSet,
                ReplicaSetName = "rs"
            };

            var client = new MongoClient(config);
            var db = client.GetDatabase("shelter");
            var adoptions = db.GetCollection<Adoption>("adoptions")
                .AsQueryable(new AggregateOptions
                    {
                        AllowDiskUse = true
                    }
                );

            #endregion

            #region FirstQuery

            var timerOne = Stopwatch.StartNew();
            var queryOne = adoptions.GroupBy(x => new
                    {
                        x.Client.Pesel,
                        x.Client.FirstName,
                        x.Client.LastName
                    }
                )
                .Select(x => new
                    {
                        Client = x.Key.Pesel + " | " + x.Key.FirstName + " " + x.Key.LastName,
                        AdpotionCount = x.Count()
                    }
                )
                .OrderByDescending(x => x.AdpotionCount)
                .ToList();
            
            timerOne.Stop();

            foreach (var item in queryOne)
            {
                Console.WriteLine($"Klient: {item.Client} | Adopcje: {item.AdpotionCount}");
            }
           
            Console.WriteLine("Czas wykonania: " + timerOne.ElapsedMilliseconds);
            
            Thread.Sleep(2000);

            #endregion

            #region SecondQuery

            var timerTwo = Stopwatch.StartNew();
            var queryTwo = adoptions.GroupBy(x => new
                    {
                        x.Employee.FirstName,
                        x.Employee.LastName
                    }
                )
                .Select(x => new
                    {
                        Employee = x.Key.FirstName + " " + x.Key.LastName,
                        AdpotionCount = x.Count()
                    }
                )
                .OrderByDescending(x => x.AdpotionCount)
                .ToList();
            
            timerTwo.Stop();

            foreach (var item in queryTwo)
            {
                Console.WriteLine($"Pracownik: {item.Employee} | Adopcje: {item.AdpotionCount}");
            }
           
            Console.WriteLine("Czas wykonania: " + timerTwo.ElapsedMilliseconds);
            
            Thread.Sleep(2000);

            #endregion

            #region ThirdQuery

            var timerThree = Stopwatch.StartNew();
            var queryThree = adoptions.GroupBy(x => new
                    {
                        x.Date
                    }
                )
                .Select(x => new
                    {
                        Date = x.Key,
                        Client = x.First().Client.FirstName + " " + x.First().Client.LastName,
                        Employee = x.First().Employee.FirstName + " " + x.First().Employee.LastName,
                        Animal = x.First().Animal.Name,
                        Remarks = x.First().Remarks
                    }
                )
                .OrderBy(x => x.Date)
                .ToList();
            
            timerThree.Stop();

            foreach (var item in queryThree)
            {
                Console.WriteLine($"Data: {item.Date.Date.ToString("dd-MM-yyyy")} | Klient: {item.Client} | Pracownik: {item.Employee} | Zwierze: {item.Animal} | Uwagi: {item.Remarks}");
            }
           
            Console.WriteLine("Czas wykonania: " + timerThree.ElapsedMilliseconds);
            
            Thread.Sleep(2000);

            #endregion
        }
    }
}
