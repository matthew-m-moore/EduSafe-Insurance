using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ClosedXML.Excel;
using EduSafe.IO.Excel;
using EduSafe.WebApi.Models;

namespace EduSafe.WebApi.Adapters
{
    internal class ExcelExportAdapter
    {
        public XLWorkbook CreateStudentInformationReport(InstitutionProfileEntry institutionProfileEntry, out string filePath)
        {
            //var excelFileWriter = new ExcelFileWriter();
            //SecuritizationTranchesSummaryExcelReport.AddReportTab(excelFileWriter.ExcelWorkbook, securitizationResultsDictionary);
            return new XLWorkbook();
        }
    }
}