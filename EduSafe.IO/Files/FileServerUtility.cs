using System;
using System.IO;
using System.Linq;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.File;

namespace EduSafe.IO.Files
{
    public class FileServerUtility
    {
        private static string _fileShareConnectionString => FileServerSettings.AssembleConnectionString();

        CloudFileDirectory _fileShareRootDirectory;

        public FileServerUtility(string fileShareName)
        {
            var fileStorageAccount = CloudStorageAccount.Parse(_fileShareConnectionString);

            // Create a CloudFileClient object for credentialed access to Azure Files.
            var fileClient = fileStorageAccount.CreateCloudFileClient();
            var fileShare = fileClient.GetShareReference(fileShareName);

            if (!fileShare.Exists())
                throw new Exception(string.Format("ERROR: Specified file share named '{0}' does not exist.", fileShareName));
            
            _fileShareRootDirectory = fileShare.GetRootDirectoryReference();

            if (!_fileShareRootDirectory.Exists())
                throw new Exception(string.Format("ERROR: File share root directory does not exist.", fileShareName));
        }

        public MemoryStream DownloadFileToMemoryStream(string fileDirectoryPath, string fileName)
        {
            var targetDirectory = _fileShareRootDirectory.GetDirectoryReference(fileDirectoryPath);

            if (!targetDirectory.Exists())
                throw new Exception(string.Format("ERROR: Target directory for file download '{0}' does not exist.", fileDirectoryPath));

            var fileToDownload = targetDirectory.GetFileReference(fileName);

            if (!fileToDownload.Exists())
                throw new Exception(string.Format("ERROR: Target file for download '{0}' does not exist.", fileName));

            var memoryStream = new MemoryStream();
            fileToDownload.DownloadToStream(memoryStream);

            return memoryStream;
        }

        public bool UploadFileFromStream(string fileDirectoryPath, string fileName, Stream stream)
        {     
            var nestedDirectoriesList = fileDirectoryPath.Split(Path.DirectorySeparatorChar).ToList();
            var assembledDirectoryPath = string.Empty;

            // Note that Azure's C# SDK won't create a nested directory on it's own without this loop
            foreach (var nestedDirectory in nestedDirectoriesList)
            {
                assembledDirectoryPath = !string.IsNullOrWhiteSpace(assembledDirectoryPath)
                    ? Path.Combine(assembledDirectoryPath, nestedDirectory)
                    : nestedDirectory;

                CreateDirectoryIfNotExists(assembledDirectoryPath);
            }

            var targetDirectory = _fileShareRootDirectory.GetDirectoryReference(fileDirectoryPath);

            if (!targetDirectory.Exists())
                throw new Exception(string.Format("ERROR: Target directory for file upload '{0}' does not exist.", fileDirectoryPath));

            stream.Position = 0;
            var fileToUpload = targetDirectory.GetFileReference(fileName);
            fileToUpload.UploadFromStream(stream);

            return fileToUpload.Exists();
        }

        private void CreateDirectoryIfNotExists(string directoryPathName)
        {
            var fileShareDirectory = _fileShareRootDirectory.GetDirectoryReference(directoryPathName);
            fileShareDirectory.CreateIfNotExists();
        }
    }
}
