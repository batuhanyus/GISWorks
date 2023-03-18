using System.Xml;
using Pan.Lib.Models;

namespace Pan.Lib.Services;

public class XmlService
{
    public List<PanStripItem> ReadXmlFile(string path)
    {
        XmlDocument doc = new XmlDocument();
        doc.Load(path);
        XmlNode rootNode = doc.DocumentElement;

        XmlNodeList panNodes = rootNode.SelectNodes("descendant::BandFraming[BAND_ID='PAN']");
        XmlNode panRootNode = rootNode.SelectNodes("descendant::BandFraming[BAND_ID='PAN']")[0];

        XmlNodeList stripNodes = panRootNode.SelectNodes("descendant::Array_Framing");
        XmlNode stripRootNode = stripNodes[0];

        XmlNodeList stripArrayNodes = stripRootNode.SelectNodes("descendant::Array");

        List<PanStripItem> panStripItems = new List<PanStripItem>();
        foreach (XmlNode stripNode in stripArrayNodes)
        {
            PanStripItem panStripItem = new PanStripItem();
            panStripItems.Add(panStripItem);

            panStripItem.StripName = stripNode.SelectSingleNode("ARRAY_ID").InnerText;

            XmlNode stripCenterNode = stripNode.SelectSingleNode("descendant::Array_Center");
            panStripItem.CenterVertex = new GeoVertex
            {
                LON = Convert.ToDecimal(stripCenterNode.SelectSingleNode("LON").InnerText),
                LAT = Convert.ToDecimal(stripCenterNode.SelectSingleNode("LAT").InnerText),
                COL = Convert.ToDecimal(stripCenterNode.SelectSingleNode("COL").InnerText),
                ROW = Convert.ToDecimal(stripCenterNode.SelectSingleNode("ROW").InnerText)
            };

            XmlNodeList stripVertexNodes = stripNode.SelectNodes("descendant::Array_Footprint")[0]
                .SelectNodes("descendant::Vertex");

            for (int i = 0; i < stripVertexNodes.Count; i++)
            {
                XmlNode vertex = stripVertexNodes[i];
                
                GeoVertex geoVertex = new GeoVertex
                {
                    ID = i,
                    LON = Convert.ToDecimal(vertex.SelectSingleNode("LON").InnerText),
                    LAT = Convert.ToDecimal(vertex.SelectSingleNode("LAT").InnerText),
                    COL = Convert.ToDecimal(vertex.SelectSingleNode("COL").InnerText),
                    ROW = Convert.ToDecimal(vertex.SelectSingleNode("ROW").InnerText)
                };

                panStripItem.Vertices.Add(geoVertex);
            }
        }

        return panStripItems;
    }
}