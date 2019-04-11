using System.Collections.Generic;
using System.Data;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EduSafe.Common.ExtensionMethods;
using EduSafe.Core.BusinessLogic.Aggregation;
using EduSafe.Common;

namespace EduSafe.Core.Tests.BusinessLogic.Aggregation
{
    [TestClass]
    public class DataTableAggregatorTests
    {
        [TestMethod, Owner("Matthew Moore")]
        public void DataTableAggregator_AggregateDataTables_AddTwoCashFlowsIsSameAsScaleByTwo()
        {
            Initialize();
            var listOfDataTablesToAggregate = new List<DataTable> { _testTableOne, _testTableOne };
            var aggregatedDataTable = DataTableAggregator.AggregateDataTables(listOfDataTablesToAggregate);

            var collectionOfDataRows = _testTableOne.AsEnumerable();
            var copyOfTestTableOne = _testTableOne.Clone();
            foreach (var dataRow in collectionOfDataRows) copyOfTestTableOne.ImportRow(dataRow);
            foreach (var dataRow in copyOfTestTableOne.AsEnumerable()) dataRow.Scale(2.0);

            Assert.AreEqual(
                aggregatedDataTable.Rows[0].Field<double>(Constants.PeriodIdentifier), 
                copyOfTestTableOne.Rows[0].Field<double>(Constants.PeriodIdentifier));

            Assert.AreEqual(
                aggregatedDataTable.Rows[1].Field<double>(Constants.PeriodIdentifier),
                copyOfTestTableOne.Rows[1].Field<double>(Constants.PeriodIdentifier));

            Assert.AreEqual(aggregatedDataTable.Rows[0].Field<double>(_dataHeader), copyOfTestTableOne.Rows[0].Field<double>(_dataHeader));
            Assert.AreEqual(aggregatedDataTable.Rows[1].Field<double>(_dataHeader), copyOfTestTableOne.Rows[1].Field<double>(_dataHeader));
        }

        [TestMethod, Owner("Matthew Moore")]
        public void DataTableAggregator_AggregateDataTables_InBetweenPeriodIsProperlyInserted()
        {
            Initialize();
            var listOfDataTablesToAggregate = new List<DataTable> { _testTableTwo, _testTableThree };
            var aggregatedDataTable = DataTableAggregator.AggregateDataTables(listOfDataTablesToAggregate);

            Assert.AreEqual(
                aggregatedDataTable.Rows[0].Field<double>(Constants.PeriodIdentifier), 
                _testTableTwo.Rows[0].Field<double>(Constants.PeriodIdentifier));

            Assert.AreEqual(
                aggregatedDataTable.Rows[1].Field<double>(Constants.PeriodIdentifier), 
                _testTableThree.Rows[0].Field<double>(Constants.PeriodIdentifier));

            Assert.AreEqual(
                aggregatedDataTable.Rows[2].Field<double>(Constants.PeriodIdentifier), 
                _testTableThree.Rows[1].Field<double>(Constants.PeriodIdentifier));

            Assert.AreEqual(aggregatedDataTable.Rows[0].Field<double>(_dataHeader), _testTableTwo.Rows[0].Field<double>(_dataHeader));
            Assert.AreEqual(aggregatedDataTable.Rows[1].Field<double>(_dataHeader), _testTableThree.Rows[0].Field<double>(_dataHeader));
            Assert.AreEqual(aggregatedDataTable.Rows[2].Field<double>(_dataHeader), _testTableThree.Rows[1].Field<double>(_dataHeader) * 2.0);
        }

        [TestMethod, Owner("Matthew Moore")]
        public void DataTableAggregator_AggregateDataTables_PriorPeriodIsProperlyInserted()
        {
            Initialize();
            var listOfDataTablesToAggregate = new List<DataTable> { _testTableThree, _testTableTwo };
            var aggregatedDataTable = DataTableAggregator.AggregateDataTables(listOfDataTablesToAggregate);

            Assert.AreEqual(
                aggregatedDataTable.Rows[0].Field<double>(Constants.PeriodIdentifier),
                _testTableTwo.Rows[0].Field<double>(Constants.PeriodIdentifier));

            Assert.AreEqual(
                aggregatedDataTable.Rows[1].Field<double>(Constants.PeriodIdentifier),
                _testTableThree.Rows[0].Field<double>(Constants.PeriodIdentifier));

            Assert.AreEqual(
                aggregatedDataTable.Rows[2].Field<double>(Constants.PeriodIdentifier),
                _testTableThree.Rows[1].Field<double>(Constants.PeriodIdentifier));

            Assert.AreEqual(aggregatedDataTable.Rows[0].Field<double>(_dataHeader), _testTableTwo.Rows[0].Field<double>(_dataHeader));
            Assert.AreEqual(aggregatedDataTable.Rows[1].Field<double>(_dataHeader), _testTableThree.Rows[0].Field<double>(_dataHeader));
            Assert.AreEqual(aggregatedDataTable.Rows[2].Field<double>(_dataHeader), _testTableThree.Rows[1].Field<double>(_dataHeader) * 2.0);
        }

        private const string _dataHeader = "Data";

        private DataTable _testTableOne = new DataTable();
        private DataTable _testTableTwo = new DataTable();
        private DataTable _testTableThree = new DataTable();

        private void Initialize()
        {
            _testTableOne.Columns.Add(Constants.PeriodIdentifier, typeof(double));
            _testTableOne.Columns.Add(_dataHeader, typeof(double));

            _testTableTwo.Columns.Add(Constants.PeriodIdentifier, typeof(double));
            _testTableTwo.Columns.Add(_dataHeader, typeof(double));

            _testTableThree.Columns.Add(Constants.PeriodIdentifier, typeof(double));
            _testTableThree.Columns.Add(_dataHeader, typeof(double));

            var testTableOneRowOne = _testTableOne.NewRow();
            var testTableOneRowTwo = _testTableOne.NewRow();

            var testTableTwoRowOne = _testTableTwo.NewRow();
            var testTableTwoRowTwo = _testTableTwo.NewRow();

            var testTableThreeRowOne = _testTableThree.NewRow();
            var testTableThreeRowTwo = _testTableThree.NewRow();

            testTableOneRowOne[Constants.PeriodIdentifier] = 1;
            testTableOneRowTwo[Constants.PeriodIdentifier] = 2;

            testTableTwoRowOne[Constants.PeriodIdentifier] = 1;
            testTableTwoRowTwo[Constants.PeriodIdentifier] = 3;

            testTableThreeRowOne[Constants.PeriodIdentifier] = 2;
            testTableThreeRowTwo[Constants.PeriodIdentifier] = 3;

            testTableOneRowOne[_dataHeader] = 13.5;
            testTableOneRowTwo[_dataHeader] = 42.3;

            testTableTwoRowOne[_dataHeader] = 13.5;
            testTableTwoRowTwo[_dataHeader] = 7.3;

            testTableThreeRowOne[_dataHeader] = 42.3;
            testTableThreeRowTwo[_dataHeader] = 7.3;

            _testTableOne.Rows.Add(testTableOneRowOne);
            _testTableOne.Rows.Add(testTableOneRowTwo);

            _testTableTwo.Rows.Add(testTableTwoRowOne);
            _testTableTwo.Rows.Add(testTableTwoRowTwo);

            _testTableThree.Rows.Add(testTableThreeRowOne);
            _testTableThree.Rows.Add(testTableThreeRowTwo);
        }
    }
}
