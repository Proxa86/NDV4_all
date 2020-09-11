using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using System.Data;

namespace NDV4Sharp
{
    class FindTmpMarker
    {
        public SQLiteConnection DbConn { get; set; }

        public SQLiteCommand SqlCmd { get; set; }
        public FindTmpMarker() 
        {
            DbConn = InfoOpenProject.DbConn;
            SqlCmd = InfoOpenProject.SqlCmd;
        }

        public void findTmpMarkerWithBin()
        {
            List<string[]> lParentFilters = new List<string[]>(); 
            string pathFolderBin = InfoOpenProject.PathLocation + "\\Lab\\Bin";

            lParentFilters.Add(Directory.GetFiles(pathFolderBin, "*", SearchOption.AllDirectories));

            if (!Directory.EnumerateFiles(pathFolderBin, "*.*", SearchOption.AllDirectories).Any())
            {
                MessageBox.Show("Folder Src/Bin is empty!");
            }

            try
            {
                SqlCmd.CommandText = @"DELETE FROM BinMarker";
                SqlCmd.ExecuteNonQuery();
                SqlCmd.CommandText = @"DELETE FROM BinName";
                SqlCmd.ExecuteNonQuery();

                using (SQLiteTransaction transation = SqlCmd.Connection.BeginTransaction())
                {
                    foreach (var filter in lParentFilters)
                    {
                        int i = 1;
                        foreach (var pathBin in filter)
                        {

                            string[] allLinesInFile = File.ReadAllLines(pathBin);

                            SqlCmd.CommandText = @"INSERT INTO BinName 
                                            (idBin,
                                            nameBin, 
                                            pathBin
                                            ) 
                                            VALUES 
                                            ($idBin,
                                            $nameBin, 
                                            $pathBin 
                                            )";

                            SqlCmd.Parameters.AddWithValue("$idBin", i);
                            SqlCmd.Parameters.AddWithValue("$nameBin", Path.GetFileName(pathBin));
                            SqlCmd.Parameters.AddWithValue("$pathBin", pathBin);

                            SqlCmd.ExecuteNonQuery();

                            foreach (var line in allLinesInFile)
                            {
                                string paternFindTmpMarker = "tmp[0-9]{8}";

                                Regex regex = new Regex(paternFindTmpMarker);
                                Match match = regex.Match(line);

                                while (match.Success)
                                {
                                    string marker = match.Value;

                                    SqlCmd.CommandText = @"UPDATE WorkMarker SET markerInBin = $markerInBin WHERE marker = $marker";
                                    SqlCmd.Parameters.AddWithValue("$markerInBin", "1");
                                    SqlCmd.Parameters.AddWithValue("$marker", marker);

                                    SqlCmd.ExecuteNonQuery();

                                    SqlCmd.CommandText = @"INSERT INTO BinMarker 
                                            (idBin,
                                            markerBin
                                            ) 
                                            VALUES 
                                            ($idBin,
                                            $markerBin
                                            )";

                                    SqlCmd.Parameters.AddWithValue("$idBin", i);
                                    SqlCmd.Parameters.AddWithValue("$markerBin", marker);

                                    SqlCmd.ExecuteNonQuery();

                                    match = match.NextMatch(); // ищем следующее совпадение в текущей строке                       
                                }
                            }
                            ++i;
                        }
                    }
                    transation.Commit();
                }

                MessageBox.Show("Analisys end!");
            }
            catch (Exception e)
            {
                MessageBox.Show("Can't open file.\nOriginal error: " + e.Message);
            }   
        }
    }
}
