using System;

namespace FilesProj
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = "/Users/pedrolopes/Downloads";
            ShowFiles.WithoutLinq(path);
            Console.WriteLine(new String('*', 50));
            ShowFiles.WithLinq(path);
        }
    }
}
