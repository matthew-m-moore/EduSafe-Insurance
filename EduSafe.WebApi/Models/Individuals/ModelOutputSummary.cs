using System.Collections.Generic;

namespace EduSafe.WebApi.Models.Individuals
{
    public class ModelOutputSummary
    {
        public string OutputTitle { get; set; }
        public int DropOutCoveragePercentage { get; set; }
        public int GradSchoolCoveragePercentage { get; set; }
        public int EarlyHireCoveragePercentage { get; set; }
        public ModelOutputHeaders ModelOutputHeaders { get; set; }
        public List<ModelOutputEntry> ModelOutputEntries { get; set; }
    }
}