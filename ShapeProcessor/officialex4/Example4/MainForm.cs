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

namespace Example4
{
    /// <summary>
    /// Main class of Example 4
    /// <remarks>
    /// Example 4 demonstrates opening a shapefile and creating a Google KML file from the contents of a ShapeFile.
    /// <para>To run the demo, open a shapefile and click Generate KML
    /// </para>
    /// <para>Currently the example only supports poylgon and polyline shapefiles; however it could be eaily extended to 
    /// support point shapefiles</para>    
    /// </remarks>
    /// </summary>
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        #region event handlers

        private void btnBrowseInputFile_Click(object sender, EventArgs e)
        {
            if (this.ofdInputFiles.ShowDialog(this) == DialogResult.OK)
            {
                this.txtFilterInputFile.Text = ofdInputFiles.FileName;
            }            
        }


        private void btnBrowseKml_Click(object sender, EventArgs e)
        {
            if (this.sfdKmlFile.ShowDialog(this) == DialogResult.OK)
            {
                this.txtKmlFile.Text = ofdInputFiles.FileName;
            }
        }
        
        
        #endregion


        /// <summary>
        /// This is the main method of the example.
        /// The method opens the input shapefile and generates a Google KML file from the shapefile records
        /// </summary>
        private void GenerateKmlFile()
        {
            ShapeFile sf = new ShapeFile(this.txtFilterInputFile.Text);
            DbfReader dbfReader = new DbfReader(System.IO.Path.ChangeExtension(this.txtFilterInputFile.Text, ".dbf"));
            try
            {
                KmlGenUtil.CreateKmlFile("test.kml", sf, dbfReader, new ProgressDelegate(UpdateProgress));
            }
            finally
            {
                sf.Close();
                dbfReader.Close();
            }

        }

        void UpdateProgress(int currentValue, int total)
        {
            if (this.toolStripProgressBar1.Maximum != total)
            {
                this.toolStripProgressBar1.Maximum = total;
            }
            this.toolStripProgressBar1.Value = currentValue;
        }

        private void btnGenerateKML_Click(object sender, EventArgs e)
        {
            try
            {
                GenerateKmlFile();
                MessageBox.Show(this, "Kml File generation complete", "Success");
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Error: " + ex.Message, "Error");
            }
        }

        

        

    }
}