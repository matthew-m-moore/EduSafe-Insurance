namespace EduSafe.WebApi.Models.Institutions
{
    public class InstitutionResultEmailEntry
    {
        public string RecipientAddress { get; set; }
        public string RecipientName { get; set; }
        public string ResultsPageHtml { get; set; }
        public InstitutionInputEntry InstitutionInputEntry { get; set; }
        public InstitutionOutputSummary InstitutionOutputSummary { get; set; }
    }
}