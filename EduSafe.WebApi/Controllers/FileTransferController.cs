using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using EduSafe.Common;
using EduSafe.Common.ExtensionMethods;
using EduSafe.Core.Savers;
using EduSafe.IO.Files;

namespace EduSafe.WebApi.Controllers
{
    [RoutePrefix("api/file")]
    public class FileTransferController : ApiController
    {
        // Could this be the path to an IP address for our hosted file server perhaps?
        private const string _rootDirectoryPath = "~/App_Data";

        // Should we set a file size limit somewhere for uploads?
        // POST: api/file/upload/{customerIdentifier}/{fileType}
        public async Task<HttpResponseMessage> UploadFileToServer(string customerIdentifier, string claimType)
        {
            var isRequestValid = !Request.Content.IsMimeMultipartContent();
            if (!isRequestValid)
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            // Could I also pass a user ID into the post so as to retrieve the proper GUID path for that user?
            var rootDirectory = HttpContext.Current.Server.MapPath(_rootDirectoryPath);
            var formDataStreamProvider = new MultipartFormDataStreamProvider(rootDirectory);

            try
            {
                await Request.Content.ReadAsMultipartAsync(formDataStreamProvider);
                var fileData = formDataStreamProvider.FileData;

                foreach (var file in fileData)
                {
                    var fileName = file.Headers.ContentDisposition.FileName;
                    var localFileName = file.LocalFileName;

                    // Do I need to save the file here? Did this already save it?
                    // Don't I need logic to make / retrieve a GUID folder name for the customer?
                }

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        // Another example I found of a file upload method, which takes a slightly different tack
        // POST: api/file/upload/{customerIdentifier}/{claimType}/{claimNumber}
        [Route("upload/{customerIdentifier}/{claimType}/{claimNumber}")]
        [HttpPost]
        public IHttpActionResult UploadFiles(string customerIdentifier, string claimType, string claimNumber)
        {
            int cntSuccess = 0;
            var uploadedFileNames = new List<string>();
            var result = string.Empty;
            var claimNumberAsLong = long.Parse(claimNumber);

            var response = new HttpResponseMessage();
            var claimDocumentDatabaseSaver = new ClaimDocumentDatabaseSaver();

            var httpRequest = HttpContext.Current.Request;
            if (httpRequest.Files.Count > 0)
            {
                for (var i = 0; i < httpRequest.Files.Count; i++)
                {
                    var postedFile = httpRequest.Files[i];
                    // var filePath = HttpContext.Current.Server.MapPath("~/UploadedFiles/" + postedFile.FileName);
                    var fileName = postedFile.FileName;
                    var stream = postedFile.InputStream;
                    var claimFolderName = claimType + "-" + claimNumberAsLong.ToString();

                    var targetFolderPath = Path.Combine(
                        FileServerSettings.IndividualCustomersDirectory,
                        customerIdentifier,
                        claimFolderName);

                    // This saves the file to the customer's folder in the file share
                    var fileServerUtility = new FileServerUtility(FileServerSettings.FileShareName);
                    if (fileServerUtility.UploadFileFromStream(targetFolderPath, fileName, stream))
                    {
                        claimDocumentDatabaseSaver.SaveClaimDocumentEntry(claimNumberAsLong, claimType, fileName);
                        uploadedFileNames.Add(httpRequest.Files[i].FileName);
                        cntSuccess++;
                    }

                    // postedFile.SaveAs(filePath);
                }
            }

            result = cntSuccess.ToString() + " files uploaded succesfully.<br/>";

            result += "<ul>";

            foreach (var f in uploadedFileNames)
            {
                result += "<li>" + f + "</li>";
            }

            result += "</ul>";

            return Json(result);
        }

        // GET: api/file/download/{customerIdentifier}/{claimType}/{claimNumber}/{fileName}/{fileType}
        [Route("download/{customerIdentifier}/{claimType}/{claimNumber}/{fileName}")]
        [HttpGet]
        public HttpResponseMessage DownloadFileFromServer
            (string customerIdentifier, string claimType, long claimNumber, string fileName)
        {
            var claimFolderName = claimType + "-" + claimNumber.ToString();
            var targetFolderPath = Path.Combine(
                FileServerSettings.IndividualCustomersDirectory,
                customerIdentifier,
                claimFolderName);

            var fileExtension = fileName.GetFileNameExtension();
            var fileServerUtility = new FileServerUtility(FileServerSettings.FileShareName);

            using (var memoryStream = fileServerUtility.DownloadFileToMemoryStream(targetFolderPath, fileName))
            {
                var response = new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new ByteArrayContent(memoryStream.ToArray())
                };

                response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = fileName
                };

                // This allows you to access the file name, even with CORS in play
                response.Content.Headers.Add("FileName", fileName);
                response.Content.Headers.Add("Access-Control-Expose-Headers", "FileName");
                response.Content.Headers.ContentLength = memoryStream.Length;

                if (Constants.FileExtensionToContentTypeDictionary.TryGetValue(fileExtension, out var contentType))
                    response.Content.Headers.ContentType = new MediaTypeHeaderValue(contentType);
            
                return response;
            }
        }
    }
}