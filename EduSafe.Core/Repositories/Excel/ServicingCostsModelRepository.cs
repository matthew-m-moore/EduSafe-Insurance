using System.Collections.Generic;
using System.IO;
using System.Linq;
using EduSafe.Core.BusinessLogic.CostsOrFees;
using EduSafe.Core.BusinessLogic.Models;
using EduSafe.Core.Repositories.Excel.Converters;
using EduSafe.IO.Excel.Records;

namespace EduSafe.Core.Repositories.Excel
{
    public class ServicingCostsModelRepository : ExcelDataRepository
    {
        private const string _costsTab = "Costs";

        private Dictionary<string, List<CostOrFee>> _dictionaryOfCostsOrFees;

        public ServicingCostsModelRepository(string pathToExcelDataFile)
            : base(pathToExcelDataFile)
        {
            Initialize();
        }

        public ServicingCostsModelRepository(Stream fileStream)
            : base(fileStream)
        {
            Initialize();
        }

        private void Initialize()
        {
            _dictionaryOfCostsOrFees = new Dictionary<string, List<CostOrFee>>();

            var costOrFeeRecords = _ExcelFileReader.GetDataFromSpecificTab<CostOrFeeRecord>(_costsTab);

            foreach (var costOrFeeRecord in costOrFeeRecords)
            {
                var servicingCostsModelName = costOrFeeRecord.CostModelName ?? string.Empty;
                var costOrFee = CostOrFeeConverter.ConvertToCostOrFee(costOrFeeRecord);

                if (!_dictionaryOfCostsOrFees.ContainsKey(servicingCostsModelName))
                    _dictionaryOfCostsOrFees.Add(servicingCostsModelName, new List<CostOrFee>());

                _dictionaryOfCostsOrFees[servicingCostsModelName].Add(costOrFee);
            }
        }

        public ServicingCostsModel GetServicingCostsModel(EnrollmentModelScenarioRecord enrollmentModelScenario)
        {
            var servicingCostsModelName = enrollmentModelScenario.CostModelName ?? string.Empty;
            var numberOfMonthlyPeriodsToProject = enrollmentModelScenario.TotalMonths;

            var listOfCostsOrFees = _dictionaryOfCostsOrFees[servicingCostsModelName];
            var servicingCostsModel = new ServicingCostsModel(listOfCostsOrFees, numberOfMonthlyPeriodsToProject);
            return servicingCostsModel;
        }
    }
}
