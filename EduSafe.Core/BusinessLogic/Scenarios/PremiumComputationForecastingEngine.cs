using System.Collections.Generic;
using System.Data;
using EduSafe.Common;
using EduSafe.Core.BusinessLogic.Aggregation;
using EduSafe.Core.BusinessLogic.Containers;
using EduSafe.Core.BusinessLogic.Containers.CashFlows;
using EduSafe.Core.BusinessLogic.Containers.TimeSeries;

namespace EduSafe.Core.BusinessLogic.Scenarios
{
    public class PremiumComputationForecastingEngine
    {
        private Dictionary<string, PremiumComputationResult> _forecastingScenariosBaseResultsDictionary;

        private List<List<PremiumCalculationCashFlow>> _forecastingCashFlowsListOfLists;
        private List<List<StudentEnrollmentStateTimeSeriesEntry>> _forecastingTimeSeriesListOfLists;
        private List<DataTable> _forecastingDataTableList;

        public PremiumComputationForecastingInput PremiumComputationForecastingInput { get; }

        public List<PremiumCalculationCashFlow> ForecastedPremiumCalculationCashFlows { get; private set; }
        public List<StudentEnrollmentStateTimeSeriesEntry> ForecastedEnrollmentTimeSeries { get; private set; }
        public DataTable ForecastedServicingCosts { get; private set; }

        public PremiumComputationForecastingEngine(PremiumComputationForecastingInput premiumComputationForecastingInput)
        {
            PremiumComputationForecastingInput = premiumComputationForecastingInput;

            _forecastingCashFlowsListOfLists = new List<List<PremiumCalculationCashFlow>>();
            _forecastingTimeSeriesListOfLists = new List<List<StudentEnrollmentStateTimeSeriesEntry>>();
            _forecastingDataTableList = new List<DataTable>();
        }

        public void RunForecast()
        {
            SetupBaseResultsDictionary();

            var forecastedOverlayScenarios = PremiumComputationForecastingInput.ForecastedTimeSeriesOverlayScenarios;
            var forecastedEnrollmentTimeSeries = PremiumComputationForecastingInput.ForecastedEnrollmentTimeSeriesEntries;

            foreach (var timeSeriesEntry in forecastedEnrollmentTimeSeries)
            {
                var monthlyPeriod = timeSeriesEntry.Period;

                foreach (var scenarioName in timeSeriesEntry.EnrollmentCountProjections.Keys)
                {
                    if (!_forecastingScenariosBaseResultsDictionary.ContainsKey(scenarioName)) continue;

                    var enrollmentCountProjection = timeSeriesEntry.EnrollmentCountProjections[scenarioName];
                    var copyOfBaseScenarioResult = _forecastingScenariosBaseResultsDictionary[scenarioName].Copy();

                    if (forecastedOverlayScenarios.ContainsKey(monthlyPeriod) &&
                        forecastedOverlayScenarios[monthlyPeriod].ContainsKey(scenarioName) &&
                        PremiumComputationForecastingInput.ScenarioNames.Contains(scenarioName))
                    {
                        var overlayScenarioLogic = forecastedOverlayScenarios[monthlyPeriod][scenarioName];
                        var copyOfPremiumComputationScenario = PremiumComputationForecastingInput.GetForecastingScenario(scenarioName).Copy();
                        var overlayComputationScenario = overlayScenarioLogic.ApplyScenarioLogic(copyOfPremiumComputationScenario);
                        var overlayScenarioResult = overlayComputationScenario.ComputePremiumResult();

                        // Then, I need to do something with the overlay scenario result to affect the base scenario result...
                        // I'll worry about that later for now
                    }
                    
                    copyOfBaseScenarioResult.ScaleResults(enrollmentCountProjection);
                    copyOfBaseScenarioResult.AdjustStartingPeriodOfResults(monthlyPeriod);

                    _forecastingCashFlowsListOfLists.Add(copyOfBaseScenarioResult.PremiumCalculationCashFlows);
                    _forecastingTimeSeriesListOfLists.Add(copyOfBaseScenarioResult.EnrollmentStateTimeSeries);
                    _forecastingDataTableList.Add(copyOfBaseScenarioResult.ServicingCosts);
                }
            }

            ForecastedPremiumCalculationCashFlows = CashFlowAggregator.AggregateCashFlows(_forecastingCashFlowsListOfLists);
            ForecastedEnrollmentTimeSeries = TimeSeriesAggregator.AggregateTimeSeries(_forecastingTimeSeriesListOfLists);
            ForecastedServicingCosts = DataTableAggregator.AggregateDataTables(_forecastingDataTableList);
        }

        private void SetupBaseResultsDictionary()
        {
            var globalPercentageFirstTimeEnrollees = PremiumComputationForecastingInput.PercentageFirstTimeEnrolleeProjections;

            foreach (var scenarioName in PremiumComputationForecastingInput.ScenarioNames)
            {
                var premiumComputationScenario = PremiumComputationForecastingInput.GetForecastingScenario(scenarioName);
                if (premiumComputationScenario == null) continue;

                var forecastingScenarioBaseResult = premiumComputationScenario.ComputePremiumResult();

                if (globalPercentageFirstTimeEnrollees != null && globalPercentageFirstTimeEnrollees.ContainsKey(scenarioName))
                {
                    var forecastingScenarioSeasonedResult = premiumComputationScenario.ComputePremiumResult(Constants.MonthsInOneYear, false);

                    var percentageFirstTimeEnrollees = globalPercentageFirstTimeEnrollees[scenarioName];
                    var percentageSeasonedEnrollees = 1.0 - percentageFirstTimeEnrollees;

                    forecastingScenarioBaseResult.ScaleResults(percentageFirstTimeEnrollees);
                    forecastingScenarioSeasonedResult.ScaleResults(percentageSeasonedEnrollees);

                    forecastingScenarioBaseResult.AggregateResults(forecastingScenarioSeasonedResult);
                }

                _forecastingScenariosBaseResultsDictionary.Add(scenarioName, forecastingScenarioBaseResult);
            }
        }
    }
}
