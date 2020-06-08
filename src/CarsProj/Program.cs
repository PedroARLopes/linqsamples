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

            var query = cars.OrderByDescending(c => c.Combined).ThenBy(c => c.Name);

            var query2 =
                from car in cars
                join manufacturer in manufacturers
                    on new { car.Manufacturer, car.Year }
                    equals
                    new { Manufacturer = manufacturer.Name, manufacturer.Year }
                orderby car.Combined descending, car.Name ascending
                select new
                {
                    manufacturer.Headquarters,
                    car.Name,
                    car.Combined,
                };

            var query3 =
                cars.Join(
                    manufacturers,
                    c => new { c.Manufacturer, c.Year },
                    m => new { Manufacturer = m.Name, m.Year },
                    (c, m) => new
                    {
                        m.Headquarters,
                        c.Name,
                        c.Combined,
                    })
                    .OrderByDescending(c => c.Combined)
                    .ThenBy(c => c.Name);

            foreach (var car in query2.Take(10))
                Console.WriteLine($"{car.Headquarters} : {car.Name} : {car.Combined}");
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
