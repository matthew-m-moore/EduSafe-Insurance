using System.Collections.Generic;
using System.Data;
using System.Linq;
using EduSafe.Common;
using EduSafe.Common.ExtensionMethods;
using EduSafe.Core.BusinessLogic.Aggregation;
using EduSafe.Core.BusinessLogic.Containers.CashFlows;
using EduSafe.Core.BusinessLogic.Containers.TimeSeries;

namespace EduSafe.Core.BusinessLogic.Containers
{
    public class PremiumComputationResult
    {
        public int? ScenarioId { get; set; }
        public string ScenarioName { get; set; }

        public DataTable ServicingCosts { get; set; }
        public List<PremiumCalculationCashFlow> PremiumCalculationCashFlows { get; set; }
        public List<StudentEnrollmentStateTimeSeriesEntry> EnrollmentStateTimeSeries { get; set; }       
        public double CalculatedMonthlyPremium { get; set; }

        public PremiumComputationResult Copy()
        {
            return new PremiumComputationResult
            {
                ScenarioId = ScenarioId.HasValue ? new int?(ScenarioId.Value) : null,
                ScenarioName = new string(ScenarioName.ToCharArray()),

                ServicingCosts = ServicingCosts.AsEnumerable().CopyToDataTable(),
                PremiumCalculationCashFlows = PremiumCalculationCashFlows.Select(c => c.Copy() as PremiumCalculationCashFlow).ToList(),
                EnrollmentStateTimeSeries = EnrollmentStateTimeSeries.Select(t => t.Copy() as StudentEnrollmentStateTimeSeriesEntry).ToList(),
                CalculatedMonthlyPremium = CalculatedMonthlyPremium
            };
        }

        public void AdjustStartingPeriodOfResults(int adjustedStartingPeriod)
        {
            var cashFlowStartingPeriod = PremiumCalculationCashFlows.First().Period;
            var incrementalCashFlowPeriodDifference = adjustedStartingPeriod - cashFlowStartingPeriod;
            PremiumCalculationCashFlows.ForEach(c => c.Period += incrementalCashFlowPeriodDifference);

            var timeSeriesStartingPeriod = EnrollmentStateTimeSeries.First().Period;
            var incrementalTimeSeriesPeriodDifference = adjustedStartingPeriod - timeSeriesStartingPeriod;
            EnrollmentStateTimeSeries.ForEach(t => t.Period += incrementalTimeSeriesPeriodDifference);

            var dataRowsCollection = ServicingCosts.AsEnumerable();
            var dataRowsCollectionStartingPeriod = dataRowsCollection.First().Field<double>(Constants.PeriodIdentifier);
            var incrementalDataRowPeriodDifference = adjustedStartingPeriod - dataRowsCollectionStartingPeriod;
            
            foreach (var dataRow in dataRowsCollection)
            {
                var dataRowPeriodValue = dataRow.Field<double>(Constants.PeriodIdentifier);
                dataRowPeriodValue += incrementalDataRowPeriodDifference;
                dataRow[Constants.PeriodIdentifier] = dataRowPeriodValue;
            }
        }

        public void ScaleResults(double scaleFactor)
        {
            PremiumCalculationCashFlows.ForEach(c => c.Scale(scaleFactor));
            EnrollmentStateTimeSeries.ForEach(t => t.Scale(scaleFactor));

            foreach (var dataRow in ServicingCosts.AsEnumerable()) dataRow.Scale(scaleFactor);

            CalculatedMonthlyPremium *= scaleFactor;
        }

        public void AggregateResults(PremiumComputationResult premiumComputationResult)
        {
            var combinedCashFlowsList = new List<List<PremiumCalculationCashFlow>>
            {
                PremiumCalculationCashFlows,
                premiumComputationResult.PremiumCalculationCashFlows
            };           

            var combinedTimeSeriesList = new List<List<StudentEnrollmentStateTimeSeriesEntry>>
            {
                EnrollmentStateTimeSeries,
                premiumComputationResult.EnrollmentStateTimeSeries
            };            

            var combinedDataTablesList = new List<DataTable>
            {
                ServicingCosts,
                premiumComputationResult.ServicingCosts
            };

            PremiumCalculationCashFlows = CashFlowAggregator.AggregateCashFlows(combinedCashFlowsList);
            EnrollmentStateTimeSeries = TimeSeriesAggregator.AggregateTimeSeries(combinedTimeSeriesList);
            ServicingCosts = DataTableAggregator.AggregateDataTables(combinedDataTablesList);

            CalculatedMonthlyPremium += premiumComputationResult.CalculatedMonthlyPremium;
        }
    }
}
