using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SQLite;

namespace NDV4Sharp
{
    class InsertMarker
    {
        FolderBrowserDialog fbd;
        public static SQLiteConnection DbConn { get; set; }

        public static SQLiteCommand SqlCmd { get; set; }
        public static string NameProject { get; set; }
        public static string PathNewLocation { get; set; }

        public static string PathSourcesFolder { get; set; }

        public static string DbFileName { get; set; }

        public InsertMarker()
        {
            //info = new InfoCreateProject();
            SqlCmd = new SQLiteCommand();
            DbConn = new SQLiteConnection();
            NameProject = InfoCreateProject.NameProject;
            PathNewLocation = InfoCreateProject.PathNewLocation;
            DbFileName = InfoCreateProject.DbFileName;

        }
        public InsertMarker(FolderBrowserDialog fbd)
        {
            this.fbd = fbd;
        }

        public async void insertMarker()
        {
            List<string[]> lParentFilters = new List<string[]>();

            DataTable dTable = new DataTable();
            String sqlQuery;

            if (DbConn.State != ConnectionState.Open)
            {
                DbConn = new SQLiteConnection("Data Source=" + PathNewLocation + "\\" + NameProject + "\\" + DbFileName + ";Version=3;");
                DbConn.Open();
                SqlCmd.Connection = DbConn;
            }

            try
            {
                sqlQuery = "SELECT * FROM WorkMarker WHERE extension = '.cs'";
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(sqlQuery, DbConn);
                adapter.Fill(dTable);

                int i;

                if (dTable.Rows.Count > 0)
                {
                    for (i = 0; i < dTable.Rows.Count; i++)
                    {
                        string pathSrcLab = dTable.Rows[i].ItemArray[4].ToString();
       
                        



                        SqlCmd.CommandText = @"UPDATE WorkMarker SET marker = $marker WHERE pathLabFiles = $pathLabFiles";
                        SqlCmd.Parameters.AddWithValue("$marker", "tmp" + i.ToString("00000000"));
                        SqlCmd.Parameters.AddWithValue("$pathLabFiles", pathSrcLab);
                        SqlCmd.ExecuteNonQuery();

                        using (StreamWriter sw = new StreamWriter(new FileStream(pathSrcLab, FileMode.Append)))
                        {
                            await sw.WriteLineAsync(String.Format(
    @"
#if !NUM_MARKER{0}
class tmp{1}{{}}
#endif", i.ToString("00000000"), i.ToString("00000000")));


                            sw.Close();
                        }
                        // Запись в Log.txt находящегося возле бинарника
                        string pathLogFile = PathNewLocation + "\\" + NameProject + "\\logInsertMarker.txt";
                        if (!File.Exists(pathLogFile))
                        {
                            using (StreamWriter sw = new StreamWriter(pathLogFile, false, System.Text.Encoding.Default))
                            {
                                await sw.WriteLineAsync(String.Format("tmp{0}", i.ToString("00000000") + "\t" + pathSrcLab));
                            }
                        }
                        else
                        {
                            using (StreamWriter sw = new StreamWriter(pathLogFile, true, System.Text.Encoding.Default))
                            {
                                await sw.WriteLineAsync(String.Format("tmp{0}", i.ToString("00000000") + "\t" + pathSrcLab));
                            }
                        }
                    }
                
                    MessageBox.Show("Insert " + i + " markers.");
            
                }
                else
                    MessageBox.Show("Database is empty");
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

        }

//        public async void insertMarker()
//        {
//            List<string[]> lParentFilters = new List<string[]>();

//            lParentFilters.Add(Directory.GetFiles(fbd.SelectedPath, "*.cs", SearchOption.AllDirectories));

//            string baseDirectoryBin = AppDomain.CurrentDomain.BaseDirectory;
//            string writePathAndMarkerInLog = String.Concat(baseDirectoryBin,"logInsertMarker.txt");

//            try
//            {
//                if (File.Exists("logInsertMarker.txt"))
//                {
//                    DialogResult resultButton = MessageBox.Show("File logInsertMarker.txt exists!\nDelete this file?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
//                    if (resultButton == DialogResult.Yes)
//                        File.Delete("logInsertMarker.txt");
//                }

//                int i = 1;
//                foreach (var filter in lParentFilters)
//                {
//                    foreach (var path in filter)
//                    {
//                        using (StreamWriter sw = new StreamWriter(new FileStream(path, FileMode.Append)))
//                        {
//                            await sw.WriteLineAsync(String.Format(
//                                @"
//#if !NUM_MARKER{0}
//class tmp{1}{{}}
//#endif", i.ToString("00000000"), i.ToString("00000000")));

//                        }
//                        //sw.Close();

//                        // Запись в Log.txt находящегося возле бинарника

//                        if (!System.IO.File.Exists("logInsertMarker.txt"))
//                        {
//                            using (StreamWriter sw = new StreamWriter(writePathAndMarkerInLog, false, System.Text.Encoding.Default))
//                            {
//                                await sw.WriteLineAsync(String.Format("tmp{0}", i.ToString("00000000") + "\t" + path));
//                            }
//                        }
//                        else
//                        {
//                            using (StreamWriter sw = new StreamWriter(writePathAndMarkerInLog, true, System.Text.Encoding.Default))
//                            {
//                                await sw.WriteLineAsync(String.Format("tmp{0}", i.ToString("00000000") + "\t" + path));
//                            }
//                        }

//                        ++i;
//                    }
//                }
//                MessageBox.Show("Insert " + --i + " markers.");

//            }
//            catch (Exception e)
//            {
//                MessageBox.Show("Can't open file.\nOriginal error: " + e.Message);
//            }
//        }
    }
}
