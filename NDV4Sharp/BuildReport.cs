﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDV4Sharp
{
    class BuildReport
    {
        public System.Windows.Forms.Button BReport { get; set; }
        public BuildReport() { }

        public void buildReportExcel(int index)
        {
            WorkExcel workExcel = new WorkExcel();

            switch (index)
            {
                case 0:
                    workExcel.DisplayInExcelFoundMarker();
                    break;
                case 1:
                    workExcel.DisplayInExcelNotFoundMarker();
                    break;
                case 2:
                    workExcel.DisplayInExcelFoundInBinMarker();
                    break;
            }            
        }

    }
}
