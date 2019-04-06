using System.Collections.Generic;
using EduSafe.Core.BusinessLogic.Containers.TimeSeries;
using EduSafe.Core.BusinessLogic.Scenarios;
using EduSafe.Core.Interfaces;

namespace EduSafe.Core.BusinessLogic.Containers
{
    public class PremiumComputationForecastingInput
    {
        private Dictionary<string, PremiumComputationEngine> _forecastingScenariosDictionary;

        public List<ForecastedEnrollmentTimeSeriesEntry> ForecastedEnrollmentTimeSeriesEntries { get; }
        public Dictionary<int, Dictionary<string, IScenario>> ForecastedTimeSeriesOverlayScenarios { get; }
        public Dictionary<string, double> PercentageFirstTimeEnrolleeProjections { get; }

        public HashSet<string> ScenarioNames { get; private set; }

        public PremiumComputationForecastingInput(
            Dictionary<string, PremiumComputationEngine> forecastingScenariosDictionary,
            List<ForecastedEnrollmentTimeSeriesEntry> forecastedEnrollmentTimeSeriesEntries,
            Dictionary<int, Dictionary<string, IScenario>> forecastedTimeSeriesOverlayScenarios = null,
            Dictionary<string, double> percentageFirstTimeEnrolleeProjections = null)
        {
            _forecastingScenariosDictionary = forecastingScenariosDictionary;

            ForecastedEnrollmentTimeSeriesEntries = forecastedEnrollmentTimeSeriesEntries;
            ForecastedTimeSeriesOverlayScenarios = forecastedTimeSeriesOverlayScenarios;
            PercentageFirstTimeEnrolleeProjections = percentageFirstTimeEnrolleeProjections;

            ScenarioNames = new HashSet<string>(_forecastingScenariosDictionary.Keys);
        }

        public PremiumComputationEngine GetForecastingScenario(string scenarioName)
        {
            return _forecastingScenariosDictionary.TryGetValue(scenarioName, out PremiumComputationEngine premiumComputationEngine)
                ? premiumComputationEngine
                : null;
        }
    }
}
