using System;
using Pan.Lib.Models;
using Pan.Lib.Services;


namespace Pan.Tests
{
    static class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            XmlService xmlService = new XmlService();
            string xmlPath = @"C:\Users\Raki\Desktop\PM_GKT_GGS_20210625181555_20210625181559_20210625215704_L2A.xml";

            var allStrips = xmlService.ReadXmlFile(xmlPath);

            SortingService sortingService = new SortingService();
            var stripsLeftToRight = sortingService.SortPanStripsLeftToRight(allStrips);
            foreach (var strip in stripsLeftToRight)
            {
                strip.SortedVertices = new List<GeoVertex>(sortingService.SortCoordinatesClockwise(strip.Vertices));
            }

            OutputService outputService = new OutputService();
            outputService.WriteStrips(stripsLeftToRight);

            //Debug
            CropZone cropZone = new CropZone();
            var debugVertices = DebugCropZoneCreator();
            cropZone.UpperLeftVertex = debugVertices[0];
            cropZone.UpperRightVertex = debugVertices[1];
            cropZone.LowerRightVertex = debugVertices[2];
            cropZone.LowerLeftVertex = debugVertices[3];

            StripKillService stripKillService = new StripKillService();
            var aliveStrips = stripKillService.KillStrips(stripsLeftToRight, cropZone);

            CropService cropService = new CropService();

            List<PanStripItem> croppedStrips = new List<PanStripItem>();
            foreach (var alive in aliveStrips)
            {
                var intersectionVertices = cropService.CropStrip(alive, cropZone, out PanStripItem croppedStrip);
                croppedStrips.Add(croppedStrip);
            }

            outputService.WriteStrips(croppedStrips, true);
        }

        static List<GeoVertex> DebugCropZoneCreator()
        {
            //-121.862837126384,36.5873942465499,"1"
            //-121.81377066463,36.6068910924945,"1"
            //-121.800943367579,36.5746094566894,"1"
            //-121.850009829333,36.5551126107447,"1"

            List<GeoVertex> cropZoneUnsortedVertices = new List<GeoVertex>();

            GeoVertex v1 = new GeoVertex();
            v1.LON = (decimal)-121.862837126384;
            v1.LAT = (decimal)36.5873942465499;

            //36.608158,-121.818551
            GeoVertex v2 = new GeoVertex();
            v2.LON = (decimal)-121.81377066463;
            v2.LAT = (decimal)36.6068910924945;

            //36.57083,-121.80268
            GeoVertex v3 = new GeoVertex();
            v3.LON = (decimal)-121.800943367579;
            v3.LAT = (decimal)36.5746094566894;

            GeoVertex v4 = new GeoVertex();
            v4.LON = (decimal)-121.8500098293339;
            v4.LAT = (decimal)36.5551126107447;

            cropZoneUnsortedVertices.Add(v1);
            cropZoneUnsortedVertices.Add(v2);
            cropZoneUnsortedVertices.Add(v3);
            cropZoneUnsortedVertices.Add(v4);

            return cropZoneUnsortedVertices;
        }
    }
}