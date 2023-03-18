using System;
using System.Collections.Generic;
using System.Linq;
using EGIS.ShapeFileLib;

namespace SP.Lib.Models
{
    public class ShapeFilePolygon
    {
        public List<PointD> Points = new List<PointD>();

        public PointD UpperLeftCorner;
        public PointD LowerRightCorner;
        public PointD CenterPoint;


        public void CalculateCorners()
        {
            //Kill the 5th duplicate.
            var distinct = Points.Distinct().ToList();

            UpperLeftCorner = distinct.OrderBy(p => p.X).ThenByDescending(p => p.Y).First();
            LowerRightCorner = distinct.OrderByDescending(p => p.X).ThenBy(p => p.Y).First();

            CenterPoint = new PointD((UpperLeftCorner.X + LowerRightCorner.X) / 2,
                (UpperLeftCorner.Y + LowerRightCorner.Y) / 2);
        }

        
    }
}