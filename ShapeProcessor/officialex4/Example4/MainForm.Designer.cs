namespace Example4
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnBrowseInputFile = new System.Windows.Forms.Button();
            this.txtFilterInputFile = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.ofdInputFiles = new System.Windows.Forms.OpenFileDialog();
            this.sfdKmlFile = new System.Windows.Forms.SaveFileDialog();
            this.btnGenerateKML = new System.Windows.Forms.Button();
            this.btnBrowseKml = new System.Windows.Forms.Button();
            this.txtKmlFile = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnBrowseInputFile
            // 
            this.btnBrowseInputFile.Location = new System.Drawing.Point(382, 4);
            this.btnBrowseInputFile.Name = "btnBrowseInputFile";
            this.btnBrowseInputFile.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseInputFile.TabIndex = 16;
            this.btnBrowseInputFile.Text = "Browse";
            this.btnBrowseInputFile.UseVisualStyleBackColor = true;
            this.btnBrowseInputFile.Click += new System.EventHandler(this.btnBrowseInputFile_Click);
            // 
            // txtFilterInputFile
            // 
            this.txtFilterInputFile.Location = new System.Drawing.Point(91, 4);
            this.txtFilterInputFile.Name = "txtFilterInputFile";
            this.txtFilterInputFile.Size = new System.Drawing.Size(285, 20);
            this.txtFilterInputFile.TabIndex = 15;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(14, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(78, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "Input Shapefile";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripProgressBar1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 109);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(470, 22);
            this.statusStrip1.TabIndex = 17;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(150, 16);
            // 
            // ofdInputFiles
            // 
            this.ofdInputFiles.Filter = "shapefiles (*.shp)|*.shp";
            this.ofdInputFiles.InitialDirectory = ".";
            // 
            // sfdKmlFile
            // 
            this.sfdKmlFile.Filter = "Google KML (*.kml) | *.kml";
            // 
            // btnGenerateKML
            // 
            this.btnGenerateKML.Location = new System.Drawing.Point(145, 78);
            this.btnGenerateKML.Name = "btnGenerateKML";
            this.btnGenerateKML.Size = new System.Drawing.Size(181, 23);
            this.btnGenerateKML.TabIndex = 18;
            this.btnGenerateKML.Text = "Generate KML";
            this.btnGenerateKML.UseVisualStyleBackColor = true;
            this.btnGenerateKML.Click += new System.EventHandler(this.btnGenerateKML_Click);
            // 
            // btnBrowseKml
            // 
            this.btnBrowseKml.Location = new System.Drawing.Point(383, 42);
            this.btnBrowseKml.Name = "btnBrowseKml";
            this.btnBrowseKml.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseKml.TabIndex = 21;
            this.btnBrowseKml.Text = "Browse";
            this.btnBrowseKml.UseVisualStyleBackColor = true;
            this.btnBrowseKml.Click += new System.EventHandler(this.btnBrowseKml_Click);
            // 
            // txtKmlFile
            // 
            this.txtKmlFile.Location = new System.Drawing.Point(91, 44);
            this.txtKmlFile.Name = "txtKmlFile";
            this.txtKmlFile.Size = new System.Drawing.Size(285, 20);
            this.txtKmlFile.TabIndex = 20;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(38, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 19;
            this.label1.Text = "KML File";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(470, 131);
            this.Controls.Add(this.btnBrowseKml);
            this.Controls.Add(this.txtKmlFile);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnGenerateKML);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.btnBrowseInputFile);
            this.Controls.Add(this.txtFilterInputFile);
            this.Controls.Add(this.label5);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "MainForm";
            this.Text = "Example 4";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnBrowseInputFile;
        private System.Windows.Forms.TextBox txtFilterInputFile;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.OpenFileDialog ofdInputFiles;
        private System.Windows.Forms.SaveFileDialog sfdKmlFile;
        private System.Windows.Forms.Button btnGenerateKML;
        private System.Windows.Forms.Button btnBrowseKml;
        private System.Windows.Forms.TextBox txtKmlFile;
        private System.Windows.Forms.Label label1;
    }
}

