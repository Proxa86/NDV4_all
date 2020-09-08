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
using System.Runtime.CompilerServices;

namespace NDV4Sharp
{
    public partial class Form1 : Form
    {
        public static bool CheckSharp { get; set; }
        public static bool CheckC { get; set; }
        public static bool CheckFortran { get; set; }
        public Form1()
        {
            
            InitializeComponent();

            listBoxReport.Items.Add("Вывести все файлы найденые в бинарниках.");
            listBoxReport.Items.Add("Вывести избыточные файлы не найденые в бинарниках.");
            listBoxReport.Items.Add("Вывести файлы входящие в бинарники.");
            listBoxReport.SelectedIndex = 1;

            this.Text = "Есть ли жизнь на марсе?";

            this.createStripMenuItem.Click += new EventHandler(this.createStripMenuItem_Click);
            
        }

        private void buttonExcel_Click(object sender, EventArgs e)
        {
            bStartAnalysis.Enabled = false;
            //LInformation = "Waiting ...";
            BuildReport buildReport = new BuildReport();
            buildReport.buildReportExcel(listBoxReport.SelectedIndex);        
            //LInformation = "Building report - OK";
            bStartAnalysis.Enabled = true;
            
        }

        private void buttonInsertMarker_Click(object sender, EventArgs e)
        {
            //LInformation = "Waiting ...";
            OpenFolder openFolder = new OpenFolder();
            openFolder.insertMarkerInFile();
            //LInformation = "Insert marker - OK";
        }

        private void createStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CheckC || CheckSharp)
            {
                CreateNewProject createNewProject = new CreateNewProject();
                createNewProject.Show();
            }
            else
                MessageBox.Show("Please, choice language!");
            //LInformation = "Create project!";

        }

        private void openStripMenuItem_Click(object sender, EventArgs e)
        {
            //LInformation = "Open project!";
            OpenFile openFileDB = new OpenFile();
            openFileDB.openFileDB();
        }

        private void cBSharp_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            if (checkBox.Checked == true)
            {
                CheckSharp = true;
                CheckC = false;
                CheckFortran = false;
            }
                
        }

        private void cBCpp_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            if (checkBox.Checked == true)
            {
                CheckC = true;
                CheckSharp = false;
                CheckFortran = false;
            }
                
        }

        private void cBFatran_CheckedChanged(object sender, EventArgs e)
        {
            CheckFortran = true;
            CheckSharp = false;
            CheckC = false;
        }

        private void bStartAnalysis_Click(object sender, EventArgs e)
        {
            FindTmpMarker findTmpMarker = new FindTmpMarker();
            findTmpMarker.findTmpMarkerWithBin();

        }
    }
}
