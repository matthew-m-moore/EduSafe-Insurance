using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EduSafe.Core.BusinessLogic.Aggregation;
using EduSafe.Core.BusinessLogic.Containers.TimeSeries;

namespace EduSafe.Core.Tests.BusinessLogic.Aggregation
{
    [TestClass]
    public class TimeSeriesAggregatorTests
    {
        [TestMethod, Owner("Matthew Moore")]
        public void TimeSeriesAggregator_AggregateTimeSeries_AddTwoCashFlowsIsSameAsScaleByTwo()
        {
            var listOfCashFlowsToAggregate = new List<List<StudentEnrollmentStateTimeSeriesEntry>> { _testListOne, _testListOne };
            var aggregatedTimeSeries = TimeSeriesAggregator.AggregateTimeSeries(listOfCashFlowsToAggregate);

            var copyOfTestListOne = _testListOne.Select(c => c.Copy() as StudentEnrollmentStateTimeSeriesEntry).ToList();
            copyOfTestListOne.ForEach(c => c.Scale(2.0));

            Assert.AreEqual(aggregatedTimeSeries[0].Period, copyOfTestListOne[0].Period);
            Assert.AreEqual(aggregatedTimeSeries[1].Period, copyOfTestListOne[1].Period);

            Assert.AreEqual(aggregatedTimeSeries[0].Enrolled, copyOfTestListOne[0].Enrolled);
            Assert.AreEqual(aggregatedTimeSeries[1].Enrolled, copyOfTestListOne[1].Enrolled);

            Assert.AreEqual(aggregatedTimeSeries[0].DeltaEnrolled, copyOfTestListOne[0].DeltaEnrolled);
            Assert.AreEqual(aggregatedTimeSeries[1].DeltaEnrolled, copyOfTestListOne[1].DeltaEnrolled);
        }

        [TestMethod, Owner("Matthew Moore")]
        public void TimeSeriesAggregator_AggregateTimeSeries_InBetweenPeriodIsProperlyInserted()
        {
            var listOfCashFlowsToAggregate = new List<List<StudentEnrollmentStateTimeSeriesEntry>> { _testListTwo, _testListThree };
            var aggregatedTimeSeries = TimeSeriesAggregator.AggregateTimeSeries(listOfCashFlowsToAggregate);

            Assert.AreEqual(aggregatedTimeSeries[0].Period, _testListTwo[0].Period);
            Assert.AreEqual(aggregatedTimeSeries[1].Period, _testListThree[0].Period);
            Assert.AreEqual(aggregatedTimeSeries[2].Period, _testListThree[1].Period);

            Assert.AreEqual(aggregatedTimeSeries[0].Enrolled, _testListTwo[0].Enrolled);
            Assert.AreEqual(aggregatedTimeSeries[1].Enrolled, _testListThree[0].Enrolled);
            Assert.AreEqual(aggregatedTimeSeries[2].Enrolled, _testListThree[1].Enrolled * 2.0);

            Assert.AreEqual(aggregatedTimeSeries[0].DeltaEnrolled, _testListTwo[0].DeltaEnrolled);
            Assert.AreEqual(aggregatedTimeSeries[1].DeltaEnrolled, _testListThree[0].DeltaEnrolled);
            Assert.AreEqual(aggregatedTimeSeries[2].DeltaEnrolled, _testListThree[1].DeltaEnrolled * 2.0);
        }

        [TestMethod, Owner("Matthew Moore")]
        public void TimeSeriesAggregator_AggregateTimeSeries_PriorPeriodIsProperlyInserted()
        {
            var listOfCashFlowsToAggregate = new List<List<StudentEnrollmentStateTimeSeriesEntry>> { _testListThree, _testListTwo };
            var aggregatedTimeSeries = TimeSeriesAggregator.AggregateTimeSeries(listOfCashFlowsToAggregate);

            Assert.AreEqual(aggregatedTimeSeries[0].Period, _testListTwo[0].Period);
            Assert.AreEqual(aggregatedTimeSeries[1].Period, _testListThree[0].Period);
            Assert.AreEqual(aggregatedTimeSeries[2].Period, _testListThree[1].Period);

            Assert.AreEqual(aggregatedTimeSeries[0].Enrolled, _testListTwo[0].Enrolled);
            Assert.AreEqual(aggregatedTimeSeries[1].Enrolled, _testListThree[0].Enrolled);
            Assert.AreEqual(aggregatedTimeSeries[2].Enrolled, _testListThree[1].Enrolled * 2.0);

            Assert.AreEqual(aggregatedTimeSeries[0].DeltaEnrolled, _testListTwo[0].DeltaEnrolled);
            Assert.AreEqual(aggregatedTimeSeries[1].DeltaEnrolled, _testListThree[0].DeltaEnrolled);
            Assert.AreEqual(aggregatedTimeSeries[2].DeltaEnrolled, _testListThree[1].DeltaEnrolled * 2.0);
        }

        private List<StudentEnrollmentStateTimeSeriesEntry> _testListOne = new List<StudentEnrollmentStateTimeSeriesEntry> { _testCashFlowOne, _testCashFlowTwo };
        private List<StudentEnrollmentStateTimeSeriesEntry> _testListTwo = new List<StudentEnrollmentStateTimeSeriesEntry> { _testCashFlowOne, _testCashFlowThree };
        private List<StudentEnrollmentStateTimeSeriesEntry> _testListThree = new List<StudentEnrollmentStateTimeSeriesEntry> { _testCashFlowTwo, _testCashFlowThree };

        private static StudentEnrollmentStateTimeSeriesEntry _testCashFlowOne =
            new StudentEnrollmentStateTimeSeriesEntry
            {
                Period = 1,

                Enrolled = 10,
                DeltaEnrolled = -2
            };

        private static StudentEnrollmentStateTimeSeriesEntry _testCashFlowTwo =
            new StudentEnrollmentStateTimeSeriesEntry
            {
                Period = 2,

                Enrolled = 20,
                DeltaEnrolled = -8
            };

        private static StudentEnrollmentStateTimeSeriesEntry _testCashFlowThree =
            new StudentEnrollmentStateTimeSeriesEntry
            {
                Period = 3,

                Enrolled = 10,
                DeltaEnrolled = -2
            };
    }
}
