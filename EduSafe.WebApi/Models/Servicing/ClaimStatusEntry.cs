using System.Collections.Generic;

namespace EduSafe.WebApi.Models.Servicing
{
    public class ClaimStatusEntry
    {
        public long ClaimNumber { get; set; }
        public string ClaimType { get; set; }
        public string ClaimStatus { get; set; }        
        public bool IsClaimApproved { get; set; }
        public List<ClaimDocumentEntry> ClaimDocumentEntries { get; set; }
    }
}