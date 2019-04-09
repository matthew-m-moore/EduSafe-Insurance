using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ClosedXML.Excel;
using EduSafe.Core.BusinessLogic.Containers.TimeSeries;

namespace EduSafe.Core.Repositories.Excel
{
    public class ForecastedFirstYearPercentageRepository : ValuesArrayExcelDataRepository<double>
    {
        private const string _firstYearPercentageTab = "ForecastedFirstYearPercent";

        private IXLRangeRow _scenarioNames;

        public ForecastedFirstYearPercentageRepository(string pathToExcelDataFile)
            : base(pathToExcelDataFile, _firstYearPercentageTab)
        { }

        public ForecastedFirstYearPercentageRepository(Stream fileStream)
            : base(fileStream, _firstYearPercentageTab)
        { }

        /// <summary>
        /// Assumes that the first value for each scenario will be used as a global projection of first year enrollee percentage.
        /// Returns a dictionary of these values.
        /// </summary>
        public Dictionary<string, double> RetrieveGlobalPercentageFirstYearEnrolleeProjections()
        {
            var enrollmentValuesArray = CreateValuesArray();
            var globalFirstYearPercentageProjections = new Dictionary<string, double>();

            for (var columnNumber = 1; columnNumber < _MaxColumnCount; columnNumber++)
            {
                var scenarioName = _scenarioNames.Cell(columnNumber + 1).GetValue<string>();
                var firstYearPercentageProjection = enrollmentValuesArray[columnNumber].First();

                if (!globalFirstYearPercentageProjections.ContainsKey(scenarioName))
                    globalFirstYearPercentageProjections.Add(scenarioName, firstYearPercentageProjection);
                else
                {
                    var exceptiontext =
                        string.Format("ERROR: Cannot add duplicate projection for first year percentage projection scenario '{0}'", scenarioName);
                    throw new Exception(exceptiontext);
                }
            }

            return globalFirstYearPercentageProjections;
        }

        /// <summary>
        /// Populates the percentage of first year enrollees in the provided forecasted enrollments projection object.
        /// </summary>
        public void PopulatePercentageFirstYearEnrolleeProjections(ForecastedEnrollmentsProjection forecastedEnrollmentsProjection)
        {
            var enrollmentValuesArray = CreateValuesArray();

            for (var columnNumber = 1; columnNumber < _MaxColumnCount; columnNumber++)
            {
                var scenarioName = _scenarioNames.Cell(columnNumber + 1).GetValue<string>();
                var firstYearPercentageProjections = enrollmentValuesArray[columnNumber];

                if (!forecastedEnrollmentsProjection.PercentageFirstYearEnrolleeProjections.ContainsKey(scenarioName))
                    forecastedEnrollmentsProjection.PercentageFirstYearEnrolleeProjections.Add(scenarioName, firstYearPercentageProjections);
                else
                {
                    var exceptiontext =
                        string.Format("ERROR: Cannot add duplicate projection for first year percentage projection scenario '{0}'", scenarioName);
                    throw new Exception(exceptiontext);
                }
            }
        }

        protected override void ProcessTabHeaders()
        {
            if (_ColumnNumbersList.Any()) _ColumnNumbersList.Clear();

            // This is the header row that will contain forecasting scenario names
            _scenarioNames = _ExcelDataRows.First();
            AddCountToColumnNumbersList(_scenarioNames);

            _MaxColumnCount = _ColumnNumbersList.Max();
            var exceptionText = "ERROR: Forecasted first-year percentages and data columns do not have matching records. " +
                    "Cannot load forecasting data.";
            CheckForHeadersNotMatchingException(exceptionText);
        }
    }
}
