using System;
using EGIS.ShapeFileLib;

namespace SP.Lib
{
    public class ShapeFileService
    {
        public void LoadShapeFile(string path)
        {
            ShapeFile shapeFile = new EGIS.ShapeFileLib.ShapeFile(path);
            DbfReader dbfReader = new DbfReader(System.IO.Path.ChangeExtension(path, "dbf"));

            ShapeFileEnumerator shapeFileEnumerator = shapeFile.GetShapeFileEnumerator();

            int currentIndex = 0;
            while (shapeFileEnumerator.MoveNext())
            {
                currentIndex++;
                // get the raw point data
                PointD[] points = shapeFileEnumerator.Current[0];
                //get the DBF record
                string[] fields = dbfReader.GetFields(shapeFileEnumerator.CurrentShapeIndex);
                if (shapeFile.ShapeType == EGIS.ShapeFileLib.ShapeType.PolyLine)
                {
                    Console.WriteLine("PolyLine at Index: " + currentIndex);

                    foreach (var p in points)
                    {
                        Console.WriteLine("X: " + p.X + " Y: " + p.Y);
                    }

                    //KmlWritePolyLine(xmlWriter, points);
                }
                else if (shapeFile.ShapeType == EGIS.ShapeFileLib.ShapeType.Polygon)
                {
                    Console.WriteLine("Polygon at Index: " + currentIndex);

                    foreach (var p in points)
                    {
                        Console.WriteLine("X: " + p.X + " Y: " + p.Y);
                    }


                    //KmlWritePolygon(xmlWriter, points, currentIndex, fields[0]);
                }
            }

            int i = 1;
        }
    }
}