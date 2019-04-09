using System;
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
        private const string _existingEnrollees = "Existing Enrollee";

        private Dictionary<string, PremiumComputationResult> _forecastingScenariosBaseResultsDictionary;

        private List<List<PremiumCalculationCashFlow>> _forecastingCashFlowsListOfLists;
        private List<List<StudentEnrollmentStateTimeSeriesEntry>> _forecastingTimeSeriesListOfLists;
        private List<DataTable> _forecastingDataTableList;

        private bool _applyFirstYearPercentGlobally => PremiumComputationForecastingInput.ApplyFirstYearPercentGlobally;

        public PremiumComputationForecastingInput PremiumComputationForecastingInput { get; }

        public List<PremiumCalculationCashFlow> ForecastedPremiumCalculationCashFlows { get; private set; }
        public List<StudentEnrollmentStateTimeSeriesEntry> ForecastedEnrollmentTimeSeries { get; private set; }
        public DataTable ForecastedServicingCosts { get; private set; }

        public PremiumComputationForecastingEngine(PremiumComputationForecastingInput premiumComputationForecastingInput)
        {
            PremiumComputationForecastingInput = premiumComputationForecastingInput;

            _forecastingScenariosBaseResultsDictionary = new Dictionary<string, PremiumComputationResult>();

            _forecastingCashFlowsListOfLists = new List<List<PremiumCalculationCashFlow>>();
            _forecastingTimeSeriesListOfLists = new List<List<StudentEnrollmentStateTimeSeriesEntry>>();
            _forecastingDataTableList = new List<DataTable>();
        }

        /// <summary>
        /// Runs a forecast based on the premium computation forecasting input object provided.
        /// Aggregates all cashflows, costs, and time series into one result.
        /// </summary>
        public void RunForecast()
        {
            Console.WriteLine("Setting up base results for forecast...");
            SetupBaseResultsDictionaryAndProcessExistingEnrollees();

            var monthlyPeriodsToForecast = PremiumComputationForecastingInput.MonthlyPeriodsToForecast;
            var forecastedOverlayScenarios = PremiumComputationForecastingInput.ForecastedOverlayScenarios;
            var forecastedEnrollmentsProjection = PremiumComputationForecastingInput.ForecastedEnrollmentsProjection;
            var forecastedFirstYearEnrollees = forecastedEnrollmentsProjection.PercentageFirstYearEnrolleeProjections;

            foreach (var scenarioName in forecastedEnrollmentsProjection.EnrollmentCountProjections.Keys)
            {
                if (!_forecastingScenariosBaseResultsDictionary.ContainsKey(scenarioName)) continue;

                for (var monthlyPeriod = 0; monthlyPeriod < monthlyPeriodsToForecast; monthlyPeriod++)
                {
                    Console.WriteLine(string.Format("Forecasting for '{0}', period {1} of {2}...", 
                        scenarioName, monthlyPeriod, monthlyPeriodsToForecast));

                    var enrollmentCountProjection = forecastedEnrollmentsProjection.EnrollmentCountProjections[scenarioName][monthlyPeriod];

                    if (enrollmentCountProjection <= 0) continue;
                    var copyOfBaseScenarioResult = _forecastingScenariosBaseResultsDictionary[scenarioName].Copy();

                    if (!_applyFirstYearPercentGlobally && forecastedFirstYearEnrollees.ContainsKey(scenarioName))
                    {
                        var premiumComputationScenario = PremiumComputationForecastingInput.GetForecastingScenario(scenarioName);
                        var percentageFirstTimeEnrollees = forecastedFirstYearEnrollees[scenarioName][monthlyPeriod];

                        copyOfBaseScenarioResult = AdjustResultsForFirstTimeEnrollees(
                            premiumComputationScenario, 
                            copyOfBaseScenarioResult, 
                            percentageFirstTimeEnrollees);
                    }

                    if (forecastedOverlayScenarios.ContainsKey(monthlyPeriod) &&
                        forecastedOverlayScenarios[monthlyPeriod].ContainsKey(scenarioName) &&
                        PremiumComputationForecastingInput.ScenarioNames.Contains(scenarioName))
                    {
                        var overlayScenarioLogic = forecastedOverlayScenarios[monthlyPeriod][scenarioName];
                        var copyOfPremiumComputationScenario = PremiumComputationForecastingInput.GetForecastingScenario(scenarioName).Copy();
                        var overlayComputationScenario = overlayScenarioLogic.ApplyScenarioLogic(copyOfPremiumComputationScenario);
                        var overlayScenarioResult = overlayComputationScenario.ComputePremiumResult();

                        if (!_applyFirstYearPercentGlobally && forecastedFirstYearEnrollees.ContainsKey(scenarioName))
                        {
                            var percentageFirstTimeEnrollees = forecastedFirstYearEnrollees[scenarioName][monthlyPeriod];

                            overlayScenarioResult = AdjustResultsForFirstTimeEnrollees(
                                overlayComputationScenario,
                                overlayScenarioResult,
                                percentageFirstTimeEnrollees);
                        }

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

        private void SetupBaseResultsDictionaryAndProcessExistingEnrollees()
        {
            var globalPercentageFirstTimeEnrollees = PremiumComputationForecastingInput.PercentageFirstYearEnrolleeProjections;

            foreach (var scenarioName in PremiumComputationForecastingInput.ScenarioNames)
            {
                var premiumComputationScenario = PremiumComputationForecastingInput.GetForecastingScenario(scenarioName);
                if (premiumComputationScenario == null) continue;

                // Existing enrollees are adding directly to the final list of results for eventual aggregation of ouputs
                if (scenarioName.Contains(_existingEnrollees))
                {
                    var existingEnrolleeBaseResult = premiumComputationScenario.ComputePremiumResult(isNewStudent: false);

                    _forecastingCashFlowsListOfLists.Add(existingEnrolleeBaseResult.PremiumCalculationCashFlows);
                    _forecastingTimeSeriesListOfLists.Add(existingEnrolleeBaseResult.EnrollmentStateTimeSeries);
                    _forecastingDataTableList.Add(existingEnrolleeBaseResult.ServicingCosts);
                }
                else
                {
                    var forecastingScenarioBaseResult = premiumComputationScenario.ComputePremiumResult();

                    if (_applyFirstYearPercentGlobally && globalPercentageFirstTimeEnrollees.ContainsKey(scenarioName))
                    {
                        var percentageFirstTimeEnrollees = globalPercentageFirstTimeEnrollees[scenarioName];

                        forecastingScenarioBaseResult = AdjustResultsForFirstTimeEnrollees(
                            premiumComputationScenario,
                            forecastingScenarioBaseResult,
                            percentageFirstTimeEnrollees);
                    }

                    _forecastingScenariosBaseResultsDictionary.Add(scenarioName, forecastingScenarioBaseResult);
                }
            }
        }

        private PremiumComputationResult AdjustResultsForFirstTimeEnrollees(
            PremiumComputationEngine premiumComputationScenario, 
            PremiumComputationResult forecastingScenarioBaseResult, 
            double percentageFirstTimeEnrollees)
        {
            var percentageSeasonedEnrollees = 1.0 - percentageFirstTimeEnrollees;
            var copyOfPremiumComputationScenario = premiumComputationScenario.Copy();
            var forecastingScenarioSeasonedResult = copyOfPremiumComputationScenario.ComputePremiumResult(Constants.MonthsInOneYear);

            forecastingScenarioBaseResult.ScaleResults(percentageFirstTimeEnrollees);
            forecastingScenarioSeasonedResult.ScaleResults(percentageSeasonedEnrollees);

            forecastingScenarioBaseResult.AggregateResults(forecastingScenarioSeasonedResult);

            return forecastingScenarioBaseResult;
        }
    }
}
