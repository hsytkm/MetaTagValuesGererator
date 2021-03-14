using CsharpSourceGeneratorSamples.Metas;
using System;
using System.Reflection;

namespace CsharpSourceGeneratorSamples
{
    class Program
    {
        static void Main(string[] args)
        {
            var filePath = @"C:\data\official\Panasonic_DMC-GF7.jpg";
            //filePath = @"C:\data\ext\Image1.bmp";

            Console.WriteLine($"ImagePath: {filePath}");
            var shelf = new MetaShelf();
            var book = shelf.GetOrAdd(filePath);

            //Console.WriteLine($"Fnumber: {book.Values.Fnumber}");
            //Console.WriteLine($"IsoSpeed: {book.Values.IsoSpeed}");

            var obj = book.Values;
            var properties = obj.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
            foreach (var pi in properties)
            {
                var ret = pi.GetGetMethod()?.Invoke(obj, null)?.ToString() ?? "null";
                Console.WriteLine($"{pi.Name} : {ret}");
            }

        }
    }
}
