using System.Collections.Generic;

namespace EduSafe.WebApi.Models
{
    public class ModelOutputSummary
    {
        public string OutputTitle { get; set; }
        public ModelOutputHeaders ModelOutputHeaders { get; set; }
        public List<ModelOutputEntry> ModelOutputEntries { get; set; }
    }
}