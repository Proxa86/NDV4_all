using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;
using System.Windows.Forms;
using System.Data.SQLite;
using System.Data;

namespace NDV4Sharp
{
    class WorkExcel
    {
        public static SQLiteConnection DbConn { get; set; }

        public static SQLiteCommand SqlCmd { get; set; }
        public DataTable dTableSrc { get; set; }
        public DataTable dTableBin { get; set; }
        public string SqlQuery { get; set; }
        public WorkExcel()
        {
            DbConn = InfoOpenProject.DbConn;
            SqlCmd = InfoOpenProject.SqlCmd;

            dTableSrc = new DataTable();
            dTableBin = new DataTable();

            if (InfoOpenProject.DbConn.State != ConnectionState.Open)
            {
                MessageBox.Show("Open connection with database");
                return;
            }

        }

        public void DisplayInExcelFoundMarker()
        {
            try
            {
                var excelApp = new Excel.Application();

                excelApp.Visible = true;
                excelApp.Workbooks.Add();
                Excel._Worksheet worksheet = (Excel.Worksheet)excelApp.ActiveSheet;
                worksheet.Cells[1, "A"] = "Number marker";
                worksheet.Cells[1, "B"] = "Path";

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

                SqlQuery = "SELECT * FROM BinMarker";
                SQLiteDataAdapter adapterBin = new SQLiteDataAdapter(SqlQuery, InfoOpenProject.DbConn);
                adapterBin.Fill(dTableBin);

                int i;

                if (dTableBin.Rows.Count > 0)
                {
                    //for (i = 0; i < dTableBin.Rows.Count; i++)
                    //{
                    //    string pathbin = dTableBin.Rows[i].ItemArray[2].ToString();
                    //    string marker = dTableBin.Rows[i].ItemArray[3].ToString();

                    //    SqlQuery = "SELECT pathLabFiles FROM WorkMarker WHERE marker = " + marker;
                    //    SQLiteDataAdapter adapterSrc = new SQLiteDataAdapter(SqlQuery, InfoOpenProject.DbConn);
                    //    adapterSrc.Fill(dTableSrc);
                    //    if (dTableBin.Rows.Count > 0)
                    //    {
                    //        worksheet.Cells[i + 1, "B"] = marker.Number;
                    //        worksheet.Cells[i + 1, "C"] = marker.Path;
                    //    }
                    //    worksheet.Columns[1].AutoFit();
                    //worksheet.Columns[2].AutoFit();

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
            catch (Exception ex)
            {
                MessageBox.Show("Markers in bin not found!");
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

        public void DisplayInExcelNotFoundMarker(List<MarkerSrc> lMarkerSrcNotFoundInBin)
        {
            try
            {
                var excelApp = new Excel.Application();

                excelApp.Visible = true;
                excelApp.Workbooks.Add();
                Excel._Worksheet worksheet = (Excel.Worksheet)excelApp.ActiveSheet;
                worksheet.Cells[1, "A"] = "Number marker";
                worksheet.Cells[1, "B"] = "Path";

                int i = 0;
                foreach (var marker in lMarkerSrcNotFoundInBin)
                {
                    worksheet.Cells[i + 2, "A"] = marker.Number;
                    worksheet.Cells[i + 2, "B"] = marker.Path;
                    ++i;
                }

                worksheet.Columns[1].AutoFit();
                worksheet.Columns[2].AutoFit();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Markers not found in bin!");
            }
           


        }









    }
}
