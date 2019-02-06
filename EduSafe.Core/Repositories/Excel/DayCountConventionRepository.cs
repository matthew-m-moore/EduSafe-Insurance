using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using EduSafe.Common.Enums;
using EduSafe.Core.Repositories.Excel.Converters;
using EduSafe.IO.Excel.Records;

namespace EduSafe.Core.Repositories.Excel
{
    public class DayCountConventionRepository : ExcelDataRepository
    {
        private const string _dataTab = "DayCountConventions";
        private const string _dataFile = "EduSafe.Core.Resources.Interest-Rate-Details-Data.xlsx";

        private static Stream _inputFileStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(_dataFile);

        public readonly Dictionary<DayCountConvention, DayCountConventionRecord> DayCountConventionDetails;
        public readonly Dictionary<string, DayCountConvention> DayCountConventionDescriptions;

        public DayCountConventionRepository() : base(_inputFileStream)
        {
            var dayCountConventionRecords = _ExcelFileReader.GetDataFromSpecificTab<DayCountConventionRecord>(_dataTab);

            DayCountConventionDetails = dayCountConventionRecords.ToDictionary
                (r => DayCountConventionConverter.ConvertStringToDayCountConvention(r.EnumName), r => r);

            DayCountConventionDescriptions = DayCountConventionDetails.ToDictionary(d => d.Value.Description, d => d.Key);
        }
    }
}
