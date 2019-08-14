using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using EduSafe.Common;
using EduSafe.Core.BusinessLogic.Containers;
using EduSafe.IO.Excel.Records;

namespace EduSafe.Core.Repositories.Excel
{
    public class CollegeDataRepository : ExcelDataRepository
    {
        private const string _collegesListDataTab = "List Of US Colleges";
        private const string _collegeMajorDataTab = "NCES Data By Major";
        private const string _institutionalGradRates = "Institutional Graduation Rates";

        public readonly HashSet<string> CollegesList;
        public readonly ConcurrentDictionary<string, CollegeMajorData> CollegeMajorDataDictionary;
        public readonly ConcurrentDictionary<string, Dictionary<string, InstitutionalGradData>> InstitutionalDataDictionary;

        public CollegeDataRepository(Stream inputFileStream) : base(inputFileStream)
        {
            var institutionalDataDictionary = CreateInstitutionalDataDictionary();
            InstitutionalDataDictionary = new ConcurrentDictionary<string, Dictionary<string, InstitutionalGradData>>(institutionalDataDictionary);

            var collegeMajorDataDictionary = CreateCollegeMajorDataDictionary();
            CollegeMajorDataDictionary = new ConcurrentDictionary<string, CollegeMajorData>(collegeMajorDataDictionary);
            CollegesList = CreateCollegesList();
        }

        private Dictionary<string, Dictionary<string, InstitutionalGradData>> CreateInstitutionalDataDictionary()
        {
            var institutionalDataDictionary = new Dictionary<string, Dictionary<string, InstitutionalGradData>>();
            var institutionalDataRecords = _ExcelFileReader.GetDataFromSpecificTab<InstitutionalDataRecord>(_institutionalGradRates);

            foreach (var record in institutionalDataRecords)
            {
                var institutionalGradData = new InstitutionalGradData(
                    record.LoanInterestRate,
                    record.AverageLoanDebtAtGraduation,
                    record.GradTargetYear1 * Constants.PercentagePoints,
                    record.GradTargetYear2 * Constants.PercentagePoints,
                    record.GradTargetYear3 * Constants.PercentagePoints,
                    record.UnemploymentTarget * Constants.PercentagePoints,
                    record.CohortDefaultRate * Constants.PercentagePoints);

                if (!institutionalDataDictionary.ContainsKey(record.DegreeType))
                    institutionalDataDictionary.Add(record.DegreeType, new Dictionary<string, InstitutionalGradData>());

                if (!institutionalDataDictionary[record.DegreeType].ContainsKey(record.CollegeType))
                    institutionalDataDictionary[record.DegreeType].Add(record.CollegeType, institutionalGradData);
            }

            return institutionalDataDictionary;
        }

        private Dictionary<string, CollegeMajorData> CreateCollegeMajorDataDictionary()
        {
            var collegeMajorDataRecords = _ExcelFileReader.GetDataFromSpecificTab<CollegeMajorDataRecord>(_collegeMajorDataTab);

            var collegeMajorDataDictionary = collegeMajorDataRecords.ToDictionary
                (r => r.CollegeMajor, r =>
                    {
                        return new CollegeMajorData(
                            r.CollegeMajor,
                            r.UnemploymentRate,
                            r.UnemploymentRateError,
                            r.MedianSalary);
                    }
                );

            return collegeMajorDataDictionary;
        }

        private HashSet<string> CreateCollegesList()
        {
            var dataHasOneRowOfHeaders = true;
            var collegeRecords = _ExcelFileReader.GetExcelDataRowsFromWorksheet(_collegesListDataTab, dataHasOneRowOfHeaders);

            var collegesList = new HashSet<string>();
            collegeRecords.ForEach(r =>
            {
                if (r.FirstCellUsed().TryGetValue(out string collegeName))
                    collegesList.Add(collegeName);
            });

            return collegesList;
        }
    }
}
