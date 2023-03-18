using Pan.Lib.Models;

namespace Pan.Lib.Services;

public class OutputService
{
    public void WriteStrips(List<PanStripItem> panStripItems, bool writeCroppedStrips = false)
    {
        //Cleanup
        if (writeCroppedStrips == false && Directory.Exists("Output"))
        {
            Directory.Delete("Output", true);
        }

        foreach (var panStripItem in panStripItems)
        {
            Directory.CreateDirectory("Output");

            StreamWriter sw;
            if (!writeCroppedStrips)
            {
                sw = File.CreateText(AppDomain.CurrentDomain.BaseDirectory +
                                     @$"/Output/PAN{panStripItem.StripName}.txt");
            }
            else
            {
                sw = File.CreateText(AppDomain.CurrentDomain.BaseDirectory +
                                     @$"/Output/CroppedPAN{panStripItem.StripName}.txt");
            }

            //Header
            sw.WriteLine("LON,LAT,INDEX");

            //TODO These were unsorted vertices. Maybe a bug happens here?
            foreach (var vertex in panStripItem.SortedVertices)
            {
                int index = panStripItem.SortedVertices.IndexOf(vertex);
                sw.WriteLine($"{vertex.LON},{vertex.LAT},{index}");
            }

            //sw.WriteLine(panStripItems[0].Vertices[0].LON + "," + panStripItems[0].Vertices[0].LAT);

            sw.Close();
        }
    }
}