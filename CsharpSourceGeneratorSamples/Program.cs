using CsharpSourceGeneratorSamples.Metas;
using System;

namespace CsharpSourceGeneratorSamples
{
    class Program
    {
        static void Main(string[] args)
        {
            var filePath = @"C:\data\ext\Image1.jpg";
            //filePath = @"C:\data\ext\Image1.bmp";

            Console.WriteLine($"ImagePath: {filePath}");
            var shelf = new MetaShelf();
            var book = shelf.GetOrAdd(filePath);

            Console.WriteLine($"Fnumber: {book.Fnumber}");

        }
    }
}
