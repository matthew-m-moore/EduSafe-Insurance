using System;

namespace EduSafe.IO.Database.Entities.Servicing.Claims
{
    public class ClaimDocumentEntryEntity
    {
        public int Id { get; set; }
        public long ClaimNumber { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }
        public int FileVerificationStatusTypeId { get; set; }
        public bool IsVerified { get; set; }
        public DateTime UploadDate { get; set; }
        public DateTime? ExpirationDate { get; set; }

        public string FileVerificationStatusType { get; set; }
    }
}
