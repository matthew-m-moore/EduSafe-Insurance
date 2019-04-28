using System;
using System.IO;
using System.Linq;
using EduSafe.Core.BusinessLogic.Scenarios;
using EduSafe.Core.Repositories.Excel.Converters;
using EduSafe.IO.Excel.Records;

namespace EduSafe.Core.Repositories.Excel
{
    public class ForecastingRepository : ExcelDataRepository
    {
        private const string _forecastingParametersTab = "ForecastingParameters";

        private readonly PremiumComputationRepository _premiumComputationRepository;
        private readonly ForecastedEnrollmentRepository _forecastedEnrollmentRepository;
        private readonly ForecastedFirstYearPercentageRepository _forecastedFirstYearPercentageRepository;
        private readonly ShockScenariosRepository _shockScenariosRepository;

        public ForecastingParametersRecord ForecastingParametersRecord { get; private set; }

        public ForecastingRepository(string pathToExcelDataFile) 
            : base(pathToExcelDataFile)
        {
            _premiumComputationRepository = new PremiumComputationRepository(pathToExcelDataFile);
            _forecastedEnrollmentRepository = new ForecastedEnrollmentRepository(pathToExcelDataFile);
            _forecastedFirstYearPercentageRepository = new ForecastedFirstYearPercentageRepository(pathToExcelDataFile);

            Initialize();

            // Note that there is order-dependency here, "Initialize()" must come before creating the scenarios repository
            var selectedShockScenario = ForecastingParametersRecord.ShockParameterSet;
            _shockScenariosRepository = new ShockScenariosRepository(pathToExcelDataFile, selectedShockScenario);
        }

        public ForecastingRepository(Stream fileStream) 
            : base(fileStream)
        {
            _premiumComputationRepository = new PremiumComputationRepository(fileStream);
            _forecastedEnrollmentRepository = new ForecastedEnrollmentRepository(fileStream);
            _forecastedFirstYearPercentageRepository = new ForecastedFirstYearPercentageRepository(fileStream);

            Initialize();

            // Note that there is order-dependency here, "Initialize()" must come before creating the scenarios repository
            var selectedShockScenario = ForecastingParametersRecord.ShockParameterSet;
            _shockScenariosRepository = new ShockScenariosRepository(fileStream, selectedShockScenario);
        }

        /// <summary>
        /// Retrieves an engine for performing a premium computation forecast
        /// </summary>
        public PremiumComputationForecastingEngine PrepareForecastingEngine()
        {
            var premiumComputationForecastingInputConverter = new PremiumComputationForecastingInputConverter(
                _premiumComputationRepository,
                _forecastedEnrollmentRepository,
                _forecastedFirstYearPercentageRepository,
                _shockScenariosRepository);

            var premiumComputationForecastingInput = premiumComputationForecastingInputConverter.Convert(ForecastingParametersRecord);

            return new PremiumComputationForecastingEngine(premiumComputationForecastingInput);
        }

        private void Initialize()
        {
            ForecastingParametersRecord = _ExcelFileReader
                .GetTransposedDataFromSpecificTab<ForecastingParametersRecord>(_forecastingParametersTab).FirstOrDefault();

            if (ForecastingParametersRecord != null) return;

            throw new Exception(string.Format("ERROR: No forecasting parameters recoreds were detected on the tab named '{0}'", 
                _forecastingParametersTab));
        }
    }
}
