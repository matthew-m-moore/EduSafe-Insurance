using System.Collections.Generic;

namespace EduSafe.WebApi.Models
{
    public class InstitutionOutputSummary
    {
        public string OutputTitle { get; set; }
        public double StudentsMonthlyDebtPayment { get; set; }
        public List<InstitutionOutputEntry> InstitutionOutputEntries { get; set; }
    }
}