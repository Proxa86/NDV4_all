﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;
using System.Windows.Forms;
using System.Data.SQLite;
using System.Data;
using System.IO;

namespace NDV4Sharp
{
    class WorkExcel
    {
        public SQLiteConnection DbConn { get; set; }

        public SQLiteCommand SqlCmd { get; set; }
        public string PathLocation { get; set; }
        public string SqlQuery { get; set; }
        public string PathFolderBin { get; set; }
        public WorkExcel()
        {
            DbConn = InfoOpenProject.DbConn;
            SqlCmd = InfoOpenProject.SqlCmd;
            PathLocation = InfoOpenProject.PathLocation;

            

            if (DbConn == null || DbConn.State != ConnectionState.Open)
            {
                MessageBox.Show("Open connection with database");
                return;
            }
            else if(PathLocation == null)
            {
                MessageBox.Show("Open connection with database");
                return;
            }

        }

        public void DisplayInExcelFoundMarker()
        {
            if (DbConn == null)
                return;
            else if (PathLocation == null)
            {
                return;
            }

            try
            {
                var excelApp = new Excel.Application();

                excelApp.Visible = true;
                excelApp.Workbooks.Add();
                Excel._Worksheet worksheet = (Excel.Worksheet)excelApp.ActiveSheet;
                worksheet.Cells[1, "A"] = "Number marker";
                worksheet.Cells[1, "B"] = "Path";

                DataTable dTableSrc = new DataTable();
                SqlQuery = "SELECT * FROM WorkMarker WHERE markerInBin = 1";
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(SqlQuery, InfoOpenProject.DbConn);
                adapter.Fill(dTableSrc);

                int i;

                if (dTableSrc.Rows.Count > 0)
                {
                    for (i = 0; i < dTableSrc.Rows.Count; i++)
                    {
                        string pathSrcLab = dTableSrc.Rows[i].ItemArray[4].ToString();
                        string marker = dTableSrc.Rows[i].ItemArray[6].ToString();
                        worksheet.Cells[i + 2, "A"] = marker;
                        worksheet.Cells[i + 2, "B"] = pathSrcLab;
                    }
                    worksheet.Columns[1].AutoFit();
                    worksheet.Columns[2].AutoFit();

                }



                //foreach (var marker in lMarkerSrcFoundInBin)
                //{
                //    worksheet.Cells[i + 2, "A"] = marker.Number;
                //    worksheet.Cells[i + 2, "B"] = marker.Path;
                //    ++i;
                //}

                //worksheet.Columns[1].AutoFit();
                //worksheet.Columns[2].AutoFit();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Markers in bin not found!");
            }



        }

        //public void DisplayInExcelFoundMarker(List<MarkerSrc> lMarkerSrcFoundInBin)
        //{
        //    try
        //    {
        //        var excelApp = new Excel.Application();

        //        excelApp.Visible = true;
        //        excelApp.Workbooks.Add();
        //        Excel._Worksheet worksheet = (Excel.Worksheet)excelApp.ActiveSheet;
        //        worksheet.Cells[1, "A"] = "Number marker";
        //        worksheet.Cells[1, "B"] = "Path";

        //        int i = 0;
        //        foreach (var marker in lMarkerSrcFoundInBin)
        //        {
        //            worksheet.Cells[i + 2, "A"] = marker.Number;
        //            worksheet.Cells[i + 2, "B"] = marker.Path;
        //            ++i;
        //        }

        //        worksheet.Columns[1].AutoFit();
        //        worksheet.Columns[2].AutoFit();
        //    }
        //    catch(Exception ex)
        //    {
        //        MessageBox.Show("Markers in bin not found!");
        //    }



        //}

        public void DisplayInExcelFoundInBinMarker()
        {
            if (DbConn == null)
                return;
            else if (PathLocation == null)
            {
                return;
            }

            try
            {
                var excelApp = new Excel.Application();

                excelApp.Visible = true;
                excelApp.Workbooks.Add();
                Excel._Worksheet worksheet = (Excel.Worksheet)excelApp.ActiveSheet;
                //worksheet.Cells[1, "A"] = "Number marker";
                //worksheet.Cells[1, "B"] = "Path";

                //SqlQuery = "SELECT * FROM WorkMarker WHERE markerInBin = 1";
                //SQLiteDataAdapter adapterSrc = new SQLiteDataAdapter(SqlQuery, InfoOpenProject.DbConn);
                //adapterSrc.Fill(dTableSrc);

                DataTable dTableBinName = new DataTable();
                
                
                SqlQuery = "SELECT * FROM BinName";
                SQLiteDataAdapter adapterBin = new SQLiteDataAdapter(SqlQuery, InfoOpenProject.DbConn);
                adapterBin.Fill(dTableBinName);

                int i,j,k=1;


                if (dTableBinName.Rows.Count > 0)
                {
                    for (i = 0; i < dTableBinName.Rows.Count; i++)
                    {
                        string pathBin = dTableBinName.Rows[i].ItemArray[3].ToString();
                        worksheet.Cells[k, "A"] = pathBin;
                        string idBin = dTableBinName.Rows[i].ItemArray[1].ToString();

                        DataTable dTableBinMarker = new DataTable();
                        SqlQuery = "SELECT markerBin FROM BinMarker WHERE idBin = " + idBin;
                        adapterBin = new SQLiteDataAdapter(SqlQuery, InfoOpenProject.DbConn);
                        adapterBin.Fill(dTableBinMarker);
                        if (dTableBinMarker.Rows.Count > 0)
                        {
                            for (j = 0; j < dTableBinMarker.Rows.Count; j++)
                            {
                                DataTable dTableNameSrc = new DataTable();
                                string marker = dTableBinMarker.Rows[j].ItemArray[0].ToString();
                                SqlQuery = "SELECT pathLabFiles FROM WorkMarker WHERE marker = '" + marker + "'";
                                adapterBin = new SQLiteDataAdapter(SqlQuery, InfoOpenProject.DbConn);
                                adapterBin.Fill(dTableNameSrc);
                                if (dTableNameSrc.Rows.Count > 0)
                                {
                                    string pathSrc = dTableNameSrc.Rows[0].ItemArray[0].ToString();
                                    worksheet.Cells[k + 1, "B"] = marker;
                                    worksheet.Cells[k + 1, "C"] = pathSrc;
                                }
                                ++k;
                            }
                                   
                        }
                        ++k;
                    }
                    worksheet.Columns[1].AutoFit();
                    worksheet.Columns[2].AutoFit();

                }
                //int i = 1;
                //int j = 0;
                //foreach (var bin in lResultMarkerInBin)
                //{
                //    worksheet.Cells[i, "A"] = bin.Path;

                //    foreach (var marker in bin.LMarkerInBin)
                //    {
                //        worksheet.Cells[i + 1, "B"] = marker.Number;
                //        worksheet.Cells[i + 1, "C"] = marker.Path;
                //        ++i;
                //    }
                //    ++i;
                //}

                //worksheet.Columns[2].AutoFit();
                //worksheet.Columns[3].AutoFit();
            }
            catch(System.Runtime.InteropServices.COMException ex)
            {
                MessageBox.Show("Excel bloked.\n" + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Markers in bin not found!\n" + ex.Message);
            }


        }

        //public void DisplayInExcelFoundInBinMarker(List<ResultMarkersInBin> lResultMarkerInBin)
        //{
        //    try
        //    {
        //        var excelApp = new Excel.Application();

        //        excelApp.Visible = true;
        //        excelApp.Workbooks.Add();
        //        Excel._Worksheet worksheet = (Excel.Worksheet)excelApp.ActiveSheet;
        //        //worksheet.Cells[1, "A"] = "Number marker";
        //        //worksheet.Cells[1, "B"] = "Path";

        //        int i = 1;
        //        int j = 0;
        //        foreach (var bin in lResultMarkerInBin)
        //        {
        //            worksheet.Cells[i, "A"] = bin.Path;

        //            foreach (var marker in bin.LMarkerInBin)
        //            {
        //                worksheet.Cells[i + 1, "B"] = marker.Number;
        //                worksheet.Cells[i + 1, "C"] = marker.Path;
        //                ++i;
        //            }
        //            ++i;
        //        }

        //        worksheet.Columns[2].AutoFit();
        //        worksheet.Columns[3].AutoFit();
        //    }
        //    catch(Exception ex)
        //    {
        //        MessageBox.Show("Markers in bin not found!");
        //    }


        //}

        public void DisplayInExcelNotFoundMarker()
        {
            if (DbConn == null)
                return;
            else if (PathLocation == null)
            {
                return;
            }

            try
            {
                var excelApp = new Excel.Application();

                excelApp.Visible = true;
                excelApp.Workbooks.Add();
                Excel._Worksheet worksheet = (Excel.Worksheet)excelApp.ActiveSheet;
                worksheet.Cells[1, "A"] = "Number marker";
                worksheet.Cells[1, "B"] = "Path";

                DataTable dTableSrc = new DataTable();
                SqlQuery = "SELECT * FROM WorkMarker WHERE markerInBin <> '1'";
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(SqlQuery, InfoOpenProject.DbConn);
                adapter.Fill(dTableSrc);

                int i;

                if (dTableSrc.Rows.Count > 0)
                {
                    for (i = 0; i < dTableSrc.Rows.Count; i++)
                    {
                        string pathSrcLab = dTableSrc.Rows[i].ItemArray[4].ToString();
                        string marker = dTableSrc.Rows[i].ItemArray[6].ToString();
                        worksheet.Cells[i + 2, "A"] = marker;
                        worksheet.Cells[i + 2, "B"] = pathSrcLab;
                    }
                    worksheet.Columns[1].AutoFit();
                    worksheet.Columns[2].AutoFit();

                }
                //int i = 0;
                //foreach (var marker in lMarkerSrcNotFoundInBin)
                //{
                //    worksheet.Cells[i + 2, "A"] = marker.Number;
                //    worksheet.Cells[i + 2, "B"] = marker.Path;
                //    ++i;
                //}

                worksheet.Columns[1].AutoFit();
                worksheet.Columns[2].AutoFit();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Markers not found in bin!");
            }



        }

        //public void DisplayInExcelNotFoundMarker(List<MarkerSrc> lMarkerSrcNotFoundInBin)
        //{
        //    try
        //    {
        //        var excelApp = new Excel.Application();

        //        excelApp.Visible = true;
        //        excelApp.Workbooks.Add();
        //        Excel._Worksheet worksheet = (Excel.Worksheet)excelApp.ActiveSheet;
        //        worksheet.Cells[1, "A"] = "Number marker";
        //        worksheet.Cells[1, "B"] = "Path";

        //        int i = 0;
        //        foreach (var marker in lMarkerSrcNotFoundInBin)
        //        {
        //            worksheet.Cells[i + 2, "A"] = marker.Number;
        //            worksheet.Cells[i + 2, "B"] = marker.Path;
        //            ++i;
        //        }

        //        worksheet.Columns[1].AutoFit();
        //        worksheet.Columns[2].AutoFit();
        //    }
        //    catch(Exception ex)
        //    {
        //        MessageBox.Show("Markers not found in bin!");
        //    }



        //}
    }
}
