using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using System.IO;

namespace NDV4Sharp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            
            InitializeComponent();
            //buttonExcel.Enabled = false;
            //buttonOpenFolderBin.Enabled = false;

            listBoxReport.Items.Add("Вывести все файлы найденые в бинарниках.");
            listBoxReport.Items.Add("Вывести избыточные файлы не найденые в бинарниках.");
            listBoxReport.Items.Add("Вывести файлы входящие в бинарники.");
            listBoxReport.SelectedIndex = 1;

            this.Text = "Есть ли жизнь на марсе?";
            

            this.createStripMenuItem.Click += new EventHandler(this.createStripMenuItem_Click);
            
        }

        //private void buttonOpenFolderSrc_Click(object sender, EventArgs e)
        //{
        //    labelInformation.Text = "Waiting ...";
        //    OpenFolder openFolder = new OpenFolder();
        //    openFolder.openFolderWithSrc();
        //    labelInformation.Text = "Find marker in files - OK";
        //    buttonOpenFolderBin.Enabled = true;
        //}

        //private void buttonOpenFolderBin_Click(object sender, EventArgs e)
        //{
        //    buttonOpenFolderSrc.Enabled = false;
        //    labelInformation.Text = "Waiting ...";
        //    OpenFolder openFolder = new OpenFolder();
        //    openFolder.openFolderWithBin();
        //    labelInformation.Text = "Find marker in bin - OK";
        //    buttonExcel.Enabled = true;
        //    buttonOpenFolderSrc.Enabled = true;

        //}

        private void buttonExcel_Click(object sender, EventArgs e)
        {
            buttonOpenFolderBin.Enabled = false;
            labelInformation.Text = "Waiting ...";
            BuildReport buildReport = new BuildReport();
            buildReport.buildReportExcel(listBoxReport.SelectedIndex);        
            labelInformation.Text = "Building report - OK";
            buttonOpenFolderBin.Enabled = true;
            
        }

        private void buttonInsertMarker_Click(object sender, EventArgs e)
        {
            labelInformation.Text = "Waiting ...";
            OpenFolder openFolder = new OpenFolder();
            openFolder.insertMarkerInFile();
            labelInformation.Text = "Insert marker - OK";
        }

        private void createStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateNewProject createNewProject = new CreateNewProject();
            createNewProject.Show();
            labelInformation.Text = "Create project.";
        }

        private void openStripMenuItem_Click(object sender, EventArgs e)
        {
            
            OpenFile openFileDB = new OpenFile();
            openFileDB.openFileDB();
        }

        private void buttonOpenFolderBin_Click(object sender, EventArgs e)
        {
            FindTmpMarker findTmpMarker = new FindTmpMarker();
            findTmpMarker.findTmpMarkerWithBin();
        }
    }
}
