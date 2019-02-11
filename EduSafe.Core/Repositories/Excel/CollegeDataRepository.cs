using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using EduSafe.Core.BusinessLogic.Containers;
using EduSafe.IO.Excel.Records;

namespace EduSafe.Core.Repositories.Excel
{
    public class CollegeDataRepository : ExcelDataRepository
    {
        private const string _collegesListDataTab = "List Of US Colleges";
        private const string _collegeMajorDataTab = "NCES Data By Major";

        public readonly HashSet<string> CollegesList;
        public readonly ConcurrentDictionary<string, CollegeMajorData> CollegeMajorDataDictionary;

        public CollegeDataRepository(Stream inputFileStream) : base(inputFileStream)
        {
            var collegeMajorDataDictionary = CreateCollegeMajorDataDictionary();
            CollegeMajorDataDictionary = new ConcurrentDictionary<string, CollegeMajorData>(collegeMajorDataDictionary);
            CollegesList = CreateCollegesList();
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
