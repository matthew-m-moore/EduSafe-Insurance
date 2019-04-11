using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EduSafe.Core.BusinessLogic.Aggregation;
using EduSafe.Core.BusinessLogic.Containers.CashFlows;

namespace EduSafe.Core.Tests.BusinessLogic.Aggregation
{
    [TestClass]
    public class CashFlowAggregatorTests
    {
        [TestMethod, Owner("Matthew Moore")]
        public void CashFlowAggregator_AggregateCashFlows_AddTwoCashFlowsIsSameAsScaleByTwo()
        {
            var listOfCashFlowsToAggregate = new List<List<PremiumCalculationCashFlow>> { _testListOne, _testListOne };
            var aggregatedCashFlows = CashFlowAggregator.AggregateCashFlows(listOfCashFlowsToAggregate);

            var copyOfTestListOne = _testListOne.Select(c => c.Copy() as PremiumCalculationCashFlow).ToList();
            copyOfTestListOne.ForEach(c => c.Scale(2.0));

            Assert.AreEqual(aggregatedCashFlows[0].Period, copyOfTestListOne[0].Period);
            Assert.AreEqual(aggregatedCashFlows[1].Period, copyOfTestListOne[1].Period);

            Assert.AreEqual(aggregatedCashFlows[0].DiscountFactor, copyOfTestListOne[0].DiscountFactor);
            Assert.AreEqual(aggregatedCashFlows[1].DiscountFactor, copyOfTestListOne[1].DiscountFactor);

            Assert.AreEqual(aggregatedCashFlows[0].Premium, copyOfTestListOne[0].Premium);
            Assert.AreEqual(aggregatedCashFlows[1].Premium, copyOfTestListOne[1].Premium);

            Assert.AreEqual(aggregatedCashFlows[0].ProbabilityAdjustedPremium, copyOfTestListOne[0].ProbabilityAdjustedPremium);
            Assert.AreEqual(aggregatedCashFlows[1].ProbabilityAdjustedPremium, copyOfTestListOne[1].ProbabilityAdjustedPremium);
        }

        [TestMethod, Owner("Matthew Moore")]
        public void CashFlowAggregator_AggregateCashFlows_InBetweenPeriodIsProperlyInserted()
        {
            var listOfCashFlowsToAggregate = new List<List<PremiumCalculationCashFlow>> { _testListTwo, _testListThree };
            var aggregatedCashFlows = CashFlowAggregator.AggregateCashFlows(listOfCashFlowsToAggregate);

            Assert.AreEqual(aggregatedCashFlows[0].Period, _testListTwo[0].Period);
            Assert.AreEqual(aggregatedCashFlows[1].Period, _testListThree[0].Period);
            Assert.AreEqual(aggregatedCashFlows[2].Period, _testListThree[1].Period);

            Assert.AreEqual(aggregatedCashFlows[0].DiscountFactor, _testListTwo[0].DiscountFactor);
            Assert.AreEqual(aggregatedCashFlows[1].DiscountFactor, _testListThree[0].DiscountFactor);
            Assert.AreEqual(aggregatedCashFlows[2].DiscountFactor, _testListThree[1].DiscountFactor);

            Assert.AreEqual(aggregatedCashFlows[0].Premium, _testListTwo[0].Premium);
            Assert.AreEqual(aggregatedCashFlows[1].Premium, _testListThree[0].Premium);
            Assert.AreEqual(aggregatedCashFlows[2].Premium, _testListThree[1].Premium * 2.0);

            Assert.AreEqual(aggregatedCashFlows[0].ProbabilityAdjustedPremium, _testListTwo[0].ProbabilityAdjustedPremium);
            Assert.AreEqual(aggregatedCashFlows[1].ProbabilityAdjustedPremium, _testListThree[0].ProbabilityAdjustedPremium);
            Assert.AreEqual(aggregatedCashFlows[2].ProbabilityAdjustedPremium, _testListThree[1].ProbabilityAdjustedPremium * 2.0);
        }

        [TestMethod, Owner("Matthew Moore")]
        public void CashFlowAggregator_AggregateCashFlows_PriorPeriodIsProperlyInserted()
        {
            var listOfCashFlowsToAggregate = new List<List<PremiumCalculationCashFlow>> { _testListThree, _testListTwo };
            var aggregatedCashFlows = CashFlowAggregator.AggregateCashFlows(listOfCashFlowsToAggregate);

            Assert.AreEqual(aggregatedCashFlows[0].Period, _testListTwo[0].Period);
            Assert.AreEqual(aggregatedCashFlows[1].Period, _testListThree[0].Period);
            Assert.AreEqual(aggregatedCashFlows[2].Period, _testListThree[1].Period);

            Assert.AreEqual(aggregatedCashFlows[0].DiscountFactor, _testListTwo[0].DiscountFactor);
            Assert.AreEqual(aggregatedCashFlows[1].DiscountFactor, _testListThree[0].DiscountFactor);
            Assert.AreEqual(aggregatedCashFlows[2].DiscountFactor, _testListThree[1].DiscountFactor);

            Assert.AreEqual(aggregatedCashFlows[0].Premium, _testListTwo[0].Premium);
            Assert.AreEqual(aggregatedCashFlows[1].Premium, _testListThree[0].Premium);
            Assert.AreEqual(aggregatedCashFlows[2].Premium, _testListThree[1].Premium * 2.0);

            Assert.AreEqual(aggregatedCashFlows[0].ProbabilityAdjustedPremium, _testListTwo[0].ProbabilityAdjustedPremium);
            Assert.AreEqual(aggregatedCashFlows[1].ProbabilityAdjustedPremium, _testListThree[0].ProbabilityAdjustedPremium);
            Assert.AreEqual(aggregatedCashFlows[2].ProbabilityAdjustedPremium, _testListThree[1].ProbabilityAdjustedPremium * 2.0);
        }

        private List<PremiumCalculationCashFlow> _testListOne = new List<PremiumCalculationCashFlow> { _testCashFlowOne, _testCashFlowTwo };
        private List<PremiumCalculationCashFlow> _testListTwo = new List<PremiumCalculationCashFlow> { _testCashFlowOne, _testCashFlowThree };
        private List<PremiumCalculationCashFlow> _testListThree = new List<PremiumCalculationCashFlow> { _testCashFlowTwo, _testCashFlowThree };

        private static PremiumCalculationCashFlow _testCashFlowOne =
            new PremiumCalculationCashFlow
            {
                Period = 1,
                DiscountFactor = 0.99,

                Premium = 100,
                ProbabilityAdjustedPremium = 90
            };

        private static PremiumCalculationCashFlow _testCashFlowTwo =
            new PremiumCalculationCashFlow
            {
                Period = 2,
                DiscountFactor = 0.98,

                Premium = 150,
                ProbabilityAdjustedPremium = 100
            };

        private static PremiumCalculationCashFlow _testCashFlowThree =
            new PremiumCalculationCashFlow
            {
                Period = 3,
                DiscountFactor = 0.97,

                Premium = 100,
                ProbabilityAdjustedPremium = 90
            };
    }
}
