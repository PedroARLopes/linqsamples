using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace LinqSamples
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
                string path = "/Users/pedrolopes/Downloads";
                ShowFiles.WithoutLinq(path);
                Console.WriteLine(new String('*', 50));
                ShowFiles.WithLinq(path);
            */

            Func<int, int> square = x => x * x;
            Func<int, int, int> add = (x, y) =>
            {
                int sum = x + y;
                return sum;
            };
            Action<int> write = x => Console.WriteLine(x);

            write(square(add(3, 5)));

            var developers = new Employee[]
            {
                new Employee { Id = 1, Name = "Ricky"},
                new Employee { Id = 2, Name = "Morty"},
                new Employee { Id = 3, Name = "Sam"}
            };

            var sales = new List<Employee>()
            {
                new Employee { Id = 3, Name = "Summer" }
            };

            var queryMethod = developers.Where(e => e.Name.Length == 5).OrderBy(e => e.Name).ToList();

            var querySytax = from dev in developers
                             where dev.Name.Length == 5
                             orderby dev.Name
                             select dev;

            foreach (var e in querySytax)
                Console.WriteLine($"Id:{e.Id}, Name:{e.Name}");
        }
    }
}
