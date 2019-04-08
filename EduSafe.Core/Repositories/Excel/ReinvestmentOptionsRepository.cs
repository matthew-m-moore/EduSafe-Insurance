using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EduSafe.Core.BusinessLogic.Containers;
using EduSafe.Core.Repositories.Excel.Converters;
using EduSafe.IO.Excel;
using EduSafe.IO.Excel.Records;

namespace EduSafe.Core.Repositories.Excel
{
    public class ReinvestmentOptionsRepository : ExcelDataRepository
    {
        private const string _reinvestmentOptionsTab = "ReinvestmentOptions";
        private readonly Dictionary<int, ReinvestmentOptionsParameters> _reinvestmentOptionsParametersDictionary
            = new Dictionary<int, ReinvestmentOptionsParameters>();

        public ReinvestmentOptionsRepository(string pathToExcelFile) : base(pathToExcelFile)
        {
            var reinvestmentOptionsInputs = _ExcelFileReader
                .GetTransposedDataFromSpecificTab<ReinvestmentOptionsInputRecord>(_reinvestmentOptionsTab);

            _reinvestmentOptionsParametersDictionary = reinvestmentOptionsInputs
                .ToDictionary(i => i.Id, i => ReinvestmentOptionsParametersConverter.Convert(i));
        }

        public ReinvestmentOptionsParameters GetReinvestmentOptionsParametersFromId(int id)
        {
            return _reinvestmentOptionsParametersDictionary[id];
        }
    }
}
