using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClosedXML.Excel;
using EduSafe.Core.BusinessLogic.Containers.TimeSeries;

namespace EduSafe.Core.Repositories.Excel
{
    public class ForecastedEnrollmentRepository : ValuesArrayExcelDataRepository<double>
    {
        private const string _enrollmentsTab = "ForecastedEnrollments";
        
        private IXLRangeRow _scenarioNames;

        public ForecastedEnrollmentRepository(string pathToExcelDataFile)
            : base(pathToExcelDataFile, _enrollmentsTab)
        { }

        public ForecastedEnrollmentRepository(Stream fileStream)
            : base(fileStream, _enrollmentsTab)
        { }

        /// <summary>
        /// Retrieves the enrollment projections for a forecast in a ForecastedEnrollmentsProjection object
        /// </summary>
        public ForecastedEnrollmentsProjection GetForecastedEnrollmentsProjection()
        {
            var enrollmentValuesArray = CreateValuesArray();
            var forecastedEnrollmentsProjection = new ForecastedEnrollmentsProjection();

            for (var columnNumber = 1; columnNumber < _MaxColumnCount; columnNumber++)
            {
                var scenarioName = _scenarioNames.Cell(columnNumber + 1).GetValue<string>();
                var enrollmentProjections = enrollmentValuesArray[columnNumber];

                if (!forecastedEnrollmentsProjection.EnrollmentCountProjections.ContainsKey(scenarioName))
                    forecastedEnrollmentsProjection.EnrollmentCountProjections.Add(scenarioName, enrollmentProjections);
                else
                {
                    var exceptiontext = 
                        string.Format("ERROR: Cannot add duplicate projection for enrollments projection scenario '{0}'", scenarioName);
                    throw new Exception(exceptiontext);
                }
            }

            return forecastedEnrollmentsProjection;
        }

        protected override void ProcessTabHeaders()
        {
            if (_ColumnNumbersList.Any()) _ColumnNumbersList.Clear();

            // This is the header row that will contain forecasting scenario names
            _scenarioNames = _ExcelDataRows.First();
            AddCountToColumnNumbersList(_scenarioNames);

            _MaxColumnCount = _ColumnNumbersList.Max();
            var exceptionText = "ERROR: Forecasted enrollments and data columns do not have matching records. " +
                    "Cannot load forecasting data.";
            CheckForHeadersNotMatchingException(exceptionText);
        }
    }
}
