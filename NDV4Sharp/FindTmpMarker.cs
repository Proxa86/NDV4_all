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

        FolderBrowserDialog fbd;


        //static Dictionary<string, string> dPathFileAndNumberTmpMarker;

        static List<MarkerSrc> lMarkersInSrc;
        public SQLiteConnection DbConn { get; set; }

        public SQLiteCommand SqlCmd { get; set; }
        public FindTmpMarker() 
        {
            DbConn = InfoOpenProject.DbConn;
            SqlCmd = InfoOpenProject.SqlCmd;
        }

        public FindTmpMarker(FolderBrowserDialog fbd)
        {
            this.fbd = fbd;
        }

        public void findTmpMarkerWithSrc()
        {
            List<string[]> lParentFilters = new List<string[]>();

            //dPathFileAndNumberTmpMarker = new Dictionary<string, string>();

            lMarkersInSrc = new List<MarkerSrc>();


            //lParentFilters.Add(Directory.GetFiles(fbd.SelectedPath, "*.cs", SearchOption.AllDirectories));

            try
            {

                

            //    foreach (var filter in lParentFilters)
            //    {
            //        foreach (var path in filter)
            //        {
            //            string[] allLinesInFile = File.ReadAllLines(path);

            //            foreach (var line in allLinesInFile)
            //            {
            //                string paternFindTmpMarker = "tmp[0-9]{8}";

            //                Regex regex = new Regex(paternFindTmpMarker);
            //                Match match = regex.Match(line);

            //                if(match.Success)
            //                {
            //                    //Sqlcmd.CommandText = "INSERT INTO WorkMarker ('marker', 'path', 'nameFile') values ('" + match.Value + "', '" + path + "', '" + Path.GetFileName(path) + "')";
            //                    //Sqlcmd.ExecuteNonQuery();

            //                    MarkerSrc markerSrc = new MarkerSrc();
            //                    markerSrc.Number = match.Value;
            //                    markerSrc.Path = path;
            //                    //dPathFileAndNumberTmpMarker.Add(match.Value, path);
            //                    lMarkersInSrc.Add(markerSrc);
            //                    break;
            //                }
                            
            //            }
                        
            //        }
            //    }

            }
            catch (Exception e)
            {
            //    MessageBox.Show("Can't open file.\nOriginal error: " + e.Message);
            }

            
        }

        public void findTmpMarkerWithBin()
        {

            //DataTable dTable = new DataTable();
            //String sqlQuery = "SELECT * FROM WorkMarker WHERE extension = '.cs'";
            //SQLiteDataAdapter adapter = new SQLiteDataAdapter(sqlQuery, InfoOpenProject.DbConn);
            //adapter.Fill(dTable);

            //int i;

            //if (dTable.Rows.Count > 0)
            //{
            //    for (i = 0; i < dTable.Rows.Count; i++)
            //    {
            //        string pathSrcLab = dTable.Rows[i].ItemArray[4].ToString();
            //    }
            //}


            List<string[]> lParentFilters = new List<string[]>(); 
            string pathFolderBin = InfoOpenProject.PathLocation + "\\Lab\\Bin";
            //Dictionary<string, List<string>> dPathBinFileAndListNumberTmpMarker = new Dictionary<string, List<string>>();
            
            //List<MarkerBin> lBinWithMarkers = new List<MarkerBin>();

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

                foreach (var filter in lParentFilters)
                {
                    int i = 1;
                    foreach (var pathBin in filter)
                    {
                        
                        string[] allLinesInFile = File.ReadAllLines(pathBin);
                        //MarkerBin markerBin = new MarkerBin();
                        //            List<string> lNumbersMarkerBin = new List<string>();

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

                                //SqlCmd.CommandText = @"INSERT INTO BinName 
                                //            (idBin,
                                //            nameBin, 
                                //            pathBin
                                //            ) 
                                //            VALUES 
                                //            ($idBin,
                                //            $nameBin, 
                                //            $pathBin 
                                //            )";
                                //SqlCmd.Parameters.AddWithValue("$idBin", i);
                                //SqlCmd.Parameters.AddWithValue("$nameBin", Path.GetFileName(pathBin));
                                //SqlCmd.Parameters.AddWithValue("$pathBin", pathBin);

                                //SqlCmd.ExecuteNonQuery();
                                

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
                                // Проверка на дублирование маркеров в бинарниках

                                //                    if(lNumbersMarkerBin.Count == 0) // если список пустой то добавляем
                                //                    {
                                //                        lNumbersMarkerBin.Add(match.Value);                                  
                                //                    }
                                //                    else if(!lNumbersMarkerBin.Exists(x => x.Equals(match.Value))) //если такого маркера нет, то добавляем, если есть то ничего не делаем
                                //                    {
                                //                        lNumbersMarkerBin.Add(match.Value);
                                //                    }
                                // так как у нас может быть несколько маркеров в одной строке
                                match = match.NextMatch(); // ищем следующее совпадение в текущей строке                       
                            }

                        }
                        //            //dPathBinFileAndListNumberTmpMarker.Add(path, lTmpMarkerBin);
                        //            markerBin.Path = path;
                        //            markerBin.ListNumberMarker = lNumbersMarkerBin;
                        //            lBinWithMarkers.Add(markerBin);
                        ++i;
                    }
                 }

                //    ComparisonTmpMarkers comparisonTmpMarkers = new ComparisonTmpMarkers();
                //    comparisonTmpMarkers.comparisonTmpMarkersFindSrcAndBin(lMarkersInSrc, lBinWithMarkers);
                MessageBox.Show("Analisys end!");
            }
            catch (Exception e)
            {
                MessageBox.Show("Can't open file.\nOriginal error: " + e.Message);
            }

            
        }
    }
}
