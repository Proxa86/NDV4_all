using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Data.SQLite;
using Microsoft.VisualBasic.FileIO;

namespace NDV4Sharp
{
    public partial class CreateNewProject : Form
    {
        public string NameProject { get; set; }
        public string PathNewLocation { get; set; }

        public string PathSourcesFolder { get; set; }

        public string DbFileName { get; set; }
        public SQLiteConnection DbConn { get; set; }

        public SQLiteCommand SqlCmd { get; set; }

        public CreateNewProject()
        {
            InitializeComponent();

            tbLocationProject.ReadOnly = true;
            tbSourceFolder.ReadOnly = true;
            tbNameProject.Text = "new_project";
            
        }

        private void CreateNewProject_Load(object sender, EventArgs e)
        {
            DbConn = new SQLiteConnection();
            SqlCmd = new SQLiteCommand();
            
        }

        private void bLocationProject_Click(object sender, EventArgs e)
        {
            

            OpenFolder openLocation = new OpenFolder();

            openLocation.openPathForSaveProject();

            PathNewLocation = openLocation.PathNewLocation;
            tbLocationProject.Text = PathNewLocation;

            
        }

        private void bSourceFolder_Click(object sender, EventArgs e)
        {
            OpenFolder openLocation = new OpenFolder();
            openLocation.openPathWithSources();

            PathSourcesFolder = openLocation.PathSourcesFolder;
            tbSourceFolder.Text = PathSourcesFolder;

            
        }

        private void bOK_Click(object sender, EventArgs e)
        {

            NameProject = tbNameProject.Text;
            if (NameProject == "")
            {
                MessageBox.Show("Please, enter name project!");
            }

            DbFileName = NameProject + ".sqlite";

            DirectoryInfo dirInfo = new DirectoryInfo(PathNewLocation + "\\" + NameProject);
            if (!dirInfo.Exists)
                dirInfo.Create();

            if (!File.Exists(PathNewLocation + "\\" + DbFileName))
                SQLiteConnection.CreateFile(PathNewLocation + "\\" + NameProject + "\\" + DbFileName);
            
            try
            {
                DbConn = new SQLiteConnection("Data Source=" + PathNewLocation + "\\" + NameProject + "\\" + DbFileName + ";Version=3;");
                DbConn.Open();
                SqlCmd.Connection = DbConn;

                SqlCmd.CommandText = @"CREATE TABLE IF NOT EXISTS WorkMarker (" +
                                        " id INTEGER PRIMARY KEY AUTOINCREMENT," +
                                        " nameFile TEXT," +
                                        " pathFolderSrc TEXT," +
                                        " pathOrigFiles TEXT," +
                                        " pathLabFiles TEXT," +
                                        " extension TEXT," +
                                        " marker TEXT," +
                                        " markerInBin TEXT," +
                                        " binName TEXT)";

                
                SqlCmd.ExecuteNonQuery();
                SqlCmd.CommandText = @"CREATE TABLE IF NOT EXISTS BinName (" +
                                        " id INTEGER PRIMARY KEY AUTOINCREMENT," +
                                        " idBin TEXT," +
                                        " nameBin TEXT," +
                                        " pathBin TEXT)";
                SqlCmd.ExecuteNonQuery();
                SqlCmd.CommandText = @"CREATE TABLE IF NOT EXISTS BinMarker (" +
                                        " id INTEGER PRIMARY KEY AUTOINCREMENT," +
                                        " idBin TEXT," +
                                        " markerBin TEXT)";
                SqlCmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                //labelInformation.Text = "Disconnected";
                MessageBox.Show("Error: " + ex.Message);
            }

            if (cbCopyFolder.Checked == true)
            {
                string pathOrigVersionSrc = PathNewLocation + "\\" + NameProject + "\\Orig\\Src";
                string pathLabVersionSrc = PathNewLocation + "\\" + NameProject + "\\Lab\\Src";
                string pathLabVersionBin = PathNewLocation + "\\" + NameProject + "\\Lab\\Bin";
                dirInfo = new DirectoryInfo(pathOrigVersionSrc);
                if (!dirInfo.Exists)
                    dirInfo.Create();
                dirInfo = new DirectoryInfo(pathLabVersionSrc);
                if (!dirInfo.Exists)
                    dirInfo.Create();
                dirInfo = new DirectoryInfo(pathLabVersionBin);
                if (!dirInfo.Exists)
                    dirInfo.Create();


                try
                {
                    FileSystem.CopyDirectory(PathSourcesFolder, pathOrigVersionSrc, UIOption.AllDialogs);
                    //File.Copy(newPath, newPath.Replace(PathSourcesFolder, pathOrigVersionSrc), true);

                    FileSystem.CopyDirectory(PathSourcesFolder, pathLabVersionSrc, UIOption.AllDialogs);

                    DbConn = new SQLiteConnection("Data Source=" + PathNewLocation + "\\" + NameProject + "\\" + DbFileName + ";Version=3;");
                    DbConn.Open();
                    SqlCmd.Connection = DbConn;

                    if (DbConn.State != ConnectionState.Open)
                    {
                        MessageBox.Show("Open connection with database");
                        return;
                    }

                    //Копировать все файлы и перезаписать файлы с идентичным именем
                    foreach (string pathSrc in Directory.GetFiles(PathSourcesFolder, "*.*",
                        System.IO.SearchOption.AllDirectories))
                    {
                        //string pathSourceSrc = pathSrc.Replace("'", "");
                        //File.Copy(newPath, newPath.Replace(PathSourcesFolder, pathLabVersionSrc), true);
                        string pathOrigVer = pathSrc.Replace(PathSourcesFolder, pathOrigVersionSrc);
                        string pathLabVer = pathSrc.Replace(PathSourcesFolder, pathLabVersionSrc);
                        //string src = Path.GetFileName(pathSrc);


                        //SqlCmd.CommandText = "INSERT INTO WorkMarker ('nameFile', 'pathFolderSrc', 'pathOrigFiles', 'pathLabFiles', 'extension') " +
                        //    "values ('" + pathSourceSrc + "', '" + pathSourceSrc + "" + "' , '" + pathOrigVer + 
                        //    "', '" + pathLabVer + "', '" + Path.GetExtension(pathSrc) + "')";

                        SqlCmd.CommandText = @"INSERT INTO WorkMarker 
                                            (nameFile, 
                                            pathFolderSrc, 
                                            pathOrigFiles, 
                                            pathLabFiles, 
                                            extension
                                            ) 
                                            VALUES 
                                            ($nameFile, 
                                            $pathFolderSrc, 
                                            $pathOrigFiles, 
                                            $pathLabFiles, 
                                            $extension
                                            )";

                        SqlCmd.Parameters.AddWithValue("$nameFile", Path.GetFileName(pathSrc));
                        SqlCmd.Parameters.AddWithValue("$pathFolderSrc", pathSrc);
                        SqlCmd.Parameters.AddWithValue("$pathOrigFiles", pathOrigVer);
                        SqlCmd.Parameters.AddWithValue("$pathLabFiles", pathLabVer);
                        SqlCmd.Parameters.AddWithValue("$extension", Path.GetExtension(pathSrc));

                        SqlCmd.ExecuteNonQuery();

                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);//здесь обрабатывай ошибки
                }
            }


            else
            {

            }

            new InfoCreateProject(NameProject, PathNewLocation, PathSourcesFolder, DbFileName, DbConn, SqlCmd);

            Close();
        }
    }

      
    
}
