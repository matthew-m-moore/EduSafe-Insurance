using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ClosedXML.Excel;
using EduSafe.Core.BusinessLogic.Containers.CompoundKeys;
using EduSafe.Core.BusinessLogic.Vectors;
using EduSafe.Core.Repositories.Excel.Converters;

namespace EduSafe.Core.Repositories.Excel
{
    public class VectorRepository : ValuesArrayExcelDataRepository<double>
    {
        private const string _vectorsTab = "Vectors";

        private IXLRangeRow _vectorTypes;
        private IXLRangeRow _vectorStartStates;
        private IXLRangeRow _vectorEndStates;
        private IXLRangeRow _vectorSetNames;

        public VectorRepository(string pathToExcelDataFile)
            : base(pathToExcelDataFile, _vectorsTab)
        { }

        public VectorRepository(Stream fileStream)
            : base(fileStream, _vectorsTab)
        { }

        public Dictionary<VectorAssignmentEntry, Vector> GetVectorsDictionary()
        {
            var vectorValuesArray = CreateValuesArray();
            var vectorsDictionary = new Dictionary<VectorAssignmentEntry, Vector>();

            for (var columnNumber = 1; columnNumber < _MaxColumnCount; columnNumber++)
            {
                var vectorSetName = _vectorSetNames.Cell(columnNumber + 1).GetValue<string>();
                var vectorStartState = _vectorStartStates.Cell(columnNumber + 1).GetValue<string>();
                var vectorEndState = _vectorEndStates.Cell(columnNumber + 1).GetValue<string>();
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

        protected override void ProcessTabHeaders()
        {
            if (_ColumnNumbersList.Any()) _ColumnNumbersList.Clear();
             
            // This is the header row that will contain vector types
            _vectorTypes = _ExcelDataRows.First();
            AddCountToColumnNumbersList(_vectorTypes);

            // This is the header row that will contain the enrollment staring state
            _vectorStartStates = _vectorTypes.RowBelow();
            AddCountToColumnNumbersList(_vectorStartStates);

            // This is the header row that will contain the enrollment ending state
            _vectorEndStates = _vectorStartStates.RowBelow();
            AddCountToColumnNumbersList(_vectorEndStates);

            // This is the header row that will contain the name of the vector set
            _vectorSetNames = _vectorEndStates.RowBelow();
            AddCountToColumnNumbersList(_vectorSetNames);

            _MaxColumnCount = _ColumnNumbersList.Max();
            var exceptionText = "ERROR: Vector set names, types, and/or states do not have matching records. " +
                    "Cannot load vectors.";
            CheckForHeadersNotMatchingException(exceptionText);
        }
    }
}
