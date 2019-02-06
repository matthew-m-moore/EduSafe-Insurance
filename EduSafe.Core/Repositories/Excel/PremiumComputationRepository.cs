using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EduSafe.Core.BusinessLogic.Models;
using EduSafe.Core.BusinessLogic.Scenarios;
using EduSafe.Core.Repositories.Excel;
using EduSafe.Core.Repositories.Excel.Converters;
using EduSafe.IO.Excel;
using EduSafe.IO.Excel.Records;

namespace EduSafe.Core.Repositories
{
    public class PremiumComputationRepository : ExcelDataRepository
    {
        private const string _scenariosTab = "Scenarios";

        private VectorRepository _vectorRepository;
        private InterestRateCurveRepository _interestRateCurveRepository;
        private ServicingCostsModelRepository _servicingCostsModelRepository;

        public PremiumComputationRepository(string pathToExcelDataFile)
            : base (pathToExcelDataFile)
        {
            _vectorRepository = new VectorRepository(pathToExcelDataFile);
            _interestRateCurveRepository = new InterestRateCurveRepository(pathToExcelDataFile);
            _servicingCostsModelRepository = new ServicingCostsModelRepository(pathToExcelDataFile);
        }

        public Dictionary<int, PremiumComputationEngine> GetPremiumComputationScenarios()
        {
            var enrollmentModelScenarios = _ExcelFileReader
                .GetDataFromSpecificTab<EnrollmentModelScenarioRecord>(_scenariosTab);

            var scenariosDictionary = new Dictionary<int, PremiumComputationEngine>();

            foreach (var enrollmentModelScenario in enrollmentModelScenarios)
            {
                var enrollmentModelConverter = 
                    new EnrollmentModelConverter(_vectorRepository.GetVectorsDictionary());
                var premiumCalculationConverter = 
                    new PremiumCalculationConverter(_interestRateCurveRepository.GetInterestRateCurveSetDictionary());

                var enrollmentModel = enrollmentModelConverter
                    .ConvertEnrollmentModelScenarioToEnrollmentModelInput(enrollmentModelScenario);
                var servicingCostsModel = _servicingCostsModelRepository
                    .GetServicingCostsModel(enrollmentModelScenario.TotalMonths);
                var premiumCalculation = premiumCalculationConverter
                    .GetAnalyticalPremiumCalculation(enrollmentModelScenario);

                var startingPeriod = enrollmentModelScenario.StartPeriod - 1;
                var repricingModel = new RollForwardRepricingModel(enrollmentModel, servicingCostsModel);
                var premiumComputationEngine = new PremiumComputationEngine(premiumCalculation, repricingModel, startingPeriod);

                if (!scenariosDictionary.ContainsKey(enrollmentModelScenario.Id))
                    throw new Exception("ERROR: Duplicate scenario Id, please check inputs. Scenario Id must be unique");

                scenariosDictionary.Add(enrollmentModelScenario.Id, premiumComputationEngine);
            }

            return scenariosDictionary;
        }
    }
}
