using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using EduSafe.WebApi.Adapters;

namespace EduSafe.WebApi.Controllers
{
    [RoutePrefix("api/file")]
    public class FileTransferController : ApiController
    {
        // Could this be the path to an IP address for our hosted file server perhaps?
        private const string _rootDirectoryPath = "~/App_Data";

        // POST: api/file/upload/{customerIdentifier}/{fileType}
        [Route("upload/{customerIdentifier}/{fileType}")]
        [HttpPost]
        public async Task<HttpResponseMessage> UploadFileToServer(int customerIdentifier, string fileType)
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

        // GET: api/file/download/{customerIdentifier}/{fileName}
        [Route("download/{customerIdentifier}/{fileName}")]
        [HttpGet]
        public void DownloadFileFromServer(int customerIdentifier, string fileName)
        {
            var response = HttpContext.Current.Response;
            response.ClearContent();
            response.Clear();

            response.ContentType = "text/plain";
            response.AddHeader("Content-Disposition", "attachment; filename=" + fileName + ";");

            var filePath = Path.Combine(_rootDirectoryPath, fileName);
            response.TransmitFile(HttpContext.Current.Server.MapPath(filePath));

            response.Flush();
            response.End();
        }

        // GET: api/file/list
        [Route("list")]
        [HttpGet]
        // Probably don't need this to be an API, since it will be called internally on the server
        public List<string> GetFileListOnServer(int customerIdentifier)
        {
            // Somehow use the user ID to find the appropriate directory folder and return a list of file names
            return new List<string>();
        }
    }
}