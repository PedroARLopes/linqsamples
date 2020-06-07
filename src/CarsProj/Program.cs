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
                orderby car.Combined descending, car.Name ascending
                select car;

            foreach (var car in query2.Take(10))
                Console.WriteLine($"{car.Name} : {car.Combined}");
        }

        private static List<Car> ProcessFile(string path)
        {
            return
                File.ReadAllLines(path)
                .Skip(1)
                .Where(l => l.Length > 0)
                .Select(Car.ParseFromCsv)
                .ToList();
        }

        private static List<Car> ProcessFileQuerySyntax(string path)
        {
            var query = from line in File.ReadAllLines(path).Skip(1)
                        where line.Length > 1
                        select Car.ParseFromCsv(line);
            return query.ToList();
        }
    }
}
