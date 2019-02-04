using System;
using System.Collections.Generic;
using System.Linq;
using ClosedXML.Excel;
using EduSafe.Common.Curves;
using EduSafe.Common.Enums;
using EduSafe.Core.Repositories.Excel.Converters;

namespace EduSafe.Core.Repositories.Excel
{
    public class InterestRateCurveRepository : ValuesArrayExcelDataRepository<double>
    {
        private const string _discountRatesTab = "DiscountRates";

        private List<IXLRangeRow> _excelDataRows;
        private List<int> _columnNumbersList;

        private IXLRangeRow _interestRateCurveTypes;
        private IXLRangeRow _interestRateCurveDates;
        private IXLRangeRow _interestRateCurveSetNames;

        private int _maxColumnCount;

        public InterestRateCurveRepository(string pathToExcelDataFile)
            : base(pathToExcelDataFile, _discountRatesTab)
        {
            _excelDataRows = _ExcelFileReader.GetExcelDataRowsFromWorksheet(_discountRatesTab);
            _columnNumbersList = new List<int>();
        }

        public Dictionary<string, Dictionary<InterestRateCurveType, InterestRateCurve>> GetInterestRateCurveSetDictionary()
        {
            var rateCurveValuesArray = CreateValuesArray();
            var rateCurvesDictionary = new Dictionary<string, Dictionary<InterestRateCurveType, InterestRateCurve>>();

            for (var columnNumber = 1; columnNumber < _MaxColumnCount; columnNumber++)
            {
                var interestRateCurveSetName = _interestRateCurveSetNames.Cell(columnNumber + 1).GetValue<string>();
                var interestRateCurveDate = _interestRateCurveDates.Cell(columnNumber + 1).GetValue<DateTime>();
                var interestRateCurveTypeText = _interestRateCurveTypes.Cell(columnNumber + 1).GetValue<string>();

                var interestRateCurveType = InterestRateCurveTypeConverter.ConvertStringToInterestRateCurveType(interestRateCurveTypeText);
                var interestRateCurveValues = rateCurveValuesArray[columnNumber];

                var interestRateCurve = InterestRateCurveConverter.ConvertInputsToInterestRateCurve(
                    interestRateCurveType,
                    interestRateCurveDate,
                    interestRateCurveValues);

                if (!rateCurvesDictionary.ContainsKey(interestRateCurveSetName))
                    rateCurvesDictionary.Add(interestRateCurveSetName, new Dictionary<InterestRateCurveType, InterestRateCurve>());

                if (!rateCurvesDictionary[interestRateCurveSetName].ContainsKey(interestRateCurveType))
                    rateCurvesDictionary[interestRateCurveSetName].Add(interestRateCurveType, interestRateCurve);
                else
                {
                    var exceptiontext = string.Format("ERROR: Cannot add duplicate curve type in rate curve set '{0}' for " +
                        "curve type, '{1}'.", interestRateCurveSetName, interestRateCurveType);
                    throw new Exception(exceptiontext);
                }
            }

            return rateCurvesDictionary;
        }

        protected override void ProcessTabHeaders()
        {
            if (_ColumnNumbersList.Any()) _ColumnNumbersList.Clear();

            // This is the header row that will contain interest rate curve types
            _interestRateCurveTypes = _ExcelDataRows.First();
            AddCountToColumnNumbersList(_interestRateCurveTypes);

            // This is the header row that will contain the starting dates for each curve
            _interestRateCurveDates = _interestRateCurveTypes.RowBelow();
            AddCountToColumnNumbersList(_interestRateCurveDates);

            // This is the header row that will contain the curve set names
            _interestRateCurveSetNames = _interestRateCurveDates.RowBelow();
            AddCountToColumnNumbersList(_interestRateCurveSetNames);

            _MaxColumnCount = _ColumnNumbersList.Max();
            var exceptionText = "ERROR: Interest rate curve set names, types, and/or dates do not have matching records. " +
                    "Cannot load interest rate curves.";
            CheckForHeadersNotMatchingException(exceptionText);
        }
    }
}
