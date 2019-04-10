using System.Collections.Generic;
using System.Data;
using System.Linq;
using EduSafe.Common;
using EduSafe.Common.ExtensionMethods;

namespace EduSafe.Core.BusinessLogic.Aggregation
{
    public class DataTableAggregator
    {
        /// <summary>
        /// Aggregates mulitple data tables into a single data table, aligning based on the "Period" value.
        /// </summary>
        public static DataTable AggregateDataTables(List<DataTable> listOfDataTables)
        {
            var listOfAggregatedDataRows = new List<DataRow>();

            foreach (var dataTable in listOfDataTables)
            {
                var collectionOfDataRows = dataTable.AsEnumerable();
                if (!collectionOfDataRows.Any()) continue;

                // This code creates a copy of the data rows prior to aggregation
                var dataTableToAggregate = dataTable.Clone();
                foreach (var dataRow in collectionOfDataRows) dataTableToAggregate.ImportRow(dataRow);

                var countOfDataRowsToAdd = dataTableToAggregate.Rows.Count;

                var startingPeriodOfAggregatedDataRows = 0d;
                var endingPeriodOfAggregatedDataRows = 0d;

                for (var dataRowCounter = 0; dataRowCounter < countOfDataRowsToAdd; dataRowCounter++)
                {
                    // If the algorithm runs out of data rows to add, break out of the loop
                    if (dataRowCounter >= countOfDataRowsToAdd) break;

                    var dataRowToAggregate = dataTableToAggregate.Rows[dataRowCounter];
                    var periodOfDataRowToAggregate = dataRowToAggregate.Field<double>(Constants.PeriodIdentifier);

                    if (!listOfAggregatedDataRows.Any())
                    {
                        listOfAggregatedDataRows.Add(dataRowToAggregate);
                        continue;
                    }

                    startingPeriodOfAggregatedDataRows = listOfAggregatedDataRows.First().Field<double>(Constants.PeriodIdentifier);
                    endingPeriodOfAggregatedDataRows = listOfAggregatedDataRows.Last().Field<double>(Constants.PeriodIdentifier);

                    // For data rows that have "Period" before the existing that are aggregated, insert them in front
                    if (periodOfDataRowToAggregate < startingPeriodOfAggregatedDataRows)
                    {
                        listOfAggregatedDataRows.Insert(0, dataRowToAggregate);
                    }
                    // For data rows that have "Period" beyond the existing that are aggregated, add them at the end
                    else if (periodOfDataRowToAggregate > endingPeriodOfAggregatedDataRows)
                    {
                        listOfAggregatedDataRows.Add(dataRowToAggregate);
                    }
                    else
                    {
                        var closestAggregatedDataRow = listOfAggregatedDataRows
                            .Select((d, i) => new { DataRow = d, Index = i })
                            .Last(o => o.DataRow.Field<double>(Constants.PeriodIdentifier) <= periodOfDataRowToAggregate);

                        var indexOfClosestAggregatedDataRow = closestAggregatedDataRow.Index;
                        var periodOfClosestAggregatedDataRow = closestAggregatedDataRow.DataRow.Field<double>(Constants.PeriodIdentifier);

                        // If the "Period" is between the starting period and the next closest, insert it in between
                        if (periodOfDataRowToAggregate > periodOfClosestAggregatedDataRow)
                        {
                            listOfAggregatedDataRows.Insert(indexOfClosestAggregatedDataRow + 1, dataRowToAggregate);
                        }
                        // Otherwise, aggregate at the matching "Period" number for those data rows
                        else
                        {
                            listOfAggregatedDataRows[indexOfClosestAggregatedDataRow].Aggregate(dataRowToAggregate);
                        }
                    }
                }
            }

            var dataTableOfAggregatedDataRows = listOfAggregatedDataRows.CopyToDataTable();
            return dataTableOfAggregatedDataRows;
        }
    }
}
