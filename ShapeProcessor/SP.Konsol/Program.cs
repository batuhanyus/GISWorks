using System;
using SP.Lib;

namespace SP.Konsol
{
    class Program
    {
        static void Main(string[] args)
        {
            ShapeFileService shapeFileService = new ShapeFileService();
            var result = shapeFileService.ReadShapeFile(@"D:\Projects\GISWorks\ShapeProcessor\exShape\bolgeler.shp");
            shapeFileService.WriteAsCSV(@"D:\Projects\GISWorks\ShapeProcessor\exShape\bolgeler.csv", result);

            Console.ReadKey();
        }
    }
}