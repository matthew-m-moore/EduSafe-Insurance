using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using EduSafe.Common.Enums;
using EduSafe.Core.Repositories.Excel.Converters;
using EduSafe.IO.Excel.Records;

namespace EduSafe.Core.Repositories.Excel
{   
    public class InterestRateCurveTypeRepository : ExcelDataRepository
    {
        private const string _dataTab = "InterestRateTypeDetails";
        private const string _dataFile = "EduSafe.Core.Resources.Interest-Rate-Details-Data.xlsx";

        private static Stream _inputFileStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(_dataFile);

        public readonly Dictionary<InterestRateCurveType, InterestRateTypeDetailRecord> InterestRateCurveTypeDetails;
        public readonly Dictionary<string, InterestRateCurveType> InterestRateCurveTypeDescriptions;

        public InterestRateCurveTypeRepository() : base(_inputFileStream)
        {
            var interestRateTypeDetailRecords = _ExcelFileReader.GetDataFromSpecificTab<InterestRateTypeDetailRecord>(_dataTab);

            InterestRateCurveTypeDetails = interestRateTypeDetailRecords.ToDictionary
                (r => InterestRateCurveTypeConverter.ConvertStringToInterestRateCurveType(r.EnumName), r => r);

            InterestRateCurveTypeDescriptions = InterestRateCurveTypeDetails.ToDictionary(d => d.Value.Description, d => d.Key);
        }
    }
}
