using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using EduSafe.Common;
using EduSafe.Common.Curves;
using EduSafe.Core.BusinessLogic.Aggregation;
using EduSafe.Core.BusinessLogic.Containers;
using EduSafe.Core.BusinessLogic.Containers.CashFlows;
using EduSafe.Core.BusinessLogic.Containers.TimeSeries;
using EduSafe.Core.Interfaces;

namespace EduSafe.Core.BusinessLogic.Scenarios
{
    public class PremiumComputationForecastingEngine
    {
        private const string _existingEnrollees = "Existing Enrollee";

        private Dictionary<string, PremiumComputationResult> _forecastingScenariosBaseResultsDictionary;
        private Dictionary<string, double> _forecastingScenariosBaseResultsPremiumDictionary;
        private Dictionary<string, double?> _forecastingScenariosSeasonedResultsPremiumDictionary;

        private List<List<PremiumCalculationCashFlow>> _forecastingCashFlowsListOfLists;
        private List<List<StudentEnrollmentStateTimeSeriesEntry>> _forecastingTimeSeriesListOfLists;
        private List<DataTable> _forecastingDataTableList;

        private bool _applyFirstYearPercentGlobally => PremiumComputationForecastingInput.ApplyFirstYearPercentGlobally;
        private bool _ignoreRollForwardOnRateCurves => PremiumComputationForecastingInput.IgnoreRollForwardOnRateCurves;

        public PremiumComputationForecastingInput PremiumComputationForecastingInput { get; }

        public List<PremiumCalculationCashFlow> ForecastedPremiumCalculationCashFlows { get; private set; }
        public List<StudentEnrollmentStateTimeSeriesEntry> ForecastedEnrollmentTimeSeries { get; private set; }
        public DataTable ForecastedServicingCosts { get; private set; }

        public PremiumComputationForecastingEngine(PremiumComputationForecastingInput premiumComputationForecastingInput)
        {
            PremiumComputationForecastingInput = premiumComputationForecastingInput;

            _forecastingScenariosBaseResultsDictionary = new Dictionary<string, PremiumComputationResult>();
            _forecastingScenariosBaseResultsPremiumDictionary = new Dictionary<string, double>();
            _forecastingScenariosSeasonedResultsPremiumDictionary = new Dictionary<string, double?>();

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

                for (var monthlyPeriod = 0; monthlyPeriod <= monthlyPeriodsToForecast; monthlyPeriod++)
                {
                    Console.WriteLine(string.Format("Forecasting for '{0}', period {1} of {2}...", 
                        scenarioName, monthlyPeriod, monthlyPeriodsToForecast));

                    var enrollmentCountProjection = forecastedEnrollmentsProjection.EnrollmentCountProjections[scenarioName][monthlyPeriod];

                    if (enrollmentCountProjection <= 0) continue;

                    // This section computes the base result, which may have to computed each period if roll-down on rate curves
                    // is not ignored. Otherwise, it can be pulled from a precomputed dictionary.
                    PremiumComputationResult forecastingScenarioBaseResult;
                    double forecastingScenarioBaseResultPremium; double? forecastingScenarioSeasonedResultPremium;
                    if (!_ignoreRollForwardOnRateCurves && monthlyPeriod > 0)
                    {
                        var scenarioResultContainer = ComputeBaseResultWithRateCurveRollForward(scenarioName, monthlyPeriod);
                        forecastingScenarioBaseResult = scenarioResultContainer.PremiumComputationResult;
                        forecastingScenarioBaseResultPremium = scenarioResultContainer.BaseScenarioComputationResultPremium;
                        forecastingScenarioSeasonedResultPremium = scenarioResultContainer.SeasonedScenarioComputationResultPremium;
                    }
                    else
                    {
                        forecastingScenarioBaseResult = _forecastingScenariosBaseResultsDictionary[scenarioName].Copy();
                        forecastingScenarioBaseResultPremium = _forecastingScenariosBaseResultsPremiumDictionary[scenarioName];
                        _forecastingScenariosSeasonedResultsPremiumDictionary.TryGetValue(scenarioName, out forecastingScenarioSeasonedResultPremium);
                    }

                    if (!_applyFirstYearPercentGlobally && forecastedFirstYearEnrollees.ContainsKey(scenarioName))
                    {
                        var premiumComputationScenario = PremiumComputationForecastingInput.GetForecastingScenario(scenarioName);
                        var percentageFirstTimeEnrollees = forecastedFirstYearEnrollees[scenarioName][monthlyPeriod];

                        var scenarioResultContainer = AdjustResultsForFirstTimeEnrollees(
                            premiumComputationScenario, 
                            forecastingScenarioBaseResult, 
                            percentageFirstTimeEnrollees);

                        forecastingScenarioBaseResult = scenarioResultContainer.PremiumComputationResult;
                        forecastingScenarioSeasonedResultPremium = scenarioResultContainer.SeasonedScenarioComputationResultPremium;
                    }

                    if (forecastedOverlayScenarios.ContainsKey(monthlyPeriod) &&
                        forecastedOverlayScenarios[monthlyPeriod].ContainsKey(scenarioName) &&
                        PremiumComputationForecastingInput.ScenarioNames.Contains(scenarioName))
                    {
                        var overlayScenarioLogicList = forecastedOverlayScenarios[monthlyPeriod][scenarioName];

                        forecastingScenarioBaseResult = ApplyForecastedOverlayScenarios(
                            forecastedFirstYearEnrollees,
                            forecastingScenarioBaseResultPremium,
                            forecastingScenarioSeasonedResultPremium,
                            overlayScenarioLogicList,
                            scenarioName, 
                            monthlyPeriod);
                    }

                    forecastingScenarioBaseResult.ScaleResults(enrollmentCountProjection);
                    forecastingScenarioBaseResult.AdjustStartingPeriodOfResults(monthlyPeriod);

                    _forecastingCashFlowsListOfLists.Add(forecastingScenarioBaseResult.PremiumCalculationCashFlows);
                    _forecastingTimeSeriesListOfLists.Add(forecastingScenarioBaseResult.EnrollmentStateTimeSeries);
                    _forecastingDataTableList.Add(forecastingScenarioBaseResult.ServicingCosts);
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

                // Existing enrollees are added directly to the final list of results for eventual aggregation of ouputs.
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
                    var forecastingScenarioBaseResultPremium = forecastingScenarioBaseResult.CalculatedMonthlyPremium;
                    _forecastingScenariosBaseResultsPremiumDictionary.Add(scenarioName, forecastingScenarioBaseResultPremium);

                    if (_applyFirstYearPercentGlobally && globalPercentageFirstTimeEnrollees.ContainsKey(scenarioName))
                    {
                        var percentageFirstTimeEnrollees = globalPercentageFirstTimeEnrollees[scenarioName];

                        var scenarioResultContainer = AdjustResultsForFirstTimeEnrollees(
                            premiumComputationScenario,
                            forecastingScenarioBaseResult,
                            percentageFirstTimeEnrollees,
                            addSeasonedResultToBaseDictionary: true);

                        forecastingScenarioBaseResult = scenarioResultContainer.PremiumComputationResult;
                    }

                    _forecastingScenariosBaseResultsDictionary.Add(scenarioName, forecastingScenarioBaseResult);
                }
            }
        }

        private ScenarioResultContainer ComputeBaseResultWithRateCurveRollForward(string scenarioName, int monthlyPeriod)
        {
            var globalPercentageFirstTimeEnrollees = PremiumComputationForecastingInput.PercentageFirstYearEnrolleeProjections;
            var premiumComputationScenario = PremiumComputationForecastingInput.GetForecastingScenario(scenarioName).Copy();

            premiumComputationScenario.SetNumberOfForecastedPeriodsAhead(monthlyPeriod);
            var forecastingScenarioBaseResult = premiumComputationScenario.ComputePremiumResult();
            var forecastingScenarioBaseResultPremium = forecastingScenarioBaseResult.CalculatedMonthlyPremium;

            double? forecastingScenarioSeasonedResultPremium = null;
            if (_applyFirstYearPercentGlobally && globalPercentageFirstTimeEnrollees.ContainsKey(scenarioName))
            {
                var percentageFirstTimeEnrollees = globalPercentageFirstTimeEnrollees[scenarioName];

                var scenarioResultContainer = AdjustResultsForFirstTimeEnrollees(
                    premiumComputationScenario,
                    forecastingScenarioBaseResult,
                    percentageFirstTimeEnrollees);

                forecastingScenarioBaseResult = scenarioResultContainer.PremiumComputationResult;
                forecastingScenarioSeasonedResultPremium = scenarioResultContainer.SeasonedScenarioComputationResultPremium;
            }

            return new ScenarioResultContainer
            {
                PremiumComputationResult = forecastingScenarioBaseResult,
                BaseScenarioComputationResultPremium = forecastingScenarioBaseResultPremium,
                SeasonedScenarioComputationResultPremium = forecastingScenarioSeasonedResultPremium,
            };
        }

        private PremiumComputationResult ApplyForecastedOverlayScenarios(
            Dictionary<string, DataCurve<double>> forecastedFirstYearEnrollees,
            double forecastingScenarioBaseResultPremium,
            double? forecastingScenarioSeasonedResultPremium,
            List<IScenario> overlayScenarioLogicList, 
            string scenarioName, 
            int monthlyPeriod)
        {
            var overlayComputationScenario = PremiumComputationForecastingInput.GetForecastingScenario(scenarioName).Copy();

            foreach (var overlayScenarioLogic in overlayScenarioLogicList)
                overlayComputationScenario = overlayScenarioLogic.ApplyScenarioLogic(overlayComputationScenario);

            // The idea here is that the forecast assumes you must live with whatever premium was calculated in the base scenario
            // for the life of the customer, which is the most conservative approach to take. Note, that as long as any of the scenarios
            // in the list disallow premiums to adjust, then it will not be allowed for the whole period.
            if (overlayScenarioLogicList.Any(s => !s.AllowPremiumsToAdjust))
            {
                var overlayScenarioResult = overlayComputationScenario.ComputeResultWithGivenPremium(forecastingScenarioBaseResultPremium);

                if (!_applyFirstYearPercentGlobally && forecastedFirstYearEnrollees.ContainsKey(scenarioName))
                {
                    var percentageFirstTimeEnrollees = forecastedFirstYearEnrollees[scenarioName][monthlyPeriod];

                    var scenarioResultContainer = AdjustResultsForFirstTimeEnrollees(
                        overlayComputationScenario,
                        overlayScenarioResult,
                        percentageFirstTimeEnrollees,
                        forecastingScenarioSeasonedResultPremium);

                    overlayScenarioResult = scenarioResultContainer.PremiumComputationResult;
                }

                return overlayScenarioResult;
            }
            // Otherwise, the other book-end option would be allow premiums to adjust immediately for the scenario that is applied.
            // Note that this is specified on a month-by-month basis in the projection, allowing a switch between these approaches.
            else
            {
                var overlayScenarioResult = overlayComputationScenario.ComputePremiumResult();

                if (!_applyFirstYearPercentGlobally && forecastedFirstYearEnrollees.ContainsKey(scenarioName))
                {
                    var percentageFirstTimeEnrollees = forecastedFirstYearEnrollees[scenarioName][monthlyPeriod];

                    var scenarioResultContainer = AdjustResultsForFirstTimeEnrollees(
                        overlayComputationScenario,
                        overlayScenarioResult,
                        percentageFirstTimeEnrollees);

                    overlayScenarioResult = scenarioResultContainer.PremiumComputationResult;
                }

                return overlayScenarioResult;
            }            
        }

        private ScenarioResultContainer AdjustResultsForFirstTimeEnrollees(
            PremiumComputationEngine premiumComputationScenario, 
            PremiumComputationResult forecastingScenarioBaseResult, 
            double percentageFirstTimeEnrollees,
            double? calculatedSeasonedMonthlyPremium = null,
            bool addSeasonedResultToBaseDictionary = false)
        {
            var forecastingScenarioBaseResultPremium = forecastingScenarioBaseResult.CalculatedMonthlyPremium;
            var percentageSeasonedEnrollees = 1.0 - percentageFirstTimeEnrollees;
            if (percentageSeasonedEnrollees <= 0)
            {
                return new ScenarioResultContainer
                {
                    PremiumComputationResult = forecastingScenarioBaseResult,
                    BaseScenarioComputationResultPremium = forecastingScenarioBaseResultPremium,
                    SeasonedScenarioComputationResultPremium = calculatedSeasonedMonthlyPremium,
                };
            }

            var copyOfPremiumComputationScenario = premiumComputationScenario.Copy();
            var forecastingScenarioSeasonedResult = calculatedSeasonedMonthlyPremium.HasValue 
                ? copyOfPremiumComputationScenario.ComputeResultWithGivenPremium(calculatedSeasonedMonthlyPremium.Value, Constants.MonthsInOneYear)
                : copyOfPremiumComputationScenario.ComputePremiumResult(Constants.MonthsInOneYear);

            var forecastingScenarioSeasonedResultPremium = forecastingScenarioSeasonedResult.CalculatedMonthlyPremium;
            if (addSeasonedResultToBaseDictionary)
            {
                var scenarioName = premiumComputationScenario.ScenarioName;
                _forecastingScenariosSeasonedResultsPremiumDictionary.Add(scenarioName, forecastingScenarioSeasonedResultPremium);
            }

            forecastingScenarioBaseResult.ScaleResults(percentageFirstTimeEnrollees);
            forecastingScenarioSeasonedResult.ScaleResults(percentageSeasonedEnrollees);

            forecastingScenarioBaseResult.AggregateResults(forecastingScenarioSeasonedResult);

            return new ScenarioResultContainer
            {
                PremiumComputationResult = forecastingScenarioBaseResult,
                BaseScenarioComputationResultPremium = forecastingScenarioBaseResultPremium,
                SeasonedScenarioComputationResultPremium = forecastingScenarioSeasonedResultPremium,
            };
        }

        private struct ScenarioResultContainer
        {
            public PremiumComputationResult PremiumComputationResult;
            public double BaseScenarioComputationResultPremium;
            public double? SeasonedScenarioComputationResultPremium;
        }
    }
}
