using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using System.Collections.Specialized;
using System.Drawing;
using EGIS.ShapeFileLib;

namespace Example4
{

    delegate void ProgressDelegate(int current, int max);
    /// <summary>
    /// Utility class with static methods to create a Google Earth KML file
    /// </summary>
    class KmlGenUtil
    {
        private KmlGenUtil()
        {
        }

        #region Googgle Earth KML creation methods

        /// <summary>
        /// Creates a google earth KML file
        /// </summary>
        /// <param name="kmlPath">The path to the kml file to be created</param>
        /// <param name="shapeFile">The ShapeFile to read the gis data from</param>
        /// <param name="dbfReader">The ShapeFile's DbfReader</param>
        public static void CreateKmlFile(string kmlPath, EGIS.ShapeFileLib.ShapeFile shapeFile, EGIS.ShapeFileLib.DbfReader dbfReader, ProgressDelegate progressDelegate)        
        {
            if (!(shapeFile.ShapeType == EGIS.ShapeFileLib.ShapeType.PolyLine || shapeFile.ShapeType == EGIS.ShapeFileLib.ShapeType.Polygon)) throw new System.NotSupportedException(shapeFile.ShapeType.ToString() + " ShapeType not supported");
            XmlTextWriter xmlWriter = new XmlTextWriter(kmlPath, System.Text.Encoding.UTF8);
            try
            {                
                // Setup the opening KML xml structure
                xmlWriter.Formatting = Formatting.Indented;
                xmlWriter.WriteStartDocument();
                xmlWriter.WriteStartElement("kml", "http://www.opengis.net/kml/2.2");

                xmlWriter.WriteStartElement("Document");

                xmlWriter.WriteStartElement("name");
                xmlWriter.WriteString(System.IO.Path.GetFileName(kmlPath));
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement("description");
                xmlWriter.WriteString("demo kml file generated from a shapefile");
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement("Style");
                xmlWriter.WriteAttributeString("id", "mystyle");

                xmlWriter.WriteStartElement("LineStyle");
                xmlWriter.WriteStartElement("color");
                xmlWriter.WriteString("7fd011ff");
                xmlWriter.WriteEndElement();
                xmlWriter.WriteStartElement("width");
                xmlWriter.WriteString("4");
                xmlWriter.WriteEndElement();
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement("PolyStyle");
                xmlWriter.WriteStartElement("color");
                xmlWriter.WriteString("7fff1faa");
                xmlWriter.WriteEndElement();
                xmlWriter.WriteStartElement("colorMode");
                xmlWriter.WriteString("random");
                xmlWriter.WriteEndElement();
                xmlWriter.WriteEndElement();

                xmlWriter.WriteEndElement(); //close Style tag

                //obtain a ShapeFileEnumerator and read all of the shapefile records
                EGIS.ShapeFileLib.ShapeFileEnumerator sfEnum = shapeFile.GetShapeFileEnumerator();
                int currentIndex = 0;
                
                while (sfEnum.MoveNext())
                {
                    currentIndex++;
                    // get the raw point data
                    PointD[] points = sfEnum.Current[0];
                    //get the DBF record
                    string[] fields = dbfReader.GetFields(sfEnum.CurrentShapeIndex);
                    if (shapeFile.ShapeType == EGIS.ShapeFileLib.ShapeType.PolyLine)
                    {
                        KmlWritePolyLine(xmlWriter, points);
                    }
                    else if (shapeFile.ShapeType == EGIS.ShapeFileLib.ShapeType.Polygon)
                    {
                        KmlWritePolygon(xmlWriter, points, currentIndex, fields[0]);
                    }
                    if (progressDelegate != null) progressDelegate(currentIndex, sfEnum.TotalRecords);
                }
                
                
                xmlWriter.WriteEndElement(); //close the Document tag			
                xmlWriter.WriteEndElement(); //close kml tag
                xmlWriter.WriteEndDocument();                                
            }
            finally
            {
                xmlWriter.Close();                
            }
        }

        private static void KmlWritePolygon(XmlTextWriter w, PointD[] points, int polygonCount, string desc)
        {
            w.WriteStartElement("Placemark");

            w.WriteStartElement("name");
            w.WriteString("polygon " + polygonCount);
            w.WriteEndElement();

            w.WriteStartElement("description");
            w.WriteString(desc);
            w.WriteEndElement();
            
            w.WriteStartElement("styleUrl");
            w.WriteString("#mystyle");
            w.WriteEndElement();

            w.WriteStartElement("Polygon");

            w.WriteStartElement("altitudeMode");
            w.WriteString("clampToGround");
            w.WriteEndElement();

            w.WriteStartElement("outerBoundaryIs");
            w.WriteStartElement("LinearRing");
            
            //now write the points
            w.WriteStartElement("coordinates");
            w.WriteString(GetKmlCoordsString(points));
            w.WriteEndElement();

            w.WriteEndElement(); //close linearRing
            w.WriteEndElement(); //close outerboundaryIs
            w.WriteEndElement(); //close polygon tag						
            w.WriteEndElement(); //close Placemark tag
        }


        private static void KmlWritePolyLine(XmlTextWriter w, PointD[] points)
        {
            w.WriteStartElement("Placemark");

            w.WriteStartElement("name");
            w.WriteString("path");
            w.WriteEndElement();

            w.WriteStartElement("description");
            w.WriteString("Path");
            w.WriteEndElement();

            w.WriteStartElement("styleUrl");
            w.WriteString("#mystyle");
            w.WriteEndElement();

            w.WriteStartElement("LineString");
            w.WriteStartElement("tessellate");
            w.WriteString("1");
            w.WriteEndElement();
            w.WriteStartElement("altitudeMode");
            w.WriteString("clampToGround");
            w.WriteEndElement();

            //now write the paths coordinates
            w.WriteStartElement("coordinates");
            w.WriteString(GetKmlCoordsString(points));
            w.WriteEndElement();
            w.WriteEndElement(); //close LineString tag						
            w.WriteEndElement(); //close Placemark tag

        }

    
        private static string GetKmlCoordsString(PointD[] points)
        {
            StringBuilder sb = new StringBuilder();
            
            for (int i = 0; i < points.Length; i++)
            {
                sb.AppendLine(string.Format("{0},{1}", points[i].X, points[i].Y));       
            }
            return sb.ToString();
        }


        #endregion
    }
}
