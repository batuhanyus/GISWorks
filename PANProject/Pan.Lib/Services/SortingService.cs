using McNeight;
using Pan.Lib.Models;

namespace Pan.Lib.Services;

public class SortingService
{
    public List<PanStripItem> SortPanStripsLeftToRight(List<PanStripItem> panStripItems)
    {
        List<PanStripItem> sortedPanStripItems = new List<PanStripItem>(panStripItems);

        sortedPanStripItems.Sort((x, y) => x.CenterVertex.LON.CompareTo(y.CenterVertex.LON));

        return sortedPanStripItems;
    }

    //This sort them in order-> top left, top right, bottom right, bottom left
    public List<GeoVertex> SortCoordinatesClockwise(List<GeoVertex> coordinates)
    {
        // List<GeoVertex> sortedCoordinates = new List<GeoVertex>();
        //
        // GeoVertex topLeft = coordinates.OrderBy(p => p.LON).ThenBy(p => p.LAT).First();
        // GeoVertex topRight = coordinates.OrderBy(p => p.LON).ThenByDescending(p => p.LAT).First();
        // GeoVertex bottomRight = coordinates.OrderByDescending(p => p.LON).ThenByDescending(p => p.LAT).First();
        // GeoVertex bottomLeft = coordinates.OrderByDescending(p => p.LON).ThenBy(p => p.LAT).First();
        //
        // sortedCoordinates.Add(topLeft);
        // sortedCoordinates.Add(topRight);
        // sortedCoordinates.Add(bottomRight);
        // sortedCoordinates.Add(bottomLeft);
        //
        // return sortedCoordinates;


        // Find the center of the coordinates
        decimal centerX = 0;
        decimal centerY = 0;

        var maxie1LON =MathM.Max(coordinates[0].LON, coordinates[1].LON);
        var maxie2LON = MathM.Max(coordinates[2].LON, coordinates[3].LON);
        var maxieFinalLON = MathM.Max(maxie1LON, maxie2LON);
        
        var maxie1LAT = MathM.Max(coordinates[0].LAT, coordinates[1].LAT);
        var maxie2LAT = MathM.Max(coordinates[2].LAT, coordinates[3].LAT);
        var maxieFinalLAT = MathM.Max(maxie1LAT, maxie2LAT);
        
        var minie1LON = MathM.Min(coordinates[0].LON, coordinates[1].LON);
        var minie2LON = MathM.Min(coordinates[2].LON, coordinates[3].LON);
        var minieFinalLON = MathM.Min(minie1LON, minie2LON);
        
        var minie1LAT = MathM.Min(coordinates[0].LAT, coordinates[1].LAT);
        var minie2LAT = MathM.Min(coordinates[2].LAT, coordinates[3].LAT);
        var minieFinalLAT = MathM.Min(minie1LAT, minie2LAT);
        
        centerX = (maxieFinalLON + minieFinalLON) / 2;
        centerY = (maxieFinalLAT + minieFinalLAT) / 2;

        // foreach (var coord in coordinates)
        // {
        //     centerX += coord.LON;
        //     centerY += coord.LAT;
        // }
        // centerX /= coordinates.Count;
        // centerY /= coordinates.Count;
        
        // Sort the coordinates based on their angle with respect to the center
        coordinates.Sort((coord1, coord2) =>
        {
            decimal angle1 = MathM.Atan2(coord1.LAT - centerY, coord1.LON - centerX);
            decimal angle2 = MathM.Atan2(coord2.LAT - centerY, coord2.LON - centerX);
            return angle1.CompareTo(angle2);
        });
        
        //Sort top left, top right, bottom right, bottom left
        
        
        
        return coordinates;
    }
}