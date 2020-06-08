using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace CarsProj
{
    class Program
    {
        static void Main(string[] args)
        {
            CreateXML();
        }

        private static void CreateXML()
        {
            var records = ProcessCars("fuel.csv");

            var document = new XDocument();
            var cars = new XElement("Cars", records.Select(record => new XElement("Car",
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
