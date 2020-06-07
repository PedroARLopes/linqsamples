using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;

namespace LinqSamples
{
    public static class ShowFiles
    {
        public static void WithLinq(string path)
        {
            Console.WriteLine("LINQ SQL like query");
            var query = from file in new DirectoryInfo(path).GetFiles()
                        orderby file.Length descending
                        select file;

            foreach (var file in query.Take(5))
                Console.WriteLine($"{file.Name,-35} : {file.Length:N0}");

            Console.WriteLine(new String('*', 50));
            Console.WriteLine("LINQ method chain query");
            var query2 = new DirectoryInfo(path).GetFiles().OrderByDescending(f => f.Length).Take(5);
            foreach (var file in query2)
                Console.WriteLine($"{file.Name,-35} : {file.Length:N0}");
        }

        public static void WithoutLinq(string path)
        {
            Console.WriteLine("Standard way without LINQ");
            DirectoryInfo directory = new DirectoryInfo(path);
            FileInfo[] files = directory.GetFiles();
            Array.Sort(files, new FileInfoComparer());


            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine($"{files[i].Name,-35} : {files[i].Length:N0}");
            }
        }

        public class FileInfoComparer : IComparer<FileInfo>
        {
            public int Compare([AllowNull] FileInfo x, [AllowNull] FileInfo y)
            {
                return y.Length.CompareTo(x.Length);
            }
        }
    }
}