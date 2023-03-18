namespace SP.Arayuz
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.inputPathTextBox = new System.Windows.Forms.TextBox();
            this.processFileButton = new System.Windows.Forms.Button();
            this.logBox = new System.Windows.Forms.RichTextBox();
            this.pickInputFileButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // inputPathTextBox
            // 
            this.inputPathTextBox.Location = new System.Drawing.Point(6, 18);
            this.inputPathTextBox.Name = "inputPathTextBox";
            this.inputPathTextBox.Size = new System.Drawing.Size(572, 20);
            this.inputPathTextBox.TabIndex = 0;
            // 
            // processFileButton
            // 
            this.processFileButton.Location = new System.Drawing.Point(181, 53);
            this.processFileButton.Name = "processFileButton";
            this.processFileButton.Size = new System.Drawing.Size(340, 38);
            this.processFileButton.TabIndex = 1;
            this.processFileButton.Text = "İşle";
            this.processFileButton.UseVisualStyleBackColor = true;
            this.processFileButton.Click += new System.EventHandler(this.processFileButton_Click);
            // 
            // logBox
            // 
            this.logBox.Location = new System.Drawing.Point(6, 107);
            this.logBox.Name = "logBox";
            this.logBox.Size = new System.Drawing.Size(669, 348);
            this.logBox.TabIndex = 2;
            this.logBox.Text = "";
            // 
            // pickInputFileButton
            // 
            this.pickInputFileButton.Location = new System.Drawing.Point(584, 18);
            this.pickInputFileButton.Name = "pickInputFileButton";
            this.pickInputFileButton.Size = new System.Drawing.Size(91, 23);
            this.pickInputFileButton.TabIndex = 3;
            this.pickInputFileButton.Text = "Seç (.shp)";
            this.pickInputFileButton.UseVisualStyleBackColor = true;
            this.pickInputFileButton.Click += new System.EventHandler(this.pickInputFileButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(687, 467);
            this.Controls.Add(this.pickInputFileButton);
            this.Controls.Add(this.logBox);
            this.Controls.Add(this.processFileButton);
            this.Controls.Add(this.inputPathTextBox);
            this.Name = "Form1";
            this.Text = "Shape Processor Arayüz";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox inputPathTextBox;
        private System.Windows.Forms.Button processFileButton;
        private System.Windows.Forms.RichTextBox logBox;
        private System.Windows.Forms.Button pickInputFileButton;
    }
}
