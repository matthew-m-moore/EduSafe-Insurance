using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using EduSafe.ConsoleApp.Interfaces;
using EduSafe.IO.Excel;
using EduSafe.IO.Files;

namespace EduSafe.ConsoleApp.Scripts
{
    class WriteToFileShareTestScript : IScript
    {
        private static readonly string _institutionalDirectoryPath = FileServerSettings.InstitutionalCustomersDirectory;
        private static readonly string _individualDirectoryPath = FileServerSettings.IndividualCustomersDirectory;

        public List<string> GetArgumentsList()
        {
            return new List<string>();
        }

        public string GetFriendlyName()
        {
            return "Azure File Store Test";
        }

        public bool GetVisibilityStatus()
        {
            return false;
        }

        public void RunScript(string[] args)
        {
            var fileName = "test_report.xlsx";
            var newUniqueId = Guid.NewGuid().ToString();
            var reportsFolder = "Reports";
            var targetFilePath = Path.Combine(FileServerSettings.IndividualCustomersDirectory, newUniqueId, reportsFolder);

            var fileServerUtility = new FileServerUtility(FileServerSettings.FileShareName);

            var excelFileWriter = new ExcelFileWriter();
            var emptyTable = new DataTable();
            excelFileWriter.AddWorksheetForDataTable(emptyTable);

            var memoryStream = excelFileWriter.ExportWorkbookToMemoryStream();
            fileServerUtility.UploadFileFromStream(targetFilePath, fileName, memoryStream);
        }
    }
}
