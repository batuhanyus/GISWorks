using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SP.Lib;

namespace SP.Arayuz
{
    public partial class Form1 : Form
    {
        string shapeFilePath;
        string dbfFilePath;

        public Form1()
        {
            InitializeComponent();

            WriteLog("Program başlatıldı.", false);
        }

        private void pickInputFileButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "ESRI Shape File (*.shp)|*.shp";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    inputPathTextBox.Text = openFileDialog.FileName;
                }
                catch (SecurityException ex)
                {
                    MessageBox.Show($"Security error.\n\nError message: {ex.Message}\n\n" +
                    $"Details:\n\n{ex.StackTrace}");
                }
            }

            shapeFilePath = inputPathTextBox.Text;
            dbfFilePath = shapeFilePath.Replace(".shp", ".dbf");

            if (!File.Exists(dbfFilePath))
            {
                WriteLog("Aynı klasörde, aynı isimde bir .dbf dosyası olmalı!");
                return;
            }


            ///

            WriteLog("Dosyalar seçildi.");
            WriteLog($"Shape Dosyası: {shapeFilePath}");
            WriteLog($"DBF Dosyası: {dbfFilePath}");
        }


        private void WriteLog(string log, bool newLine = true)
        {
            if (newLine)
                logBox.AppendText($"\n{log}");
            else
                logBox.AppendText($"{log}");

            logBox.ScrollToCaret();
        }

        private void processFileButton_Click(object sender, EventArgs e)
        {
            WriteLog("-----------------------------");
            WriteLog("Dosyalar işleniyor. Bu işlem biraz sürebilir.");

            ShapeFileService shapeFileService = new ShapeFileService();

            var result = shapeFileService.ReadShapeFile(shapeFilePath);

            if (result == null)
            {
                WriteLog("Shape dosyası okunamadı!");
                return;
            }

            WriteLog("Poligon uç noktaları işleniyor.");

            WriteLog("-----------------------------");
            WriteLog($"{result.Polygons.Count} adet poligon bulundu.");


            var polygons = result.Polygons;

            foreach (var polygon in polygons)
            {
                WriteLog($"\nPoligon: {polygons.IndexOf(polygon) + 1} - UpLeft LAT: {polygon.UpperLeftCorner.Y} - UpLeft LON: {polygon.UpperLeftCorner.X} " +
                    $"- LowRight LAT: {polygon.LowerRightCorner.Y} - LowRight LON: {polygon.LowerRightCorner.X} - Center LAT: {polygon.CenterPoint.Y} - Center LON: {polygon.CenterPoint.X}");
            }


            WriteLog("-----------------------------");
            WriteLog("CSV dosyası olarak yazılıyor.");

            string csvPath = shapeFilePath.Replace(".shp", ".csv");

            shapeFileService.WriteAsCSV(csvPath, result);

            WriteLog($"CSV dosyası lokasyonu: {csvPath}");
        }
    }
}
