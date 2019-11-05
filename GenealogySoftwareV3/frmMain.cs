using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GenealogySoftwareV3.Types;

namespace GenealogySoftwareV3
{
    public partial class frmMain : Form
    {
        FileReader fileInterpreter;
        

        public frmMain()
        {
            InitializeComponent();
            fileInterpreter = new FileReader();

        }


        private void frmMain_Load(object sender, EventArgs e)
        {
            btnOpenGEDCOMFile.Text = "Open\nGEDCOM\nfile";
            
        }

        private void btnOpenGEDCOMFile_Click(object sender, EventArgs e)
        {
            ofdGEDCOM.ShowDialog();
            
        }

        private void ofdGEDCOM_FileOk(object sender, CancelEventArgs e)
        {
            DataTable fileData = fileInterpreter.LoadDataIntoDataTable(ofdGEDCOM.FileName);
            fileInterpreter.InterpretFileContents(fileData);
            TreeNode rootNode = new TreeNode("Family Tree");
            List<int> rootMarriages = new List<int>(new int[] {8, 135, 144});
            Marriage rootMarriageMatch;
            foreach(int rootMarriage in rootMarriages)
                if (fileInterpreter.Marriages.TryGetValue(rootMarriage, out rootMarriageMatch))
                    fileInterpreter.GenerateTree(rootNode, rootMarriageMatch);

            treeView.Nodes.Add(rootNode);

            fileInterpreter.GetFamiliesRoots();
        }

        private void btnCollectFamilies_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ofdGEDCOM.FileName)) return;

            var dialogResult = sfdFamiliesList.ShowDialog();
            if (dialogResult != DialogResult.OK) return;

            File.WriteAllLines(sfdFamiliesList.FileName, fileInterpreter.GetFamiliesCsv(), Encoding.UTF8);
            Process.Start(sfdFamiliesList.FileName);
        }
    }
}
