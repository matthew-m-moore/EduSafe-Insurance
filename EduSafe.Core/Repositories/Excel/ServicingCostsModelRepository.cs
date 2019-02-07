using System.IO;
using System.Linq;
using EduSafe.Core.BusinessLogic.Models;
using EduSafe.Core.Repositories.Excel.Converters;
using EduSafe.IO.Excel.Records;

namespace EduSafe.Core.Repositories.Excel
{
    public class ServicingCostsModelRepository : ExcelDataRepository
    {
        private const string _costsTab = "Costs";

        public ServicingCostsModelRepository(string pathToExcelDataFile)
            : base(pathToExcelDataFile)
        { }

        public ServicingCostsModelRepository(Stream fileStream)
            : base(fileStream)
        { }

        public ServicingCostsModel GetServicingCostsModel(int numberOfMonthlyPeriodsToProject)
        {
            var costOrFeeRecords = _ExcelFileReader.GetDataFromSpecificTab<CostOrFeeRecord>(_costsTab);
            var listOfCostsOrFees = costOrFeeRecords
                .Select(CostOrFeeConverter.ConvertCostOrFeeRecordToCostOrFee).ToList();

            var servicingCostsModel = new ServicingCostsModel(listOfCostsOrFees, numberOfMonthlyPeriodsToProject);
            return servicingCostsModel;
        }
    }
}
