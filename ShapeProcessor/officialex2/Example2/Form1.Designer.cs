namespace Example2
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
            this.ofdShapefile = new System.Windows.Forms.OpenFileDialog();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rtbShapeFileAttributes = new System.Windows.Forms.RichTextBox();
            this.chkDisplayPoints = new System.Windows.Forms.CheckBox();
            this.chkOutputZValues = new System.Windows.Forms.CheckBox();
            this.chkOutputMValues = new System.Windows.Forms.CheckBox();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ofdShapefile
            // 
            this.ofdShapefile.Filter = "ShapeFiles (*.shp) | *.shp";
            this.ofdShapefile.InitialDirectory = ".";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(599, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // rtbShapeFileAttributes
            // 
            this.rtbShapeFileAttributes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbShapeFileAttributes.DetectUrls = false;
            this.rtbShapeFileAttributes.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbShapeFileAttributes.Location = new System.Drawing.Point(12, 57);
            this.rtbShapeFileAttributes.Name = "rtbShapeFileAttributes";
            this.rtbShapeFileAttributes.Size = new System.Drawing.Size(575, 210);
            this.rtbShapeFileAttributes.TabIndex = 1;
            this.rtbShapeFileAttributes.Text = "";
            this.rtbShapeFileAttributes.WordWrap = false;
            // 
            // chkDisplayPoints
            // 
            this.chkDisplayPoints.AutoSize = true;
            this.chkDisplayPoints.Location = new System.Drawing.Point(13, 34);
            this.chkDisplayPoints.Name = "chkDisplayPoints";
            this.chkDisplayPoints.Size = new System.Drawing.Size(96, 17);
            this.chkDisplayPoints.TabIndex = 2;
            this.chkDisplayPoints.Text = "Outpout Points";
            this.chkDisplayPoints.UseVisualStyleBackColor = true;
            this.chkDisplayPoints.Click += new System.EventHandler(this.chkDisplayPoints_Click);
            // 
            // chkOutputZValues
            // 
            this.chkOutputZValues.AutoSize = true;
            this.chkOutputZValues.Location = new System.Drawing.Point(127, 34);
            this.chkOutputZValues.Name = "chkOutputZValues";
            this.chkOutputZValues.Size = new System.Drawing.Size(103, 17);
            this.chkOutputZValues.TabIndex = 3;
            this.chkOutputZValues.Text = "Output Z Values";
            this.chkOutputZValues.UseVisualStyleBackColor = true;
            // 
            // chkOutputMValues
            // 
            this.chkOutputMValues.AutoSize = true;
            this.chkOutputMValues.Location = new System.Drawing.Point(236, 34);
            this.chkOutputMValues.Name = "chkOutputMValues";
            this.chkOutputMValues.Size = new System.Drawing.Size(105, 17);
            this.chkOutputMValues.TabIndex = 4;
            this.chkOutputMValues.Text = "Output M Values";
            this.chkOutputMValues.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(599, 279);
            this.Controls.Add(this.chkOutputMValues);
            this.Controls.Add(this.chkOutputZValues);
            this.Controls.Add(this.chkDisplayPoints);
            this.Controls.Add(this.rtbShapeFileAttributes);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "Example 2";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog ofdShapefile;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.RichTextBox rtbShapeFileAttributes;
        private System.Windows.Forms.CheckBox chkDisplayPoints;
        private System.Windows.Forms.CheckBox chkOutputZValues;
        private System.Windows.Forms.CheckBox chkOutputMValues;
    }
}

