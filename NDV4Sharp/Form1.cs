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
            this.bInsertMarker.Enabled = false;
            this.buttonExcel.Enabled = false;
            this.bStartAnalysis.Enabled = false;

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
            if (CheckC || CheckSharp || CheckFortran)
            {
                CreateNewProject createNewProject = new CreateNewProject(bInsertMarker);
                createNewProject.Show();
            }
            else
                MessageBox.Show("Please, choice language!");
        }

        private void openStripMenuItem_Click(object sender, EventArgs e)
        {
            //LInformation = "Open project!";
            OpenFile openFileDB = new OpenFile(bStartAnalysis);
            openFileDB.openFileDB();
            buttonExcel.Enabled = true;
        }

        private void cBSharp_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            if (checkBox.Checked == true)
            {
                CheckSharp = true;
                CheckC = false;
                CheckFortran = false;
                cBCpp.Enabled = false;
                cBFatran.Enabled = false;
            }
            else
            {
                cBCpp.Enabled = true;
                cBFatran.Enabled = true;
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
                cBSharp.Enabled = false;
                cBFatran.Enabled = false;
            }
            else
            {
                cBSharp.Enabled = true;
                cBFatran.Enabled = true;
            }
                
        }

        private void cBFatran_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            if (checkBox.Checked == true)
            {
                CheckC = false;
                CheckSharp = false;
                CheckFortran = true;
                cBSharp.Enabled = false;
                cBCpp.Enabled = false;
            }
            else
            {
                cBSharp.Enabled = true;
                cBCpp.Enabled = true;
            }
        }

        private void bStartAnalysis_Click(object sender, EventArgs e)
        {
            FindTmpMarker findTmpMarker = new FindTmpMarker();
            findTmpMarker.findTmpMarkerWithBin();

        }
    }
}
