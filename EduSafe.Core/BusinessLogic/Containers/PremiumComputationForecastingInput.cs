using System.Collections.Generic;
using System.Linq;
using EduSafe.Core.BusinessLogic.Containers.TimeSeries;
using EduSafe.Core.BusinessLogic.Scenarios;
using EduSafe.Core.Interfaces;

namespace EduSafe.Core.BusinessLogic.Containers
{
    public class PremiumComputationForecastingInput
    {
        private Dictionary<string, PremiumComputationEngine> _forecastingScenariosDictionary;

        public string ForecastName { get; set; }
        public int MonthlyPeriodsToForecast { get; }

        public ForecastedEnrollmentsProjection ForecastedEnrollmentsProjection { get; }
        public Dictionary<int, Dictionary<string, IScenario>> ForecastedOverlayScenarios { get; }
        public Dictionary<string, double> PercentageFirstYearEnrolleeProjections { get; }

        public bool ApplyFirstYearPercentGlobally =>
            PercentageFirstYearEnrolleeProjections != null &&
            PercentageFirstYearEnrolleeProjections.Any();

        public HashSet<string> ScenarioNames { get; private set; }

        public PremiumComputationForecastingInput(
            int monthlyPeriodsToForecast,
            Dictionary<string, PremiumComputationEngine> forecastingScenariosDictionary,
            ForecastedEnrollmentsProjection forecastedEnrollmentsProjection,
            Dictionary<int, Dictionary<string, IScenario>> forecastedOverlayScenarios = null,
            Dictionary<string, double> percentageFirstTimeEnrolleeProjections = null)
        {
            _forecastingScenariosDictionary = forecastingScenariosDictionary;

            MonthlyPeriodsToForecast = monthlyPeriodsToForecast;
            ForecastedEnrollmentsProjection = forecastedEnrollmentsProjection;
            ForecastedOverlayScenarios = forecastedOverlayScenarios;
            PercentageFirstYearEnrolleeProjections = percentageFirstTimeEnrolleeProjections;

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
