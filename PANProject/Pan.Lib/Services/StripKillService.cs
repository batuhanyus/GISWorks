using Pan.Lib.Models;

namespace Pan.Lib.Services;

public class StripKillService
{
    public List<PanStripItem> KillStrips(List<PanStripItem> panStripItems, CropZone cropZone)
    {
        //Strategy here is to determine leftmost and rightmost strips which includes one of the crop zone corners.
        //After these are decided, we kill strips which are outside of these bounds.

        List<PanStripItem> alives = new List<PanStripItem>();

        bool[] includesCorner = new bool[panStripItems.Count];

        for (int i = 0; i < panStripItems.Count; i++)
        {
            PanStripItem panStripItem = panStripItems[i];

            if (IsPointInsideTrapezoid(cropZone.UpperLeftVertex, panStripItem.Vertices)
                || IsPointInsideTrapezoid(cropZone.UpperRightVertex, panStripItem.Vertices)
                || IsPointInsideTrapezoid(cropZone.LowerRightVertex, panStripItem.Vertices)
                || IsPointInsideTrapezoid(cropZone.LowerLeftVertex, panStripItem.Vertices))
            {
                includesCorner[i] = true;
            }
        }

        int leftmostStripIndex = -1;
        int rightmostStripIndex = -1;

        for (int i = 0; i < includesCorner.Length; i++)
        {
            if (includesCorner[i])
            {
                leftmostStripIndex = i;
                break;
            }
        }

        for (int i = includesCorner.Length - 1; i >= 0; i--)
        {
            if (includesCorner[i])
            {
                rightmostStripIndex = i;
                break;
            }
        }

        for (int i = leftmostStripIndex; i <= rightmostStripIndex; i++)
        {
            alives.Add(panStripItems[i]);
        }

        return alives;
    }

    //Trapezoid is our strip :)
    bool IsPointInsideTrapezoid(GeoVertex point, List<GeoVertex> stripVertices)
    {
        int intersections = 0;
        int count = stripVertices.Count;

        for (int i = 0; i < count; i++)
        {
            GeoVertex currentVertex = stripVertices[i];
            GeoVertex nextVertex = stripVertices[(i + 1) % count];

            if (currentVertex.LAT == nextVertex.LAT || point.LAT < Math.Min(currentVertex.LAT, nextVertex.LAT) ||
                point.LAT > Math.Max(currentVertex.LAT, nextVertex.LAT))
            {
                continue;
            }

            decimal xIntersection =
                (decimal)(point.LAT - currentVertex.LAT) * (decimal)(nextVertex.LON - currentVertex.LON) /
                (decimal)(nextVertex.LAT - currentVertex.LAT) + currentVertex.LON;

            if (xIntersection > point.LON)
            {
                intersections++;
            }
        }

        return (intersections % 2) == 1;
    }

    struct Point2D
    {
        public decimal X;
        public decimal Y;
    }
}