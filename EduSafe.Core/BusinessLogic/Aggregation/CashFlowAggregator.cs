using System.Collections.Generic;
using System.Data;
using System.Linq;
using EduSafe.Common;
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

        /// <summary>
        /// Aggregates mulitple data tables into a single data table of cash flows, aligning based on the "Period" value.
        /// </summary>
        public static DataTable AggregateDataTables<T>(List<DataTable> listOfDataTables) where T : CashFlow
        {
            var dataRowsOfAggregateCashFlows = new List<DataRow>();
            
            foreach (var dataTable in listOfDataTables)
            {
                var collectionOfDataRows = dataTable.AsEnumerable();
                if (!collectionOfDataRows.Any()) continue;
                
                var dataTableToAggregate = new DataTable();
                foreach (var dataRow in collectionOfDataRows) dataTableToAggregate.ImportRow(dataRow);

                var countOfDataRowsToAdd = dataTableToAggregate.Rows.Count;

                var startingPeriodOfAggregatedCashFlows = 0;
                var endingPeriodOfAggregatedCashFlows = 0;

                for (var dataRowCounter = 0; dataRowCounter < countOfDataRowsToAdd; dataRowCounter++)
                {
                    // If the algorithm runs out of data rows to add, break out of the loop
                    if (dataRowCounter >= countOfDataRowsToAdd) break;

                    var dataRowToAggregate = dataTableToAggregate.Rows[dataRowCounter];
                    var periodOfDataRowToAggregate = dataRowToAggregate.Field<int>(Constants.PeriodIdentifier);

                    if (!dataRowsOfAggregateCashFlows.Any())
                    {
                        dataRowsOfAggregateCashFlows.Add(dataRowToAggregate);
                        continue;
                    }

                    startingPeriodOfAggregatedCashFlows = dataRowsOfAggregateCashFlows.First().Field<int>(Constants.PeriodIdentifier);
                    endingPeriodOfAggregatedCashFlows = dataRowsOfAggregateCashFlows.Last().Field<int>(Constants.PeriodIdentifier);

                    // For data rows that have "Period" before the existing that are aggregated, insert them in front
                    if (periodOfDataRowToAggregate < startingPeriodOfAggregatedCashFlows)
                    {
                        dataRowsOfAggregateCashFlows.Insert(0, dataRowToAggregate);
                    }
                    // For data rows that have "Period" beyond the existing that are aggregated, add them at the end
                    else if (periodOfDataRowToAggregate > endingPeriodOfAggregatedCashFlows)
                    {
                        dataRowsOfAggregateCashFlows.Add(dataRowToAggregate);
                    }
                    else
                    {
                        var closestAggregatedCashFlow = dataRowsOfAggregateCashFlows
                            .Select((d, i) => new { DataRow = d, Index = i })
                            .First(o => o.DataRow.Field<int>(Constants.PeriodIdentifier) <= periodOfDataRowToAggregate);

                        var indexOfClosestAggregatedCashFlow = closestAggregatedCashFlow.Index;
                        var periodOfClosestAggregatedCashFlow = closestAggregatedCashFlow.DataRow.Field<int>(Constants.PeriodIdentifier);

                        // If the "Period" is between the starting period and the next closest, insert it in between
                        if (periodOfDataRowToAggregate < periodOfClosestAggregatedCashFlow)
                        {
                            dataRowsOfAggregateCashFlows.Insert(indexOfClosestAggregatedCashFlow, dataRowToAggregate);
                        }
                        // Otherwise, aggregate the cash flow at the matching "Period" number for those data rows
                        else
                        {
                            AggregateDataRows(dataRowsOfAggregateCashFlows[indexOfClosestAggregatedCashFlow], dataRowToAggregate);
                        }
                    }
                }
            }

            var dataTableOfAggregatedCashFlows = dataRowsOfAggregateCashFlows.CopyToDataTable();
            return dataTableOfAggregatedCashFlows;
        }

        private static void AggregateDataRows(DataRow aggregatedDataRow, DataRow dataRowToAggregate)
        {
            foreach (DataColumn column in dataRowToAggregate.Table.Columns)
            {
                var columnName = column.ColumnName;
                if (columnName == Constants.PeriodIdentifier) continue;

                var valueToAggregate = dataRowToAggregate[columnName];
                if (!aggregatedDataRow.Table.Columns.Contains(columnName))
                {
                    aggregatedDataRow[columnName] = valueToAggregate;
                    continue;
                }

                // If these values are not like types, skip the aggregation
                var aggregatedValue = aggregatedDataRow[columnName];
                if (valueToAggregate is double && aggregatedValue is double)
                {
                    var totalAggregatedValue = (double)aggregatedValue + (double)valueToAggregate;
                    aggregatedDataRow[columnName] = totalAggregatedValue;
                    continue;
                }

                // It's highly unlikely we'll ever have integer values in a DataTable for aggregation
                // But, it's better to cover this corner case while it's on my mind
                if (valueToAggregate is int && aggregatedValue is int)
                {
                    var totalAggregatedValue = (int)aggregatedValue + (int)valueToAggregate;
                    aggregatedDataRow[columnName] = totalAggregatedValue;
                }
            }
        }
    }
}
