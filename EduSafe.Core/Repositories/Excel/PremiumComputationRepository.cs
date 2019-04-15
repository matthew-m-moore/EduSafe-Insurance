using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using EduSafe.Core.BusinessLogic.Models;
using EduSafe.Core.BusinessLogic.Scenarios;
using EduSafe.Core.Repositories.Excel;
using EduSafe.Core.Repositories.Excel.Converters;
using EduSafe.IO.Excel.Records;

namespace EduSafe.Core.Repositories
{
    public class PremiumComputationRepository : ExcelDataRepository
    {
        private const string _scenariosTab = "Scenarios";

        private EnrollmentModelConverter _enrollmentModelConverter;
        private PremiumCalculationConverter _premiumCalculationConverter;
        private ServicingCostsModelRepository _servicingCostsModelRepository;

        private List<EnrollmentModelScenarioRecord> _enrollmentModelScenarios;

        public PremiumComputationRepository(string pathToExcelDataFile)
            : base (pathToExcelDataFile)
        {
            var vectorRepository = new VectorRepository(pathToExcelDataFile);
            var interestRateCurveRepository = new InterestRateCurveRepository(pathToExcelDataFile);
            _servicingCostsModelRepository = new ServicingCostsModelRepository(pathToExcelDataFile);

            Initialize(vectorRepository, interestRateCurveRepository);
        }

        public PremiumComputationRepository(Stream fileStream)
            : base(fileStream)
        {
            var vectorRepository = new VectorRepository(fileStream);
            var interestRateCurveRepository = new InterestRateCurveRepository(fileStream);          
            _servicingCostsModelRepository = new ServicingCostsModelRepository(fileStream);

            Initialize(vectorRepository, interestRateCurveRepository);
        }

        public PremiumComputationEngine GetPremiumComputationScenarioById(int scenarioId, bool useNumericalComputation = false)
        {
            var enrollmentModelScenario = _enrollmentModelScenarios.SingleOrDefault(e => e.Id == scenarioId);
            if (enrollmentModelScenario == null)
                throw new Exception(string.Format("ERROR: No scenario found for Id #: {0}.", scenarioId));

            var premiumComputationEngine = LoadPremiumComputationScenario(enrollmentModelScenario, useNumericalComputation);
            return premiumComputationEngine;
        }

        public Dictionary<int, PremiumComputationEngine> GetPremiumComputationScenarios(bool useNumericalComputation = false)
        {
            var scenariosDictionary = new Dictionary<int, PremiumComputationEngine>();
            foreach (var enrollmentModelScenario in _enrollmentModelScenarios)
            {
                var premiumComputationEngine = LoadPremiumComputationScenario(enrollmentModelScenario, useNumericalComputation);

                if (scenariosDictionary.ContainsKey(enrollmentModelScenario.Id))
                    throw new Exception("ERROR: Duplicate scenario Id, please check inputs. Scenario Id must be unique");

                scenariosDictionary.Add(enrollmentModelScenario.Id, premiumComputationEngine);
            }

            return scenariosDictionary;
        }

        public Dictionary<string, PremiumComputationEngine> GetPremiumComputationScenariosByName(bool useNumericalComputation = false)
        {
            var scenariosDictionary = new Dictionary<string, PremiumComputationEngine>();
            foreach (var enrollmentModelScenario in _enrollmentModelScenarios)
            {
                var premiumComputationEngine = LoadPremiumComputationScenario(enrollmentModelScenario, useNumericalComputation);

                if (scenariosDictionary.ContainsKey(enrollmentModelScenario.Scenario))
                    throw new Exception("ERROR: Duplicate scenario name, please check inputs. Scenario name must be unique");

                scenariosDictionary.Add(enrollmentModelScenario.Scenario, premiumComputationEngine);
            }

            return scenariosDictionary;
        }

        public PremiumComputationEngine GetPremiumComputationScenario(EnrollmentModelScenarioRecord enrollmentModelScenarioRecord,
            bool useNumericalComputation = false)
        {
            var premiumComputationEngine = LoadPremiumComputationScenario(enrollmentModelScenarioRecord, useNumericalComputation);
            return premiumComputationEngine;
        }

        public Dictionary<string, EnrollmentModelScenarioRecord> GetEnrollmentModelScenariosByName()
        {
            var scenariosDictionaryByName = _enrollmentModelScenarios.ToDictionary(s => s.Scenario, s => s);
            return scenariosDictionaryByName;
        }

        private PremiumComputationEngine LoadPremiumComputationScenario(EnrollmentModelScenarioRecord enrollmentModelScenario, bool useNumericalComputation)
        {
            Console.WriteLine(string.Format("Loading scenario '{0}'...", enrollmentModelScenario.Scenario));

            var enrollmentModel = _enrollmentModelConverter
                .ConvertEnrollmentModelScenarioToEnrollmentModelInput(enrollmentModelScenario);

            var servicingCostsModel = _servicingCostsModelRepository.GetServicingCostsModel(enrollmentModelScenario);

            var premiumCalculation = useNumericalComputation 
                ? _premiumCalculationConverter.GetNumericalPremiumCalculation(enrollmentModelScenario)
                : _premiumCalculationConverter.GetAnalyticalPremiumCalculation(enrollmentModelScenario);

            var premium = enrollmentModelScenario.Premium;
            var startingPeriod = enrollmentModelScenario.StartPeriod - 1;
            var repricingModel = new RollForwardRepricingModel(enrollmentModel, servicingCostsModel);
            var premiumComputationEngine = new PremiumComputationEngine(premiumCalculation, repricingModel, startingPeriod, premium);

            premiumComputationEngine.ScenarioId = enrollmentModelScenario.Id;
            premiumComputationEngine.ScenarioName = enrollmentModelScenario.Scenario;

            return premiumComputationEngine;
        }

        private void Initialize(VectorRepository vectorRepository, InterestRateCurveRepository interestRateCurveRepository)
        {
            _enrollmentModelConverter =
                new EnrollmentModelConverter(vectorRepository.GetVectorsDictionary());
            _premiumCalculationConverter =
                new PremiumCalculationConverter(interestRateCurveRepository.GetInterestRateCurveSetDictionary());
            _enrollmentModelScenarios = _ExcelFileReader
                .GetDataFromSpecificTab<EnrollmentModelScenarioRecord>(_scenariosTab);
        }
    }
}
