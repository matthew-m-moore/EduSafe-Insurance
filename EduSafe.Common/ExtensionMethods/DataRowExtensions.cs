using System;
using System.Collections.Generic;
using System.Data;

namespace EduSafe.Common.ExtensionMethods
{
    public static class DataRowExtensions
    {
        private static HashSet<Type> _numericTypesSet = new HashSet<Type>
        {
            typeof(int),
            typeof(uint),
            typeof(long),
            typeof(ulong),
            typeof(byte),
            typeof(sbyte),
            typeof(short),
            typeof(ushort),
            typeof(float),
            typeof(double),
            typeof(decimal)
        };

        /// <summary>
        /// Aggregates numeric values of two data rows with shared schema, and converts all values to double in doing so.
        /// </summary>
        public static void Aggregate(this DataRow aggregatedDataRow, DataRow dataRowToAggregate)
        {
            foreach (DataColumn column in dataRowToAggregate.Table.Columns)
            {
                var columnName = column.ColumnName;
                if (columnName == Constants.PeriodIdentifier) continue;
                
                if (!aggregatedDataRow.Table.Columns.Contains(columnName))
                {
                    throw new Exception(string.Format("ERROR: Data table columns and schema must match to perform aggregation. "
                        + "The column '{0}' was not found in a table.", columnName));
                }

                var valueToAggregate = dataRowToAggregate[columnName];
                var aggregatedValue = aggregatedDataRow[columnName];

                // If these values are not numeric types, skip the aggregation
                if (IsNumeric(valueToAggregate) && IsNumeric(aggregatedValue))
                {
                    var totalAggregatedValue = (double)aggregatedValue + (double)valueToAggregate;
                    aggregatedDataRow[columnName] = totalAggregatedValue;
                }              
            }
        }

        /// <summary>
        /// Scales numeric values of a data row, and converts all values to double in doing so.
        /// </summary>
        public static void Scale(this DataRow dataRowToScale, double scaleFactor)
        {
            foreach (DataColumn column in dataRowToScale.Table.Columns)
            {
                var columnName = column.ColumnName;
                if (columnName == Constants.PeriodIdentifier) continue;

                // If these values are not numeric types, skip the scaling
                var valuetoScale = dataRowToScale[columnName];
                if (IsNumeric(valuetoScale))
                {
                    var scaledValue = (double)valuetoScale * scaleFactor;
                    dataRowToScale[columnName] = scaledValue;
                    continue;
                }
            }
        }

        private static bool IsNumeric(object objectToCheck)
        {
            if (objectToCheck == null) return false;
            var type = objectToCheck.GetType();

            if (_numericTypesSet.Contains(type))
                return true;

            var underlyingType = Nullable.GetUnderlyingType(type);
            if (underlyingType == null) return false;

            if (_numericTypesSet.Contains(underlyingType))
                return true;

            return false;
        }
    }
}
