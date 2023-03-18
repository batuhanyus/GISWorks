namespace Pan.Lib.Models;

public class CropZone
{
    public GeoVertex UpperLeftVertex { get; set; }
    public GeoVertex UpperRightVertex { get; set; }
    public GeoVertex LowerRightVertex { get; set; }
    public GeoVertex LowerLeftVertex { get; set; }
    public GeoVertex CenterVertex { get; set; }

    public CropZone()
    {
        CenterVertex = new GeoVertex();
    }

    public void CalculateCenterVertex()
    {
        //Not sure if this correct. I think it is.
        CenterVertex.LAT = (UpperLeftVertex.LAT + LowerRightVertex.LAT) / 2;
        CenterVertex.LON = (UpperLeftVertex.LON + LowerRightVertex.LON) / 2;
    }
}