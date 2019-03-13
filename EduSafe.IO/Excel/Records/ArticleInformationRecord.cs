using System;

namespace EduSafe.IO.Excel.Records
{
    public class ArticleInformationRecord
    {
        public int Order { get; set; }
        public DateTime ArticleDate { get; set; }
        public string ArticleUrl { get; set; }
        public string ArticleTitle { get; set; }
        public string ArticleDescription { get; set; }
    }
}
