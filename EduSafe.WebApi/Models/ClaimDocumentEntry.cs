using System;

namespace EduSafe.WebApi.Models
{
    public class ClaimDocumentEntry
    {
        public string FileName { get; set; }
        public string FileType { get; set; }

        public string FileVerificationStatus { get; set; }
        public bool IsFileVerified { get; set; }

        public DateTime UploadDate { get; set; }
        public DateTime? ExpirationDate { get; set; }
    }
}