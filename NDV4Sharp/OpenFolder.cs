using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NDV4Sharp
{
    class OpenFolder
    {
        public string PathNewLocation { get; set; }
        public string PathSourcesFolder { get; set; }

        //public void openFolderWithSrc()
        //{
        //    FolderBrowserDialog fbd = new FolderBrowserDialog();
        //    fbd.SelectedPath = @"C:\";
        //    fbd.ShowNewFolderButton = false;

        //    if(fbd.ShowDialog() == DialogResult.OK)
        //    {
        //        try
        //        {
        //            FindTmpMarker findTmpMarker = new FindTmpMarker(fbd);
        //            findTmpMarker.findTmpMarkerWithSrc();
        //        }
        //        catch(Exception e)
        //        {
        //            MessageBox.Show("Error: can't open folder.\nOriginal error: "+ e.Message);
        //        }
        //    }
        //}

        //public void openFolderWithBin()
        //{
        //    FolderBrowserDialog fbd = new FolderBrowserDialog();
        //    fbd.SelectedPath = @"C:\";
        //    fbd.ShowNewFolderButton = false;

        //    if (fbd.ShowDialog() == DialogResult.OK)
        //    {
        //        try
        //        {
        //            FindTmpMarker findTmpMarker = new FindTmpMarker(fbd);
        //            findTmpMarker.findTmpMarkerWithBin();
        //        }
        //        catch (Exception e)
        //        {
        //            MessageBox.Show("Error: can't open folder.\nOriginal error: " + e.Message);
        //        }
        //    }
        //}

        public void openFolderWithSrcForInsertMarker()
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.SelectedPath = @"C:\";
            fbd.ShowNewFolderButton = false;

            if (fbd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    InsertMarker insertMarker = new InsertMarker(fbd);
                    insertMarker.insertMarker();
                    
                }
                catch (Exception e)
                {
                    MessageBox.Show("Error: can't open folder.\nOriginal error: " + e.Message);
                }
            }
        }

        public void openPathForSaveProject()
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.SelectedPath = @"C:\";

            if (fbd.ShowDialog() == DialogResult.OK)
            {
                PathNewLocation = fbd.SelectedPath;
            }
        }

        public void openPathWithSources()
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.SelectedPath = @"C:\";

            if (fbd.ShowDialog() == DialogResult.OK)
            {
                PathSourcesFolder = fbd.SelectedPath;
            }
        }

        public void insertMarkerInFile()
        {
            InsertMarker insertMarker = new InsertMarker();
            insertMarker.insertMarker();
        }

        public void openFileDB()
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.SelectedPath = @"C:\";

            if (fbd.ShowDialog() == DialogResult.OK)
            {
                PathNewLocation = fbd.SelectedPath;
            }
        }

        
    }
}
