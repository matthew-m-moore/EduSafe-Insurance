using System;
using System.Collections.Generic;
using System.Linq;
using ClosedXML.Excel;
using EduSafe.Common.Curves;
using EduSafe.Core.BusinessLogic.Containers.CompoundKeys;
using EduSafe.Core.BusinessLogic.Vectors;
using EduSafe.Core.Repositories.Excel.Converters;

namespace EduSafe.Core.Repositories.Excel
{
    public class VectorRepository : ExcelDataRepository
    {
        private const string _vectorsTab = "Vectors";

        private List<IXLRangeRow> _excelDataRows;
        private List<int> _columnNumbersList;

        private IXLRangeRow _vectorTypes;
        private IXLRangeRow _vectorStartStates;
        private IXLRangeRow _vectorEndStates;
        private IXLRangeRow _vectorSetNames;

        private int _maxColumnCount;

        public VectorRepository(string pathToExcelDataFile)
            : base(pathToExcelDataFile)
        {
            _excelDataRows = _ExcelFileReader.GetExcelDataRowsFromWorksheet(_vectorsTab);
            _columnNumbersList = new List<int>();
        }

        public Dictionary<VectorAssignmentEntry, Vector> GetVectorsDictionary()
        {
            ProcessTabHeaders();

            var vectorValuesArray = CreateVectorValuesArray();
            var vectorsDictionary = new Dictionary<VectorAssignmentEntry, Vector>();

            for (var columnNumber = 1; columnNumber < _maxColumnCount; columnNumber++)
            {
                var vectorSetName = _vectorSetNames.Cell(columnNumber + 1).GetValue<string>();
                var vectorStartState = _vectorStartStates.Cell(columnNumber + 1).GetValue<string>();
                var vectorEndState = _vectorStartStates.Cell(columnNumber + 1).GetValue<string>();
                var vectorType = _vectorTypes.Cell(columnNumber + 1).GetValue<string>();

                var enrollmentStartState = StudentEnrollmentStateConverter.ConvertStringToEnrollmentState(vectorStartState);
                var enrollmentEndState = StudentEnrollmentStateConverter.ConvertStringToEnrollmentState(vectorEndState);

                var vectorAssignment = new VectorAssignmentEntry(vectorSetName, enrollmentStartState, enrollmentEndState);
                var vectorValues = vectorValuesArray[columnNumber];
                var vector = VectorConverter.ConvertStringAndValuesToVector(vectorType, vectorValues);

                if (!vectorsDictionary.ContainsKey(vectorAssignment))
                    vectorsDictionary.Add(vectorAssignment, vector);
                else
                {
                    var exceptiontext = string.Format("ERROR: Cannot add duplicate vector in vector set '{0}' for " +
                        "start state, '{1}', and end state, '{2}'.", vectorSetName, vectorStartState, vectorEndState);
                    throw new Exception(exceptiontext);
                }
            }

            return vectorsDictionary;
        }

        private void ProcessTabHeaders()
        {
            // This is the header row that will contain vector types
            _vectorTypes = _excelDataRows.First();
            var vectorTypeColumns = _vectorTypes.CellCount();
            _columnNumbersList.Add(vectorTypeColumns);

            // This is the header row that will contain the enrollment staring state
            _vectorStartStates = _vectorTypes.RowBelow();
            var vectorStartStateColumns = _vectorStartStates.CellCount();
            _columnNumbersList.Add(vectorStartStateColumns);

            // This is the header row that will contain the enrollment ending state
            _vectorEndStates = _vectorStartStates.RowBelow();
            var vectorEndStateColumns = _vectorEndStates.CellCount();
            _columnNumbersList.Add(vectorEndStateColumns);

            // This is the header row that will contain the name of the vector set
            _vectorSetNames = _vectorEndStates.RowBelow();
            var vectorSetNameColumns = _vectorSetNames.CellCount();
            _columnNumbersList.Add(vectorSetNameColumns);

            _maxColumnCount = _columnNumbersList.Max();
            if (_columnNumbersList.Any(c => c != _maxColumnCount))
            {
                throw new Exception("ERROR: Vector set names, types, and/or states do not have matching records. " +
                    "Cannot load vectors.");
            }
        }

        private Dictionary<int, DataCurve<double>> CreateVectorValuesArray()
        {
            // This remaining block of data should contain the vector values
            // The first cell in each row should contain the integer period number
            var vectorValuesDataRows = _excelDataRows.Skip(_columnNumbersList.Count)
                .OrderBy(r => r.FirstCell().GetValue<int>())
                .ToList();

            var vectorValuesArray = new Dictionary<int, DataCurve<double>>();
            foreach (var vectorValues in vectorValuesDataRows)
            {
                // Note that column indexing in ClosedXML starts at unity, not zero
                var periodNumber = vectorValues.FirstCell().GetValue<int>();
                for (var columnNumber = 1; columnNumber < _maxColumnCount; columnNumber++)
                {
                    var vectorValue = vectorValues.Cell(columnNumber + 1).GetValue<double>();

                    if (!vectorValuesArray.ContainsKey(columnNumber))
                        vectorValuesArray.Add(columnNumber, new DataCurve<double>(vectorValue));
                    else
                        vectorValuesArray[columnNumber][periodNumber] = vectorValue;
                }
            }

            return vectorValuesArray;
        }
    }
}
