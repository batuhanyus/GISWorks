namespace Pan.Lib.Models;

public class PanStripItem
{
    public string StripName { get; set; }
    public List<GeoVertex> Vertices { get; set; }
    public List<GeoVertex> SortedVertices { get; set; }
    public GeoVertex CenterVertex { get; set; }

    public PanStripItem()
    {
        Vertices = new List<GeoVertex>();
        SortedVertices = new List<GeoVertex>();
    }
}