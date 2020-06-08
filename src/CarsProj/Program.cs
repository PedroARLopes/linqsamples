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
                from manufacturer in manufacturers
                join car in cars on manufacturer.Name equals car.Manufacturer
                    into carGroup
                orderby manufacturer.Name
                select new
                {
                    Manufacturer = manufacturer,
                    Cars = carGroup,
                } into result
                group result by result.Manufacturer.Headquarters;

            var query2 =
                manufacturers.GroupJoin(
                    cars,
                    m => m.Name,
                    c => c.Manufacturer,
                    (m, g) => new
                    {
                        Manufacturer = m,
                        Cars = g
                    }).GroupBy(m => m.Manufacturer.Headquarters);

            foreach (var group in query)
            {
                Console.WriteLine($"Headquarters: {group.Key}");
                foreach (var car in group.SelectMany(g => g.Cars).OrderByDescending(c => c.Combined).Take(3))
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
