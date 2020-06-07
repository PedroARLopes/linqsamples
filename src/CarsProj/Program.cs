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
            var cars = ProcessFile("fuel.csv");
            var query = cars.OrderByDescending(c => c.Combined).ThenBy(c => c.Name);

            var query2 =
                from car in cars
                where car.Manufacturer == "BMW" && car.Year == 2016
                orderby car.Combined descending, car.Name ascending
                select car;

            // Iterate throught he sequence inside the sequence
            var result = cars
            .SelectMany(c => c.Name)
            .OrderBy(c => c);

            foreach (var name in result)
            {
                Console.WriteLine(name);
            }

            // foreach (var car in query2.Take(10))
            //     Console.WriteLine($"{car.Name} : {car.Combined}");
        }

        private static List<Car> ProcessFile(string path)
        {
            return
                File.ReadAllLines(path)
                .Skip(1)
                .Where(l => l.Length > 0)
                .ToCar()
                .ToList();
        }
    }
}
