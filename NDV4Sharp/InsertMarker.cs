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
        public InfoCreateProject info { get; set; }

        public InsertMarker()
        {
            //info = new InfoCreateProject();

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

            if (InfoCreateProject.DbConn.State != ConnectionState.Open)
            {
                MessageBox.Show("Open connection with database");
                return;
            }

            try
            {
                sqlQuery = "SELECT * FROM WorkMarker WHERE extension = '.cs'";
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(sqlQuery, InfoCreateProject.DbConn);
                adapter.Fill(dTable);

                int i;

                if (dTable.Rows.Count > 0)
                {
                    for (i = 0; i < dTable.Rows.Count; i++)
                    {
                        string pathSrcLab = dTable.Rows[i].ItemArray[4].ToString();
       
                        



                        InfoCreateProject.SqlCmd.CommandText = @"UPDATE WorkMarker SET marker = $marker WHERE pathLabFiles = $pathLabFiles";
                        InfoCreateProject.SqlCmd.Parameters.AddWithValue("$marker", "tmp" + i.ToString("00000000"));
                        InfoCreateProject.SqlCmd.Parameters.AddWithValue("$pathLabFiles", pathSrcLab);
                        InfoCreateProject.SqlCmd.ExecuteNonQuery();

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
                        string pathLogFile = InfoCreateProject.PathNewLocation + "\\" + InfoCreateProject.NameProject + "\\logInsertMarker.txt";
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
