using System;
using System.Collections.Generic;
using System.Linq;
using ClosedXML.Excel;
using EduSafe.Common.Curves;

namespace EduSafe.Core.Repositories.Excel
{
    /// <summary>
    /// Abstract representation of an Excel tab with multiple columns data specified by multiple header rows.
    /// The class is designed such that ProcessTabHeaders() must always be run first.
    /// </summary>
    public abstract class ValuesArrayExcelDataRepository<T> : ExcelDataRepository  
    {
        protected List<IXLRangeRow> _ExcelDataRows;
        protected List<int> _ColumnNumbersList;

        protected int _MaxColumnCount;

        public ValuesArrayExcelDataRepository(string pathToExcelDataFile, string tabName)
            : base(pathToExcelDataFile)
        {
            _ExcelDataRows = _ExcelFileReader.GetExcelDataRowsFromWorksheet(tabName);
            _ColumnNumbersList = new List<int>();
        }

        protected abstract void ProcessTabHeaders();

        protected void AddCountToColumnNumbersList(IXLRangeRow excelDataRow)
        {
            var excelDataRowColumns = excelDataRow.CellCount();
            _ColumnNumbersList.Add(excelDataRowColumns);
        }

        protected void CheckForHeadersNotMatchingException(string exceptionText)
        {
            if (_ColumnNumbersList.Any(c => c != _MaxColumnCount))
            {
                throw new Exception(exceptionText);
            }
        }

        /// <summary>
        /// Creates a values array of type T, assuming data is list in columns across the Excel tab spreadsheet.
        /// </summary>
        protected Dictionary<int, DataCurve<T>> CreateValuesArray()
        {
            ProcessTabHeaders();

            // This remaining block of data should contain the vector values
            // The first cell in each row should contain the integer period number
            var valuesDataRows = _ExcelDataRows.Skip(_ColumnNumbersList.Count)
                .OrderBy(r => r.FirstCell().GetValue<int>())
                .ToList();

            var valuesArray = new Dictionary<int, DataCurve<T>>();
            foreach (var valuesDataRow in valuesDataRows)
            {
                // Note that column indexing in ClosedXML starts at unity, not zero
                var periodNumber = valuesDataRow.FirstCell().GetValue<int>();
                for (var columnNumber = 1; columnNumber < _MaxColumnCount; columnNumber++)
                {
                    var value = valuesDataRow.Cell(columnNumber + 1).GetValue<T>();

                    if (!valuesArray.ContainsKey(columnNumber))
                        valuesArray.Add(columnNumber, new DataCurve<T>(value));
                    else
                        valuesArray[columnNumber][periodNumber] = value;
                }
            }

            return valuesArray;
        }
    }
}
