namespace EduSafe.WebApi.Models.Individuals
{
    public class ResultsEmailEntry
    {
        public string RecipientAddress { get; set; }
        public string RecipientName { get; set; }
        public string ResultsPageHtml { get; set; }
        public ModelInputEntry ModelInputEntry { get; set; }
        public ModelOutputSummary ModelOutputSummary { get; set; }
    }
}