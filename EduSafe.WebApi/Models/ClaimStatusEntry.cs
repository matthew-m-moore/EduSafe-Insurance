using System.Collections.Generic;

namespace EduSafe.WebApi.Models
{
    public class ClaimStatusEntry
    {
        public string ClaimType { get; set; }
        public string ClaimStatus { get; set; }
        
        public bool IsClaimApproved { get; set; }

        public List<ClaimDocumentEntry> ClaimDocumentEntries { get; set; }
        public List<ClaimPaymentEntry> ClaimPaymentEntries { get; set; }
    }
}