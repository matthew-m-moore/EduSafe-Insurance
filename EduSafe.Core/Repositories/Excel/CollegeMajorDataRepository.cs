using System.Collections.Generic;
using System.IO;
using System.Linq;
using EduSafe.Core.BusinessLogic.Containers;
using EduSafe.IO.Excel.Records;

namespace EduSafe.Core.Repositories.Excel
{
    public class CollegeMajorDataRepository : ExcelDataRepository
    {
        private const string _dataTab = "NCES Data By Major";

        public readonly Dictionary<string, CollegeMajorData> CollegeMajorDataDictionary;

        public CollegeMajorDataRepository(Stream inputFileStream) : base(inputFileStream)
        {
            var collegeMajorDataRecords = _ExcelFileReader.GetDataFromSpecificTab<CollegeMajorDataRecord>(_dataTab);

            CollegeMajorDataDictionary = collegeMajorDataRecords.ToDictionary
                (r => r.CollegeMajor, r => 
                    {
                        return new CollegeMajorData(
                            r.CollegeMajor, 
                            r.UnemploymentRate, 
                            r.UnemploymentRateError, 
                            r.MedianSalary);
                    }
                );
        }
    }
}
