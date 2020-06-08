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
                group car by car.Manufacturer into carGroup
                select new
                {
                    Name = carGroup.Key,
                    Max = carGroup.Max(c => c.Combined),
                    Min = carGroup.Min(c => c.Combined),
                    Avg = carGroup.Average(c => c.Combined)
                } into result
                orderby result.Avg descending
                select result;

            var query2 =
                cars.GroupBy(c => c.Manufacturer)
                .Select(g =>
                {
                    var result = g.Aggregate(
                        new CarStatistics(),
                        (acc, c) => acc.Accumulate(c),
                        acc => acc.Compute());
                    return new
                    {
                        Name = g.Key,
                        Avg = result.Average,
                        Min = result.Min,
                        Max = result.Max
                    };
                }).OrderByDescending(r => r.Avg);

            foreach (var result in query2)
            {
                Console.WriteLine($"Car: {result.Name,-30} Max: {result.Max} | Min: {result.Min} | Avg: {result.Avg:N1}");
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
