using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using EGIS.ShapeFileLib;
using SP.Lib.Models;

namespace SP.Lib
{
    public class ShapeFileService
    {
        public ShapeFileReadResult ReadShapeFile(string path)
        {
            ShapeFile shapeFile = new EGIS.ShapeFileLib.ShapeFile(path);
            DbfReader dbfReader = new DbfReader(System.IO.Path.ChangeExtension(path, "dbf"));

            ShapeFileEnumerator shapeFileEnumerator = shapeFile.GetShapeFileEnumerator();

            ShapeFileReadResult result = new ShapeFileReadResult();

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
                    throw new Exception("PolyLine cinsinden SHAPE dosyası desteklenmiyor. İletişime geçiniz!");

                    Console.WriteLine("PolyLine at Index: " + currentIndex);

                    foreach (var p in points)
                    {
                        Console.WriteLine("X: " + p.X + " Y: " + p.Y);
                    }
                }
                else if (shapeFile.ShapeType == EGIS.ShapeFileLib.ShapeType.Polygon)
                {
                    Console.WriteLine("Polygon at Index: " + currentIndex);

                    ShapeFilePolygon polygon = new ShapeFilePolygon();

                    foreach (var p in points)
                    {
                        polygon.Points.Add(p);
                        Console.WriteLine("X: " + p.X + " Y: " + p.Y);
                    }

                    result.Polygons.Insert(currentIndex - 1, polygon);
                }
            }

            result.Finalise();
            return result;
        }

        public void WriteAsCSV(string path, ShapeFileReadResult result)
        {
            var sw = File.CreateText(path);

            sw.WriteLine("PolygonIndex,UpLeft LAT,UpLeft LON,LowRight LAT,LowRight LON,Center LAT,Center LON");

            for (int i = 0; i < result.Polygons.Count; i++)
            {
                var polygon = result.Polygons[i];

                string s = (i+1) + "," + polygon.UpperLeftCorner.Y + "," + polygon.UpperLeftCorner.X + "," +
                           polygon.LowerRightCorner.Y + "," + polygon.LowerRightCorner.X + "," +
                           polygon.CenterPoint.Y + "," + polygon.CenterPoint.X;

                sw.WriteLine(s);
            }

            sw.Flush();
            sw.Close();
        }
    }
}