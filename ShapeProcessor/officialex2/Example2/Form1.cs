using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using EGIS.ShapeFileLib;

/*
 * 
 * DISCLAIMER OF WARRANTY: THIS SOFTWARE IS PROVIDED ON AN "AS IS" BASIS, WITHOUT WARRANTY OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING,
 * WITHOUT LIMITATION, WARRANTIES THAT THE SOFTWARE IS FREE OF DEFECTS, MERCHANTABLE, FIT FOR A PARTICULAR PURPOSE OR NON-INFRINGING.
 * THE ENTIRE RISK AS TO THE QUALITY AND PERFORMANCE OF THE SOFTWARE IS WITH YOU. SHOULD ANY COVERED CODE PROVE DEFECTIVE IN ANY RESPECT,
 * YOU (NOT CORPORATE EASY GIS .NET) ASSUME THE COST OF ANY NECESSARY SERVICING, REPAIR OR CORRECTION.  
 * 
 * LIABILITY: IN NO EVENT SHALL CORPORATE EASY GIS .NET BE LIABLE FOR ANY DAMAGES WHATSOEVER (INCLUDING, WITHOUT LIMITATION, 
 * DAMAGES FOR LOSS OF BUSINESS PROFITS, BUSINESS INTERRUPTION, LOSS OF INFORMATION OR ANY OTHER PECUNIARY LOSS)
 * ARISING OUT OF THE USE OF INABILITY TO USE THIS SOFTWARE, EVEN IF CORPORATE EASY GIS .NET HAS BEEN ADVISED OF THE POSSIBILITY
 * OF SUCH DAMAGES.
 * 
 * Copyright: Easy GIS .NET 2010
 *
 */
namespace Example2
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (ofdShapefile.ShowDialog(this) == DialogResult.OK)
                {
                    DateTime dtStart = DateTime.Now;
                    ReadShapeFile(ofdShapefile.FileName);
                    rtbShapeFileAttributes.AppendText(string.Format("Time to output attributes: {0}s",((TimeSpan)DateTime.Now.Subtract(dtStart)).TotalSeconds));
                    //scroll to end of richtextbox
                    this.rtbShapeFileAttributes.ScrollToCaret();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }


        private void ReadShapeFile(string path)
        {
            ShapeFile.MapFilesInMemory = true;
            rtbShapeFileAttributes.Clear();
            this.rtbShapeFileAttributes.ScrollToCaret();
            bool outputPoints = this.chkDisplayPoints.Checked;
            bool outputZValues = this.chkOutputZValues.Checked;
            bool outputMValues = this.chkOutputMValues.Checked;
            // open the shapefile
            EGIS.ShapeFileLib.ShapeFile sf = new EGIS.ShapeFileLib.ShapeFile(path);
            try
            {
                // output the shapefile path, extent, record count and shape type
                rtbShapeFileAttributes.AppendText(string.Format("File Path {0} \n", sf.FilePath));
                rtbShapeFileAttributes.AppendText(string.Format("Extent: {0}\n", sf.Extent));
                rtbShapeFileAttributes.AppendText(string.Format("Num Shapes: {0}\n", sf.RecordCount));
                rtbShapeFileAttributes.AppendText(string.Format("Shape Type: {0}\n", sf.ShapeType));
                
                // create the shape files's rendersetings so we can read access the DBF reader
                sf.RenderSettings = new EGIS.ShapeFileLib.RenderSettings(path, "", this.Font);
                EGIS.ShapeFileLib.DbfReader dbfr = sf.RenderSettings.DbfReader;
                rtbShapeFileAttributes.AppendText("\n--- DBF Field Descriptions ---\n");
                
                // output the DBF Field Descriptions
                foreach (EGIS.ShapeFileLib.DbfFieldDesc dfd in dbfr.DbfRecordHeader.GetFieldDescriptions())                    
                {
                    rtbShapeFileAttributes.AppendText(dfd + "\n");
                }
                //check whether the shapefile contains Z values
                if(outputZValues)
                {
                    outputZValues = (sf.ShapeType == ShapeType.PolygonZ || sf.ShapeType == ShapeType.PointZ);
                }
                if (outputPoints || outputZValues || outputMValues)
                {
                    rtbShapeFileAttributes.AppendText("Outputting point data..\n");
                    rtbShapeFileAttributes.Refresh();
                    //scroll to end of richtextbox
                    this.rtbShapeFileAttributes.ScrollToCaret();

                    using (System.IO.StreamWriter writer = new System.IO.StreamWriter("output.txt"))
                    {
                        EGIS.ShapeFileLib.ShapeFileEnumerator sfEnum = sf.GetShapeFileEnumerator();
                        int recordIndex = 0;

                        while (sfEnum.MoveNext())
                        {
                            //if (recordIndex >= 100) break;
                            writer.WriteLine(string.Format("Record:{0}", recordIndex));

                            if (outputPoints)
                            {
                                //var rec = sf.GetShapeDataD(0);

                                System.Collections.ObjectModel.ReadOnlyCollection<PointD[]> pointRecords = sfEnum.Current;
                                
                                foreach (PointD[] pts in pointRecords)
                                {
                                    if (pts.Length < 50)
                                    {
                                        writer.Write(string.Format("[NumPoints:{0}]", pts.Length));

                                        for (int n = 0; n < pts.Length; ++n)
                                        {
                                            if (n > 0) writer.Write(',');
                                            writer.Write(pts[n].ToString());
                                        }
                                        writer.WriteLine();
                                    }
                                }
                            }
                            if (outputZValues)
                            {
                                
                                System.Collections.ObjectModel.ReadOnlyCollection<double[]> zValueRecords = sfEnum.GetCurrentZValues();
                                if (zValueRecords != null)
                                {
                                    writer.Write("ZValues:");
                                    foreach (double[] zValues in zValueRecords)
                                    {
                                        writer.Write("[NumHeights:{0}]", zValues.Length);
                                        for (int n = 0; n < zValues.Length; ++n)
                                        {
                                            if (n > 0) writer.Write(',');
                                            writer.Write(zValues[n]);
                                        }
                                    }
                                }
                                writer.WriteLine();
                            }
                            if (outputMValues)
                            {

                                System.Collections.ObjectModel.ReadOnlyCollection<double[]> mValueRecords = sf.GetShapeMDataD(recordIndex);
                                if (mValueRecords != null)
                                {
                                    foreach (double[] mValues in mValueRecords)
                                    {
                                        if (mValues.Length < 50)
                                        {
                                            writer.Write("[NumMeasures:{0}]", mValues.Length);
                                            for (int n = 0; n < mValues.Length; ++n)
                                            {
                                                if (n > 0) writer.Write(", ");
                                                writer.Write(mValues[n]);
                                            }
                                            writer.WriteLine();
                                        }
                                    }
                                }
                                writer.WriteLine();
                            }
                            ++recordIndex;
                        }
                        sfEnum.Dispose();
                    }
                    this.rtbShapeFileAttributes.AppendText("Point Data written to output.txt\n");                    
                }

               // GeneratePointGrid(sf);
                
            }
            finally
            {
                sf.Close();
                sf.Dispose();
            }

        }

        private void chkDisplayPoints_Click(object sender, EventArgs e)
        {
            
        }

        private void GeneratePointGrid(ShapeFile shapefile)
        {
            //loop on each record in the shapefile
            for(int n=0;n<shapefile.RecordCount;++n)
            {
                //change gridCountX and gridCountY for lower or higher resolution grid
                List<PointD> gridPoints = GetRecordGridPoints(shapefile, n, 50, 50);
                //this.rtbShapeFileAttributes.AppendText(string.Format("Record {0} contains {1} grid points\n", n, gridPoints.Count));
                //this.Refresh();

                //draw points to an image to view grid (test)
                using (Bitmap bm = new Bitmap(360, 180))
                {
                    using (Graphics g = Graphics.FromImage(bm))
                    {
                        for (int i = gridPoints.Count - 1; i >= 0; --i)
                        {
                            float x = (float)gridPoints[i].X;
                            if (x > 180) x -= 180;
                            x += 180;
                            float y = 180 - ((float)gridPoints[i].Y + 90);

                            g.FillEllipse(Brushes.Red, x, y, 2, 2);
                        }
                    }
                    bm.Save(string.Format("record_{0}.bmp", n), System.Drawing.Imaging.ImageFormat.Bmp);
                }

            }
        }

        
        
        private List<PointD> GetRecordGridPoints(ShapeFile shapefile, int recordIndex, int gridCountX, int gridCountY)
        {
            if (shapefile.ShapeType != ShapeType.Polygon) throw new Exception("only polygon shapefiles supported");
            List<PointD> gridPoints = new List<PointD>();
            System.Collections.ObjectModel.ReadOnlyCollection<PointD[]> shapeData = shapefile.GetShapeDataD(recordIndex);

            foreach (PointD[] polygonPoints in shapeData)
            {
                double minX = double.PositiveInfinity, minY = double.PositiveInfinity, maxX = double.NegativeInfinity, maxY = double.NegativeInfinity;
                for (int n = polygonPoints.Length - 1; n >= 0; --n)
                {
                    if (minX > polygonPoints[n].X) minX = polygonPoints[n].X;
                    if (minY > polygonPoints[n].Y) minY = polygonPoints[n].Y;
                    if (maxX < polygonPoints[n].X) maxX = polygonPoints[n].X;
                    if (maxY < polygonPoints[n].Y) maxY = polygonPoints[n].Y;
                }

                double cellWidth = (maxX - minX) / gridCountX;
                double cellHeight = (maxY-minY) / gridCountY;            
                double x = minX;
                while (x <= maxX)
                {
                    double y = minY;
                    while (y <= maxY)
                    {                    
                        if (GeometryAlgorithms.PointInPolygon(polygonPoints, x, y))
                        {
                            gridPoints.Add(new PointD(x, y));
                        }                    
                        y += cellHeight;
                    }
                    x += cellWidth;
                }
            }
            return gridPoints;
        }

        //private List<PointD> GetRecordGridPoints(ShapeFile shapefile, int recordIndex, int gridCountX, int gridCountY)
        //{
        //    if(shapefile.ShapeType != ShapeType.Polygon) throw new Exception("only polygon shapefiles supported");
        //    List<PointD> gridPoints = new List<PointD>();
        //    //get the extent of the record in the shapefile
        //    RectangleD recordExtent = shapefile.GetShapeBoundsD(recordIndex);
        //    double cellWidth = recordExtent.Width / gridCountX;
        //    double cellHeight = recordExtent.Height / gridCountY;
        //    System.Collections.ObjectModel.ReadOnlyCollection<PointD[]> shapeData = shapefile.GetShapeDataD(recordIndex);

        //    double x = recordExtent.Left;

        //    while (x <= recordExtent.Right)
        //    {
        //        double y = recordExtent.Top;
        //        while (y <= recordExtent.Bottom)
        //        {    
        //            foreach(PointD[] polygonPoints in shapeData)
        //            {
        //                if (GeometryAlgorithms.PointInPolygon(polygonPoints, x, y))
        //                {
        //                    gridPoints.Add(new PointD(x,y));
        //                    break;
        //                }

        //            }
        //            y += cellHeight;
        //        }
        //        x += cellWidth;
        //    }
        //    return gridPoints;
        //}


    }
}