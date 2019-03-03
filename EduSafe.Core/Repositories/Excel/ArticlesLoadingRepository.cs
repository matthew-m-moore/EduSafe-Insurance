using System.Collections.Generic;
using System.IO;
using EduSafe.IO.Excel.Records;

namespace EduSafe.Core.Repositories.Excel
{
    public class ArticlesLoadingRepository : ExcelDataRepository
    {
        private const string _articlesListTab = "List";

        public readonly List<ArticleInformationRecord> ArticleInformationRecords;

        public ArticlesLoadingRepository(Stream inputFileStream) : base(inputFileStream)
        {
            ArticleInformationRecords = _ExcelFileReader.GetDataFromSpecificTab<ArticleInformationRecord>(_articlesListTab);
        }
    }
}
