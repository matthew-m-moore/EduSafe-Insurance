using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClosedXML.Excel;
using EduSafe.ConsoleApp.Interfaces;
using EduSafe.IO.Excel;
using EduSafe.IO.Files;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.File;

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
            var newUniqueId = Guid.NewGuid();

            var filesConnectionString = FileServerSettings.AssembleConnectionString();
            var fileStorageAccount = CloudStorageAccount.Parse(filesConnectionString);

            // Create a CloudFileClient object for credentialed access to Azure Files.
            var fileClient = fileStorageAccount.CreateCloudFileClient();

            // Get a reference to the file share we created previously.
            var list = fileClient.ListShares();
            var share = fileClient.GetShareReference(FileServerSettings.FileShareName);

            // Ensure that the share exists.
            if (share.Exists())
            {
                // Get a reference to the root directory for the share.
                var rootDir = share.GetRootDirectoryReference();
                
                // Get a reference to the directory we created previously.
                var sampleDir = rootDir.GetDirectoryReference(FileServerSettings.InstitutionalCustomersDirectory);
                sampleDir.CreateIfNotExists();

                // Ensure that the directory exists.
                if (sampleDir.Exists())
                {                    
                    var excelFileWriter = new ExcelFileWriter();
                    var emptyTable = new DataTable();
                    excelFileWriter.AddWorksheetForDataTable(emptyTable);
                    
                    var file = sampleDir.GetFileReference("test_cloud.xlsx");
                    var memoryStream = excelFileWriter.ExportWorkbookToMemoryStream();
                    file.UploadFromStream(memoryStream);
                }
            }
        }
    }
}
