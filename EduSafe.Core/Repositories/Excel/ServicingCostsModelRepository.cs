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

        private List<CostOrFee> _listOfCostsOrFees;

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
            var costOrFeeRecords = _ExcelFileReader.GetDataFromSpecificTab<CostOrFeeRecord>(_costsTab);
            _listOfCostsOrFees = costOrFeeRecords
                .Select(CostOrFeeConverter.ConvertCostOrFeeRecordToCostOrFee).ToList();
        }

        public ServicingCostsModel GetServicingCostsModel(int numberOfMonthlyPeriodsToProject)
        {
            var servicingCostsModel = new ServicingCostsModel(_listOfCostsOrFees, numberOfMonthlyPeriodsToProject);
            return servicingCostsModel;
        }
    }
}
