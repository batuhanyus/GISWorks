using Pan.Lib.Models;

namespace Pan.Lib.Services;

public class CropService
{
    //Returns intersection points.
    public List<GeoVertex> CropStrip(PanStripItem uncroppedStrip, CropZone cropZone, out PanStripItem croppedStrip)
    {
        // Define the trapezoids. TODO sorted vertices maybe?
        Point bigOneTopLeft = new Point(uncroppedStrip.Vertices[0].LON, uncroppedStrip.Vertices[0].LAT);
        Point bigOneTopRight = new Point(uncroppedStrip.Vertices[1].LON, uncroppedStrip.Vertices[1].LAT);
        Point bigOneBottomRight = new Point(uncroppedStrip.Vertices[2].LON, uncroppedStrip.Vertices[2].LAT);
        Point bigOneBottomLeft = new Point(uncroppedStrip.Vertices[3].LON, uncroppedStrip.Vertices[3].LAT);

        Point smallOneTopLeft = new Point(cropZone.UpperLeftVertex.LON, cropZone.UpperLeftVertex.LAT);
        Point smallOneTopRight = new Point(cropZone.UpperRightVertex.LON, cropZone.UpperRightVertex.LAT);
        Point smallOneBottomRight = new Point(cropZone.LowerRightVertex.LON, cropZone.LowerRightVertex.LAT);
        Point smallOneBottomLeft = new Point(cropZone.LowerLeftVertex.LON, cropZone.LowerLeftVertex.LAT);

        Trapezoid bigOne = new Trapezoid(bigOneTopLeft, bigOneTopRight, bigOneBottomRight, bigOneBottomLeft);
        Trapezoid smallOne = new Trapezoid(smallOneTopLeft, smallOneTopRight, smallOneBottomRight, smallOneBottomLeft);


        // Find the intersection points
        List<Point> intersectionPoints = bigOne.GetIntersectionPoints(smallOne).ToList();

        // Create the new strip
        croppedStrip = new PanStripItem();
        croppedStrip.StripName = uncroppedStrip.StripName;
        croppedStrip.Vertices = new List<GeoVertex>();

        // Add the intersection points to the new strip
        foreach (Point intersectionPoint in intersectionPoints)
        {
            GeoVertex intersectionVertex = new GeoVertex();
            intersectionVertex.LON = (decimal)intersectionPoint.X;
            intersectionVertex.LAT = (decimal)intersectionPoint.Y;
            croppedStrip.Vertices.Add(intersectionVertex);
        }

        //Border strip treatment
        if (intersectionPoints.Count == 2)
        {
            foreach (var extra in BorderStripTreatment(uncroppedStrip, cropZone))
            {
                croppedStrip.Vertices.Add(extra);
            }
        }

        //Sort the vertices
        SortingService sortingService = new SortingService();
        croppedStrip.SortedVertices = sortingService.SortCoordinatesClockwise(croppedStrip.Vertices);

        //Return the intersection points
        return croppedStrip.SortedVertices;
    }

    //Returns two more intersection GeoVertex additions for cropped strip.
    List<GeoVertex> BorderStripTreatment(PanStripItem panStripItem, CropZone cropZone)
    {
        //Determine where this strip falls according to crop zone's center.
        cropZone.CalculateCenterVertex();

        bool isStripLeftOfCropZone = panStripItem.CenterVertex.LON < cropZone.CenterVertex.LON;

        List<GeoVertex> extras = new List<GeoVertex>();

        if (isStripLeftOfCropZone)
        {
            extras.Add(cropZone.UpperLeftVertex);
            extras.Add(cropZone.LowerLeftVertex);
        }
        else
        {
            extras.Add(cropZone.UpperRightVertex);
            extras.Add(cropZone.LowerRightVertex);
        }

        return extras;
    }
}

class Trapezoid
{
    public Point TopLeft { get; set; }
    public Point TopRight { get; set; }
    public Point BottomRight { get; set; }
    public Point BottomLeft { get; set; }

    public Trapezoid(Point topLeft, Point topRight, Point bottomRight, Point bottomLeft)
    {
        TopLeft = topLeft;
        TopRight = topRight;
        BottomRight = bottomRight;
        BottomLeft = bottomLeft;
    }

    public Point[] GetIntersectionPoints(Trapezoid other)
    {
        // Calculate the intersection points
        LineSegment[] edges1 = GetEdges();
        LineSegment[] edges2 = other.GetEdges();
        Point[] intersectionPoints = new Point[0];

        foreach (LineSegment edge1 in edges1)
        {
            foreach (LineSegment edge2 in edges2)
            {
                Point[] edgeIntersectionPoints = edge1.GetIntersectionPoints(edge2);
                intersectionPoints = intersectionPoints.Concat(edgeIntersectionPoints).ToArray();
            }
        }

        // Remove duplicate points
        intersectionPoints = intersectionPoints.Distinct().ToArray();

        return intersectionPoints;
    }

    private LineSegment[] GetEdges()
    {
        LineSegment[] edges = new LineSegment[4];
        edges[0] = new LineSegment(TopLeft, TopRight);
        edges[1] = new LineSegment(TopRight, BottomRight);
        edges[2] = new LineSegment(BottomRight, BottomLeft);
        edges[3] = new LineSegment(BottomLeft, TopLeft);
        return edges;
    }
}

class LineSegment
{
    public Point Start { get; set; }
    public Point End { get; set; }

    public LineSegment(Point start, Point end)
    {
        Start = start;
        End = end;
    }

    public Point[] GetIntersectionPoints(LineSegment other)
    {
        // Calculate the intersection point between two line segments
        Point[] intersectionPoints = new Point[0];

        decimal ua =
            ((other.End.X - other.Start.X) * (Start.Y - other.Start.Y) -
             (other.End.Y - other.Start.Y) * (Start.X - other.Start.X)) /
            ((other.End.Y - other.Start.Y) * (End.X - Start.X) - (other.End.X - other.Start.X) * (End.Y - Start.Y));
        decimal ub =
            ((End.X - Start.X) * (Start.Y - other.Start.Y) - (End.Y - Start.Y) * (Start.X - other.Start.X)) /
            ((other.End.Y - other.Start.Y) * (End.X - Start.X) - (other.End.X - other.Start.X) * (End.Y - Start.Y));
        if (ua >= 0 && ua <= 1 && ub >= 0 && ub <= 1)
        {
            decimal intersectionX = Start.X + ua * (End.X - Start.X);
            decimal intersectionY = Start.Y + ua * (End.Y - Start.Y);
            intersectionPoints = new Point[] { new Point(intersectionX, intersectionY) };
        }

        return intersectionPoints;
    }
}

class Point
{
    public decimal X { get; set; }
    public decimal Y { get; set; }

    public Point(decimal x, decimal y)
    {
        X = x;
        Y = y;
    }

    public override bool Equals(object obj)
    {
        if (!(obj is Point))
        {
            return false;
        }

        Point other = (Point)obj;
        return X == other.X && Y == other.Y;
    }

    public override int GetHashCode()
    {
        return X.GetHashCode() ^ Y.GetHashCode();
    }
}