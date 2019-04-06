using System.Collections.Generic;
using System.Data;
using System.Linq;
using EduSafe.Core.BusinessLogic.Containers.CashFlows;

namespace EduSafe.Core.BusinessLogic.Aggregation
{
    public class CashFlowAggregator
    {
        /// <summary>
        /// Aggregates a multpile lists of cash flows into a single list of cash flows, aligning based on the "Period" value.
        /// </summary>
        public static List<T> AggregateCashFlows<T>(List<List<T>> listOfCashFlows) where T : CashFlow
        {
            var listOfAggregatedCashFlows = new List<T>();

            foreach (var listOfCashFlow in listOfCashFlows)
            {
                if (!listOfCashFlow.Any()) continue;

                var cashFlowsToAggregate = listOfCashFlow.Select(c => c.Copy() as T).ToList();
                var countOfCashFlowsToAdd = cashFlowsToAggregate.Count;

                var startingPeriodOfAggregatedCashFlows = 0;
                var endingPeriodOfAggregatedCashFlows = 0;

                for (var cashFlowCounter = 0; cashFlowCounter < countOfCashFlowsToAdd; cashFlowCounter++)
                {
                    // If the algorithm runs out of cash flows to add, break out of the loop
                    if (cashFlowCounter >= countOfCashFlowsToAdd) break;

                    var cashFlowToAggregate = cashFlowsToAggregate[cashFlowCounter];
                    var periodOfCashFlowToAggregate = cashFlowToAggregate.Period;

                    if (!listOfAggregatedCashFlows.Any())
                    {
                        listOfAggregatedCashFlows.Add(cashFlowToAggregate);
                        continue;
                    }

                    startingPeriodOfAggregatedCashFlows = listOfAggregatedCashFlows.First().Period;
                    endingPeriodOfAggregatedCashFlows = listOfAggregatedCashFlows.Last().Period;

                    // For cash flows that have "Period" before the existing that are aggregated, insert them in front
                    if (periodOfCashFlowToAggregate < startingPeriodOfAggregatedCashFlows)
                    {
                        listOfAggregatedCashFlows.Insert(0, cashFlowToAggregate);
                    }
                    // For cash flows that have "Period" beyond the existing that are aggregated, add them at the end
                    else if (periodOfCashFlowToAggregate > endingPeriodOfAggregatedCashFlows)
                    {
                        listOfAggregatedCashFlows.Add(cashFlowToAggregate);
                    }
                    else
                    {
                        var closestAggregatedCashFlow = listOfAggregatedCashFlows
                            .Select((c, i) => new { CashFlow = c, Index = i })
                            .First(o => o.CashFlow.Period <= periodOfCashFlowToAggregate);

                        var indexOfClosestAggregatedCashFlow = closestAggregatedCashFlow.Index;
                        var periodOfClosestAggregatedCashFlow = closestAggregatedCashFlow.CashFlow.Period;

                        // If the "Period" is between the starting period and the next closest, insert it in between
                        if (periodOfCashFlowToAggregate < periodOfClosestAggregatedCashFlow)
                        {
                            listOfAggregatedCashFlows.Insert(indexOfClosestAggregatedCashFlow, cashFlowToAggregate);
                        }
                        // Otherwise, aggregate the cash flow at the matching "Period" number
                        else
                        {
                            listOfAggregatedCashFlows[indexOfClosestAggregatedCashFlow].Aggregate(cashFlowToAggregate);
                        }
                    }
                }
            }

            return listOfAggregatedCashFlows;
        }
    }
}
