namespace GenealogySoftwareV3
{
    partial class frmMain
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
            this.ofdGEDCOM = new System.Windows.Forms.OpenFileDialog();
            this.btnOpenGEDCOMFile = new System.Windows.Forms.Button();
            this.treeView = new System.Windows.Forms.TreeView();
            this.btnCollectFamilies = new System.Windows.Forms.Button();
            this.sfdFamiliesList = new System.Windows.Forms.SaveFileDialog();
            this.SuspendLayout();
            // 
            // ofdGEDCOM
            // 
            this.ofdGEDCOM.DefaultExt = "GED";
            this.ofdGEDCOM.Filter = "GED Files | *.ged";
            this.ofdGEDCOM.Title = "Choose your GEDCOM file:";
            this.ofdGEDCOM.FileOk += new System.ComponentModel.CancelEventHandler(this.ofdGEDCOM_FileOk);
            // 
            // btnOpenGEDCOMFile
            // 
            this.btnOpenGEDCOMFile.Location = new System.Drawing.Point(12, 12);
            this.btnOpenGEDCOMFile.Name = "btnOpenGEDCOMFile";
            this.btnOpenGEDCOMFile.Size = new System.Drawing.Size(75, 53);
            this.btnOpenGEDCOMFile.TabIndex = 0;
            this.btnOpenGEDCOMFile.UseVisualStyleBackColor = true;
            this.btnOpenGEDCOMFile.Click += new System.EventHandler(this.btnOpenGEDCOMFile_Click);
            // 
            // treeView
            // 
            this.treeView.Location = new System.Drawing.Point(12, 84);
            this.treeView.Name = "treeView";
            this.treeView.Size = new System.Drawing.Size(604, 328);
            this.treeView.TabIndex = 1;
            // 
            // btnCollectFamilies
            // 
            this.btnCollectFamilies.Location = new System.Drawing.Point(93, 12);
            this.btnCollectFamilies.Name = "btnCollectFamilies";
            this.btnCollectFamilies.Size = new System.Drawing.Size(76, 53);
            this.btnCollectFamilies.TabIndex = 2;
            this.btnCollectFamilies.Text = "Collect Families";
            this.btnCollectFamilies.UseVisualStyleBackColor = true;
            this.btnCollectFamilies.Click += new System.EventHandler(this.btnCollectFamilies_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(642, 502);
            this.Controls.Add(this.btnCollectFamilies);
            this.Controls.Add(this.treeView);
            this.Controls.Add(this.btnOpenGEDCOMFile);
            this.Name = "frmMain";
            this.Text = "My Genealogy Software V3";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog ofdGEDCOM;
        private System.Windows.Forms.Button btnOpenGEDCOMFile;
        private System.Windows.Forms.TreeView treeView;
        private System.Windows.Forms.Button btnCollectFamilies;
        private System.Windows.Forms.SaveFileDialog sfdFamiliesList;
    }
}

