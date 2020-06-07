using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace EmployeeProj
{
    class Program
    {
        static void Main(string[] args)
        {

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
