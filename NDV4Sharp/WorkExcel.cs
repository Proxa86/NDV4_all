using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Excel = Microsoft.Office.Interop.Excel;
using OfficeOpenXml;
using System.Drawing;
using OfficeOpenXml.Style;
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
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                using (var package = new ExcelPackage())
                {
                    var worksheet = package.Workbook.Worksheets.Add("Found marker");
                    worksheet.Cells[1, 1].Value = "Number marker";
                    worksheet.Cells[1, 2].Value = "Path";

                    DataTable dTableSrc = new DataTable();
                    SqlQuery = "SELECT * FROM WorkMarker WHERE markerInBin = 1";
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(SqlQuery, InfoOpenProject.DbConn);
                    adapter.Fill(dTableSrc);

                    int i;
                    //
                    if (dTableSrc.Rows.Count > 0)
                    {
                        for (i = 0; i < dTableSrc.Rows.Count; i++)
                        {
                            string pathSrcLab = dTableSrc.Rows[i].ItemArray[4].ToString();
                            string marker = dTableSrc.Rows[i].ItemArray[6].ToString();
                            worksheet.Cells["A" + (i+2)].Value = marker;
                            worksheet.Cells["B" + (i+2)].Value = pathSrcLab;
                        }
                        worksheet.Cells.AutoFitColumns(1);
                    }
                    package.SaveAs(new FileInfo("Found marker.xlsx"));
                }
            }
            catch (System.Runtime.InteropServices.COMException ex)
            {
                MessageBox.Show("Excel bloked.\n" + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Excel don't install!\n" + ex.Message);
            }
        }

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
                
                    //var excelApp = new Excel.Application();

                    //excelApp.Visible = true;
                    //excelApp.Workbooks.Add();
                    //Excel._Worksheet worksheet = (Excel.Worksheet)excelApp.ActiveSheet;

                DataTable dTableBinName = new DataTable();
                
                
                SqlQuery = "SELECT * FROM BinName";
                SQLiteDataAdapter adapterBin = new SQLiteDataAdapter(SqlQuery, InfoOpenProject.DbConn);
                adapterBin.Fill(dTableBinName);

                int i,j,k=1;


                if (dTableBinName.Rows.Count > 0)
                {
                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                    using (var package = new ExcelPackage())
                    {
                        var worksheet = package.Workbook.Worksheets.Add("Found marker in bin");
                        worksheet.Cells[1, 1].Value = "Binary";
                        worksheet.Cells[1, 2].Value = "Number marker";
                        worksheet.Cells[1, 3].Value = "Path";
                        for (i = 0; i < dTableBinName.Rows.Count; i++)
                        {
                            string pathBin = dTableBinName.Rows[i].ItemArray[3].ToString();
                            worksheet.Cells["A"+ (k + 1)].Value = pathBin;
                            string idBin = dTableBinName.Rows[i].ItemArray[1].ToString();

                            DataTable dTableBinMarker = new DataTable();
                            SqlQuery = "SELECT DISTINCT markerBin FROM BinMarker WHERE idBin = " + idBin;
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
                                        worksheet.Cells["B" + (k + 2)].Value = marker;
                                        worksheet.Cells["C" + (k + 2)].Value = pathSrc;
                                    }
                                    ++k;
                                }
                            }
                            ++k;
                        }
                        worksheet.Cells.AutoFitColumns(1);
                        package.SaveAs(new FileInfo("Found marker in bin.xlsx"));
                    }
                }
            }
            catch(System.Runtime.InteropServices.COMException ex)
            {
                MessageBox.Show("Excel bloked.\n" + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Excel don't install!\n" + ex.Message);
            }


        }
        public void DisplayInExcelNotFoundMarker()
        {
            if (DbConn == null)
                return;
            else if (PathLocation == null)
            {
                return;
            }

            if (!(Form1.CheckSharp || Form1.CheckC || Form1.CheckFortran))
            {
                MessageBox.Show("Please, choose language!");
                return;
            }
                
            try
            {
                //var excelApp = new Excel.Application();
                //excelApp.Visible = true;
                //excelApp.Workbooks.Add();
                //Excel._Worksheet worksheet = (Excel.Worksheet)excelApp.ActiveSheet;
                //worksheet.Cells[1, "A"] = "Number marker";
                //worksheet.Cells[1, "B"] = "Path";

                DataTable dTableSrc = new DataTable();
                if(Form1.CheckSharp)
                {
                    SqlQuery = "SELECT pathLabFiles, marker FROM WorkMarker WHERE extension = '.cs' AND markerInBin is NULL";
                    SQLiteDataAdapter adapter_cs = new SQLiteDataAdapter(SqlQuery, InfoOpenProject.DbConn);
                    adapter_cs.Fill(dTableSrc);
                    SqlQuery = "SELECT pathLabFiles, marker FROM WorkMarker WHERE extension = '.CS' AND markerInBin is NULL";
                    SQLiteDataAdapter adapter_CS = new SQLiteDataAdapter(SqlQuery, InfoOpenProject.DbConn);
                    adapter_CS.Fill(dTableSrc);
                }
                    
                else if(Form1.CheckC)
                {
                    SqlQuery = "SELECT pathLabFiles, marker FROM WorkMarker WHERE extension = '.c' AND markerInBin is NULL";
                    SQLiteDataAdapter adapter_c = new SQLiteDataAdapter(SqlQuery, InfoOpenProject.DbConn);
                    adapter_c.Fill(dTableSrc);
                    SqlQuery = "SELECT pathLabFiles, marker FROM WorkMarker WHERE extension = '.C' AND markerInBin is NULL";
                    SQLiteDataAdapter adapter_C = new SQLiteDataAdapter(SqlQuery, InfoOpenProject.DbConn);
                    adapter_C.Fill(dTableSrc);
                    SqlQuery = "SELECT pathLabFiles, marker FROM WorkMarker WHERE extension = '.cc' AND markerInBin is NULL";
                    SQLiteDataAdapter adapter_cc = new SQLiteDataAdapter(SqlQuery, InfoOpenProject.DbConn);
                    adapter_cc.Fill(dTableSrc);
                    SqlQuery = "SELECT pathLabFiles, marker FROM WorkMarker WHERE extension = '.CC' AND markerInBin is NULL";
                    SQLiteDataAdapter adapter_CC = new SQLiteDataAdapter(SqlQuery, InfoOpenProject.DbConn);
                    adapter_CC.Fill(dTableSrc);
                    SqlQuery = "SELECT pathLabFiles, marker FROM WorkMarker WHERE extension = '.cpp' AND markerInBin is NULL";
                    SQLiteDataAdapter adapter_cpp = new SQLiteDataAdapter(SqlQuery, InfoOpenProject.DbConn);
                    adapter_cpp.Fill(dTableSrc);
                    SqlQuery = "SELECT pathLabFiles, marker FROM WorkMarker WHERE extension = '.CPP' AND markerInBin is NULL";
                    SQLiteDataAdapter adapter_CPP = new SQLiteDataAdapter(SqlQuery, InfoOpenProject.DbConn);
                    adapter_CPP.Fill(dTableSrc);
                    SqlQuery = "SELECT pathLabFiles, marker FROM WorkMarker WHERE extension = '.cxx' AND markerInBin is NULL";
                    SQLiteDataAdapter adapter_cxx = new SQLiteDataAdapter(SqlQuery, InfoOpenProject.DbConn);
                    adapter_cxx.Fill(dTableSrc);
                    SqlQuery = "SELECT pathLabFiles, marker FROM WorkMarker WHERE extension = '.CXX' AND markerInBin is NULL";
                    SQLiteDataAdapter adapter_CXX = new SQLiteDataAdapter(SqlQuery, InfoOpenProject.DbConn);
                    adapter_CXX.Fill(dTableSrc);
                    SqlQuery = "SELECT pathLabFiles, marker FROM WorkMarker WHERE extension = '.h' AND markerInBin is NULL";
                    SQLiteDataAdapter adapter_h = new SQLiteDataAdapter(SqlQuery, InfoOpenProject.DbConn);
                    adapter_h.Fill(dTableSrc);
                    SqlQuery = "SELECT pathLabFiles, marker FROM WorkMarker WHERE extension = '.H' AND markerInBin is NULL";
                    SQLiteDataAdapter adapter_H = new SQLiteDataAdapter(SqlQuery, InfoOpenProject.DbConn);
                    adapter_H.Fill(dTableSrc);
                    SqlQuery = "SELECT pathLabFiles, marker FROM WorkMarker WHERE extension = '.hh' AND markerInBin is NULL";
                    SQLiteDataAdapter adapter_hh = new SQLiteDataAdapter(SqlQuery, InfoOpenProject.DbConn);
                    adapter_hh.Fill(dTableSrc);
                    SqlQuery = "SELECT pathLabFiles, marker FROM WorkMarker WHERE extension = '.HH' AND markerInBin is NULL";
                    SQLiteDataAdapter adapter_HH = new SQLiteDataAdapter(SqlQuery, InfoOpenProject.DbConn);
                    adapter_HH.Fill(dTableSrc);
                    SqlQuery = "SELECT pathLabFiles, marker FROM WorkMarker WHERE extension = '.hpp' AND markerInBin is NULL";
                    SQLiteDataAdapter adapter_hpp = new SQLiteDataAdapter(SqlQuery, InfoOpenProject.DbConn);
                    adapter_hpp.Fill(dTableSrc);
                    SqlQuery = "SELECT pathLabFiles, marker FROM WorkMarker WHERE extension = '.HPP' AND markerInBin is NULL";
                    SQLiteDataAdapter adapter_HPP = new SQLiteDataAdapter(SqlQuery, InfoOpenProject.DbConn);
                    adapter_HPP.Fill(dTableSrc);
                    SqlQuery = "SELECT pathLabFiles, marker FROM WorkMarker WHERE extension = '.hxx' AND markerInBin is NULL";
                    SQLiteDataAdapter adapter_hxx = new SQLiteDataAdapter(SqlQuery, InfoOpenProject.DbConn);
                    adapter_hxx.Fill(dTableSrc);
                    SqlQuery = "SELECT pathLabFiles, marker FROM WorkMarker WHERE extension = '.HXX' AND markerInBin is NULL";
                    SQLiteDataAdapter adapter_HXX = new SQLiteDataAdapter(SqlQuery, InfoOpenProject.DbConn);
                    adapter_HXX.Fill(dTableSrc);
                }
                else if (Form1.CheckFortran)
                {
                    SqlQuery = "SELECT pathLabFiles, marker FROM WorkMarker WHERE extension = '.f90' AND markerInBin is NULL";
                    SQLiteDataAdapter adapter_f90 = new SQLiteDataAdapter(SqlQuery, DbConn);
                    adapter_f90.Fill(dTableSrc);
                    SqlQuery = "SELECT pathLabFiles, marker FROM WorkMarker WHERE extension = '.F90' AND markerInBin is NULL";
                    SQLiteDataAdapter adapter_F90 = new SQLiteDataAdapter(SqlQuery, DbConn);
                    adapter_F90.Fill(dTableSrc);
                    SqlQuery = "SELECT pathLabFiles, marker FROM WorkMarker WHERE extension = '.f' AND markerInBin is NULL";
                    SQLiteDataAdapter adapter_f = new SQLiteDataAdapter(SqlQuery, DbConn);
                    adapter_f.Fill(dTableSrc);
                    SqlQuery = "SELECT pathLabFiles, marker FROM WorkMarker WHERE extension = '.F' AND markerInBin is NULL";
                    SQLiteDataAdapter adapter_F = new SQLiteDataAdapter(SqlQuery, DbConn);
                    adapter_F.Fill(dTableSrc);
                }


                int i;

                if (dTableSrc.Rows.Count > 0)
                {
                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                    using (var package = new ExcelPackage())
                    {
                        var worksheet = package.Workbook.Worksheets.Add("Not found marker in bin");
                        worksheet.Cells[1, 1].Value = "Number marker";
                        worksheet.Cells[1, 2].Value = "Path";
                        for (i = 0; i < dTableSrc.Rows.Count; i++)
                        {
                            string pathSrcLab = dTableSrc.Rows[i].ItemArray[0].ToString();
                            string marker = dTableSrc.Rows[i].ItemArray[1].ToString();
                            worksheet.Cells["A" + (i + 2)].Value = marker;
                            worksheet.Cells["B" + (i + 2)].Value = pathSrcLab;
                        }
                        worksheet.Cells.AutoFitColumns(1);
                        package.SaveAs(new FileInfo("Not found marker in bin.xlsx"));
                    }
                }
            }
            catch (System.Runtime.InteropServices.COMException ex)
            {
                MessageBox.Show("Excel bloked.\n" + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Excel don't install");
            }
        }
    }
}
