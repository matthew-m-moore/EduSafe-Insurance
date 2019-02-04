using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EduSafe.Common.Curves;

namespace EduSafe.Core.Repositories.Excel
{
    public class InterestRateCurveRepository : ExcelDataRepository
    {
        public InterestRateCurveRepository(string pathToExcelDataFile)
            : base(pathToExcelDataFile)
        { }

        public Dictionary<string, InterestRateCurve> GetInterestRateCurveDictionary()
        {
            throw new NotImplementedException();
        }
    }
}
