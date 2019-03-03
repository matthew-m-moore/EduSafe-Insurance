using System.Collections.Generic;
using System.IO;
using System.Reflection;
using EduSafe.Core.Repositories.Excel;
using EduSafe.IO.Excel.Records;
using EduSafe.WebApi.Models;

namespace EduSafe.WebApi.Adapters
{
    internal static class ArticlesLoadingAdapter
    {
        private const string _websitePostedArticlesFile = "EduSafe.WebApi.App_Data.EduSafe-Website-Posted-Articles.xlsx";
        private static Stream _websitePostedArticlesFileStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(_websitePostedArticlesFile);

        internal static ArticlesLoadingRepository ArticlesLoadingRepository;

        public static readonly List<ArticleInformationEntry> ArticleInformationEntries;

        static ArticlesLoadingAdapter()
        {
            ArticleInformationEntries = new List<ArticleInformationEntry>();
        }

        internal static void LoadArticleRecords()
        {
            ArticlesLoadingRepository = new ArticlesLoadingRepository(_websitePostedArticlesFileStream);

            var articlesList = ArticlesLoadingRepository.ArticleInformationRecords;
            foreach (var article in articlesList)
            {
                var articleInformationEntry = LoadArticleInformationEntry(article);
                ArticleInformationEntries.Add(articleInformationEntry);
            }
        }

        private static ArticleInformationEntry LoadArticleInformationEntry(ArticleInformationRecord articleInformationRecord)
        {
            var articleInformationEntry = new ArticleInformationEntry
            {
                Order = articleInformationRecord.Order,
                Date = articleInformationRecord.ArticleDate,
                UrlAddress = articleInformationRecord.ArticleUrl,
                Title = articleInformationRecord.ArticleTitle,
                Description = articleInformationRecord.ArticleDescription
            };

            return articleInformationEntry;
        }
    }
}