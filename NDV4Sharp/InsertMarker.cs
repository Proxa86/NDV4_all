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
        public string PathLogFile { get; set; }

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
            String sqlQuery = "";

            if (DbConn.State != ConnectionState.Open)
            {
                DbConn = new SQLiteConnection("Data Source=" + PathNewLocation + "\\" + NameProject + "\\" + DbFileName + ";Version=3;");
                DbConn.Open();
                SqlCmd.Connection = DbConn;
            }

            try
            {
                if(Form1.CheckSharp)
                {
                    sqlQuery = "SELECT * FROM WorkMarker WHERE extension = '.cs'";
                    SQLiteDataAdapter adapter_cs = new SQLiteDataAdapter(sqlQuery, DbConn);
                    adapter_cs.Fill(dTable);
                    sqlQuery = "SELECT * FROM WorkMarker WHERE extension = '.CS'";
                    SQLiteDataAdapter adapter_CS = new SQLiteDataAdapter(sqlQuery, DbConn);
                    adapter_CS.Fill(dTable);
                }                    
                else if(Form1.CheckC)
                {
                    sqlQuery = "SELECT * FROM WorkMarker WHERE extension = '.c'";
                    SQLiteDataAdapter adapter_c = new SQLiteDataAdapter(sqlQuery, DbConn);
                    adapter_c.Fill(dTable);
                    sqlQuery = "SELECT * FROM WorkMarker WHERE extension = '.C'";
                    SQLiteDataAdapter adapter_C = new SQLiteDataAdapter(sqlQuery, DbConn);
                    adapter_C.Fill(dTable);
                    sqlQuery = "SELECT * FROM WorkMarker WHERE extension = '.cc'";
                    SQLiteDataAdapter adapter_cc = new SQLiteDataAdapter(sqlQuery, DbConn);
                    adapter_cc.Fill(dTable);
                    sqlQuery = "SELECT * FROM WorkMarker WHERE extension = '.CC'";
                    SQLiteDataAdapter adapter_CC = new SQLiteDataAdapter(sqlQuery, DbConn);
                    adapter_CC.Fill(dTable);
                    sqlQuery = "SELECT * FROM WorkMarker WHERE extension = '.cpp'";
                    SQLiteDataAdapter adapter_cpp = new SQLiteDataAdapter(sqlQuery, DbConn);
                    adapter_cpp.Fill(dTable);
                    sqlQuery = "SELECT * FROM WorkMarker WHERE extension = '.CPP'";
                    SQLiteDataAdapter adapter_CPP = new SQLiteDataAdapter(sqlQuery, DbConn);
                    adapter_CPP.Fill(dTable);
                    sqlQuery = "SELECT * FROM WorkMarker WHERE extension = '.cxx'";
                    SQLiteDataAdapter adapter_cxx = new SQLiteDataAdapter(sqlQuery, DbConn);
                    adapter_cxx.Fill(dTable);
                    sqlQuery = "SELECT * FROM WorkMarker WHERE extension = '.CXX'";
                    SQLiteDataAdapter adapter_CXX = new SQLiteDataAdapter(sqlQuery, DbConn);
                    adapter_CXX.Fill(dTable);
                    sqlQuery = "SELECT * FROM WorkMarker WHERE extension = '.h'";
                    SQLiteDataAdapter adapter_h = new SQLiteDataAdapter(sqlQuery, DbConn);
                    adapter_h.Fill(dTable);
                    sqlQuery = "SELECT * FROM WorkMarker WHERE extension = '.H'";
                    SQLiteDataAdapter adapter_H = new SQLiteDataAdapter(sqlQuery, DbConn);
                    adapter_H.Fill(dTable);
                    sqlQuery = "SELECT * FROM WorkMarker WHERE extension = '.hh'";
                    SQLiteDataAdapter adapter_hh = new SQLiteDataAdapter(sqlQuery, DbConn);
                    adapter_hh.Fill(dTable);
                    sqlQuery = "SELECT * FROM WorkMarker WHERE extension = '.HH'";
                    SQLiteDataAdapter adapter_HH = new SQLiteDataAdapter(sqlQuery, DbConn);
                    adapter_HH.Fill(dTable);
                    sqlQuery = "SELECT * FROM WorkMarker WHERE extension = '.hpp'";
                    SQLiteDataAdapter adapter_hpp = new SQLiteDataAdapter(sqlQuery, DbConn);
                    adapter_hpp.Fill(dTable);
                    sqlQuery = "SELECT * FROM WorkMarker WHERE extension = '.HPP'";
                    SQLiteDataAdapter adapter_HPP = new SQLiteDataAdapter(sqlQuery, DbConn);
                    adapter_HPP.Fill(dTable);
                    sqlQuery = "SELECT * FROM WorkMarker WHERE extension = '.hхх'";
                    SQLiteDataAdapter adapter_hxx = new SQLiteDataAdapter(sqlQuery, DbConn);
                    adapter_hxx.Fill(dTable);
                    sqlQuery = "SELECT * FROM WorkMarker WHERE extension = '.HXX'";
                    SQLiteDataAdapter adapter_HХХ = new SQLiteDataAdapter(sqlQuery, DbConn);
                    adapter_HХХ.Fill(dTable);
                }
                else if (Form1.CheckFortran)
                {
                    sqlQuery = "SELECT * FROM WorkMarker WHERE extension = '.f'";
                    SQLiteDataAdapter adapter_f = new SQLiteDataAdapter(sqlQuery, DbConn);
                    adapter_f.Fill(dTable);
                    sqlQuery = "SELECT * FROM WorkMarker WHERE extension = '.F'";
                    SQLiteDataAdapter adapter_F = new SQLiteDataAdapter(sqlQuery, DbConn);
                    adapter_F.Fill(dTable);
                    sqlQuery = "SELECT * FROM WorkMarker WHERE extension = '.f90'";
                    SQLiteDataAdapter adapter_f90 = new SQLiteDataAdapter(sqlQuery, DbConn);
                    adapter_f90.Fill(dTable);
                    sqlQuery = "SELECT * FROM WorkMarker WHERE extension = '.F90'";
                    SQLiteDataAdapter adapter_F90 = new SQLiteDataAdapter(sqlQuery, DbConn);
                    adapter_F90.Fill(dTable);
                }


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

                        if (Form1.CheckSharp)
                        {
                            using (StreamWriter sw = new StreamWriter(new FileStream(pathSrcLab, FileMode.Append)))
                            {
                                await sw.WriteLineAsync(String.Format(
        @"
#if !NUM_MARKER{0}
class tmp{1}{{}}
#endif", i.ToString("00000000"), i.ToString("00000000")));


                                sw.Close();
                            }
                        }
                        else if (Form1.CheckC)
                        {
                            using (StreamWriter sw = new StreamWriter(new FileStream(pathSrcLab, FileMode.Append)))
                            {
                                await sw.WriteLineAsync(String.Format(
        @"
#ifndef NUM_MARKER{0}
#define NUM_MARKER{1}
void tmp{2}(){{}};
#endif", i.ToString("00000000"), i.ToString("00000000"), i.ToString("00000000")));


                                sw.Close();
                            }
                        }
                        else if (Form1.CheckFortran)
                        {
                            using (StreamWriter sw = new StreamWriter(new FileStream(pathSrcLab, FileMode.Append)))
                            {
                                await sw.WriteLineAsync(String.Format(
        @"
#ifndef NUM_MARKER{0}
      SUBROUTINE tmp{1}()
      STOP """"
      END
#endif", i.ToString("00000000"), i.ToString("00000000")));


                                sw.Close();
                            }
                        }
                        // Запись в Log.txt находящегося возле бинарника
                       PathLogFile = PathNewLocation + "\\" + NameProject + "\\logInsertMarker.txt";
                        if (!File.Exists(PathLogFile))
                        {
                            using (StreamWriter sw = new StreamWriter(PathLogFile, false, System.Text.Encoding.Default))
                            {
                                await sw.WriteLineAsync(String.Format("tmp{0}", i.ToString("00000000") + "\t" + pathSrcLab));
                            }
                        }
                        else
                        {
                            using (StreamWriter sw = new StreamWriter(PathLogFile, true, System.Text.Encoding.Default))
                            {
                                await sw.WriteLineAsync(String.Format("tmp{0}", i.ToString("00000000") + "\t" + pathSrcLab));
                            }
                        }
                    }

                    using (StreamWriter sw = new StreamWriter(PathLogFile, true, System.Text.Encoding.Default))
                    {
                        await sw.WriteLineAsync(String.Format("\nInsert " + i + " markers."));
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
