using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CarsProj
{
    class Program
    {
        static void Main(string[] args)
        {
            var cars = ProcessCars("fuel.csv");
            var manufacturers = ProcessManufacturer("manufacturers.csv");

            var query =
                from car in cars
                group car by car.Manufacturer.ToUpper() into manufacturer
                orderby manufacturer.Key
                select manufacturer;

            var query2 =
                cars.GroupBy(c => c.Manufacturer.ToUpper()).OrderBy(g => g.Key);

            foreach (var group in query2.Take(10))
            {
                Console.WriteLine($"Manufacturer: {group.Key} | Car Count: {group.Count()}");
                foreach (var car in group.OrderByDescending(c => c.Combined).Take(2))
                {
                    Console.WriteLine($"\tCar: {car.Name} | Combined: {car.Combined}");
                }
            }
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
