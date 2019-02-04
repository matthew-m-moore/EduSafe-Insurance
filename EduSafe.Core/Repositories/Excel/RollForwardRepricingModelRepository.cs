using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EduSafe.Core.BusinessLogic.Models;
using EduSafe.Core.Repositories.Excel;
using EduSafe.IO.Excel;
using EduSafe.IO.Excel.Records;

namespace EduSafe.Core.Repositories
{
    public class RollForwardRepricingModelRepository : ExcelDataRepository
    {
        private const string _scenariosTab = "Scenarios";

        private VectorRepository _vectorRepository;
        private InterestRateCurveRepository _interestRateCurveRepository;
        private ServicingCostsModelRepository _servicingCostsModelRepository;

        public RollForwardRepricingModelRepository(string pathToExcelDataFile)
            : base (pathToExcelDataFile)
        {
            _vectorRepository = new VectorRepository(pathToExcelDataFile);
            _interestRateCurveRepository = new InterestRateCurveRepository(pathToExcelDataFile);
            _servicingCostsModelRepository = new ServicingCostsModelRepository(pathToExcelDataFile);
        }

        public Dictionary<int, RollForwardRepricingModel> GetRollForwardPricingModelScenarios()
        {
            var enrollmentModelScenarios = _ExcelFileReader
                .GetDataFromSpecificTab<EnrollmentModelScenarioRecord>(_scenariosTab);

            foreach (var enrollmentModelScenario in enrollmentModelScenarios)
            {

            }

            throw new NotImplementedException();
        }

        private RollForwardRepricingModel GetRollForwardPricingModel()
        {
            

            throw new NotImplementedException();
        }
    }
}
