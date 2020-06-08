using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace CarsProj
{
    class Program
    {
        static void Main(string[] args)
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<CarDb>());
            InsertData();
            QueryData2SQL();
        }

        private static void QueryData1()
        {
            var db = new CarDb();
            db.Database.Log = Console.WriteLine;

            var query = db.Cars
                .Where(c => c.Manufacturer == "BMW")
                .OrderByDescending(c => c.Combined)
                .ThenBy(c => c.Name)
                .Take(10)
                .Select(c => new { Name = c.Name.ToUpper() });
            
            foreach(var car in query)
            {
                Console.WriteLine($"{car.Name}");
            }
        }

        private static void QueryData2()
        {
            var db = new CarDb();
            db.Database.Log = Console.WriteLine;

            var query = db.Cars
                .GroupBy(c => c.Manufacturer)
                .Select(g => new
                {
                    Name = g.Key,
                    Cars = g.OrderByDescending(c => c.Combined).Take(2)
                });

            foreach (var group in query)
            {
                Console.WriteLine($"{group.Name}");
                foreach(var car in group.Cars)
                {
                    Console.WriteLine($"\t{car.Name} : {car.Combined}");
                }
            }
        }

        private static void QueryData2SQL()
        {
            var db = new CarDb();
            db.Database.Log = Console.WriteLine;

            var query =
                from car in db.Cars
                group car by car.Manufacturer into manufacturer
                select new
                {
                    Name = manufacturer.Key,
                    Cars = (
                        from car in manufacturer
                        orderby car.Combined descending
                        select car
                    )        
                };

            foreach (var group in query.Take(2))
            {
                Console.WriteLine($"{group.Name}");
                foreach (var car in group.Cars)
                {
                    Console.WriteLine($"\t{car.Name} : {car.Combined}");
                }
            }
        }

        private static void InsertData()
        {
            var cars = ProcessCars("fuel.csv");
            var db = new CarDb();

            if (!db.Cars.Any())
            {
                foreach ( var car in cars)
                {
                    db.Cars.Add(car);
                }
                db.SaveChanges();
            }
        }

        private static void QueryXML()
        {
            var document = XDocument.Load("fuel.xml");

            var query = document.Element("Cars").Elements("Car")
                .Where(e => e.Attribute("Manufacturer").Value == "BMW")
                .Select(e => e.Attribute("Name").Value);

            query.ToList().ForEach(e => Console.WriteLine(e));
        }

        private static void CreateXML()
        {
            var records = ProcessCars("fuel.csv");

            XNamespace ns = "http://pluralsight.com/cars/2016";

            var document = new XDocument();
            var cars = new XElement(ns + "Cars", records.Select(record => new XElement(ns + "Car",
                   new XAttribute("Name", record.Name),
                   new XAttribute("Combined", record.Combined),
                   new XAttribute("Manufacturer", record.Manufacturer)
               )));

            document.Add(cars);
            document.Save("fuel.xml");
        }

        private static List<Car> ProcessCars(string path)
        {
            return
                File.ReadAllLines(path)
                .Skip(1)
                .Where(l => l.Length > 0)
                .ToCar()
                .ToList();
        }

        private static List<Manufacturer> ProcessManufacturer(string path)
        {
            return
                File.ReadAllLines(path)
                .Skip(1)
                .Where(l => l.Length > 0)
                .ToManufacturer()
                .ToList();
        }
    }
}
