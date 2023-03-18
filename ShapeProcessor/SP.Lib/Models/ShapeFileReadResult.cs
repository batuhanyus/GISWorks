using System.Collections.Generic;
using System.IO;
using System.Linq;
using EGIS.ShapeFileLib;

namespace SP.Lib.Models
{
    public class ShapeFileReadResult
    {
        public List<ShapeFilePolygon> Polygons = new List<ShapeFilePolygon>();

        public void Finalise()
        {
            foreach (var polygon in Polygons)
            {
                polygon.CalculateCorners();
            }
        }
    }
}