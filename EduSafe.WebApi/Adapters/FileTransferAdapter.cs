using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EduSafe.WebApi.Models;

namespace EduSafe.WebApi.Adapters
{
    internal class FileTransferAdapter
    {
        internal void SaveFileToServer(int customerIdentifier, string fileType)
        {

        }

        internal List<ClaimDocumentEntry> RetrieveFileNameList(int customerIdentifier)
        {
            return new List<ClaimDocumentEntry>();
        }
    }
}