using System.Collections.Generic;

namespace EduSafe.WebApi.Models.Institutions
{
    public class InstitutionOutputSummary
    {
        public string OutputTitle { get; set; }
        public List<InstitutionOutputEntry> InstitutionOutputEntries { get; set; }
    }
}