using System;

namespace EduSafe.WebApi.Models
{
    public class ArticleInformationEntry
    {
        public DateTime Date { get; set; }
        public int Order { get; set;  }

        public string Title { get; set; }
        public string UrlAddress { get; set; }
        public string Description { get; set; }
    }
}