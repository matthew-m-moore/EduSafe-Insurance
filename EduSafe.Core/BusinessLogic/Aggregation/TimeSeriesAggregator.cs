using System.Collections.Generic;
using System.Linq;
using EduSafe.Core.BusinessLogic.Containers.TimeSeries;

namespace EduSafe.Core.BusinessLogic.Aggregation
{
    public class TimeSeriesAggregator
    {
        /// <summary>
        /// Aggregates a multpile lists of time series into a single list of cash flows, aligning based on the "Period" value.
        /// </summary>
        public static List<T> AggregateTimeSeries<T>(List<List<T>> listOfTimeSeries) where T : TimeSeriesEntry
        {
            var listOfAggregatedTimeSeriesEntries = new List<T>();

            foreach (var selectedTimeSeries in listOfTimeSeries)
            {
                if (!selectedTimeSeries.Any()) continue;

                var timeSeriesToAggregate = selectedTimeSeries.Select(t => t.Copy() as T).ToList();
                var countOfTimeSeriesEntriesToAdd = timeSeriesToAggregate.Count;

                var startingPeriodOfAggregatedTimeSeries = 0;
                var endingPeriodOfAggregatedTimeSeries = 0;

                for (var timeSeriesEntryCounter = 0; timeSeriesEntryCounter < countOfTimeSeriesEntriesToAdd; timeSeriesEntryCounter++)
                {
                    // If the algorithm runs out of entries to add, break out of the loop
                    if (timeSeriesEntryCounter >= countOfTimeSeriesEntriesToAdd) break;

                    var timeSeriesEntryToAggregate = timeSeriesToAggregate[timeSeriesEntryCounter];
                    var periodOfTimeSeriesEntryToAggregate = timeSeriesEntryToAggregate.Period;

                    if (!listOfAggregatedTimeSeriesEntries.Any())
                    {
                        listOfAggregatedTimeSeriesEntries.Add(timeSeriesEntryToAggregate);
                        continue;
                    }

                    startingPeriodOfAggregatedTimeSeries = listOfAggregatedTimeSeriesEntries.First().Period;
                    endingPeriodOfAggregatedTimeSeries = listOfAggregatedTimeSeriesEntries.Last().Period;

                    // For time series entries that have "Period" before the existing that are aggregated, insert them in front
                    if (periodOfTimeSeriesEntryToAggregate < startingPeriodOfAggregatedTimeSeries)
                    {
                        listOfAggregatedTimeSeriesEntries.Insert(0, timeSeriesEntryToAggregate);
                    }
                    // For time series entries that have "Period" beyond the existing that are aggregated, add them at the end
                    else if (periodOfTimeSeriesEntryToAggregate > endingPeriodOfAggregatedTimeSeries)
                    {
                        listOfAggregatedTimeSeriesEntries.Add(timeSeriesEntryToAggregate);
                    }
                    else
                    {
                        var closestAggregatedTimeSeriesEntry = listOfAggregatedTimeSeriesEntries
                            .Select((c, i) => new { CashFlow = c, Index = i })
                            .First(o => o.CashFlow.Period <= periodOfTimeSeriesEntryToAggregate);

                        var indexOfClosestAggregatedTimeSeriesEntry = closestAggregatedTimeSeriesEntry.Index;
                        var periodOfClosestAggregatedTimeSeriesEntry = closestAggregatedTimeSeriesEntry.CashFlow.Period;

                        // If the "Period" is between the starting period and the next closest, insert it in between
                        if (periodOfTimeSeriesEntryToAggregate < periodOfClosestAggregatedTimeSeriesEntry)
                        {
                            listOfAggregatedTimeSeriesEntries.Insert(indexOfClosestAggregatedTimeSeriesEntry, timeSeriesEntryToAggregate);
                        }
                        // Otherwise, aggregate the time series entry at the matching "Period" number
                        else
                        {
                            listOfAggregatedTimeSeriesEntries[indexOfClosestAggregatedTimeSeriesEntry].Aggregate(timeSeriesEntryToAggregate);
                        }
                    }
                }
            }

            return listOfAggregatedTimeSeriesEntries;
        }
    }
}
