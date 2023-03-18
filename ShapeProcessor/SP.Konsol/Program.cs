using System;
using SP.Lib;

namespace SP.Konsol
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            ShapeFileService shapeFileService = new ShapeFileService();

            shapeFileService.LoadShapeFile(@"D:\Projects\GISWorks\ShapeProcessor\exShape\bolgeler.shp");
        }
    }
}