using System;
using System.Collections.Generic;
using System.Linq;
using EduSafe.Core.BusinessLogic.Containers;
using EduSafe.Core.BusinessLogic.Investments;

namespace EduSafe.Core.BusinessLogic.Models.Investments
{
    public class ReinvestmentModel
    {
        private PremiumComputationResult _premiumComputationResult;
        private ReinvestmentOptionsParameters _reinvestmentOptionsParameters;

        public List<ReinvestmentModelResultEntry> ReinvestmentModelCashFlows { get; private set; }

        public ReinvestmentModel(PremiumComputationResult premiumComputationResult, ReinvestmentOptionsParameters reinvestmentOptionsParameters)
        {
            _premiumComputationResult = premiumComputationResult;
            _reinvestmentOptionsParameters = reinvestmentOptionsParameters;
        }

        public ReinvestmentModelProfitLossResult GetReinvestmentModelProfitLoss()
        {
            var finalCashFlow = ReinvestmentModelCashFlows.Select(x => x.EndingCashFlow).Last();

            var profitFrom3M = ReinvestmentModelCashFlows.Sum(x => x.PortionInThreeMonth) -
                ReinvestmentModelCashFlows.Sum(x => x.CashFlowReturningFromThreeMonth);

            var profitFrom6M = ReinvestmentModelCashFlows.Sum(x => x.PortionInSixMonth) -
                ReinvestmentModelCashFlows.Sum(x => x.CashFlowReturningFromSixMonth);

            var profitFrom1Y = ReinvestmentModelCashFlows.Sum(x => x.PortionInOneYear) -
                ReinvestmentModelCashFlows.Sum(x => x.CashFlowReturningFromOneYear);

            var totalProfitLoss = finalCashFlow + profitFrom3M + profitFrom6M + profitFrom1Y;

            return new ReinvestmentModelProfitLossResult
            {
                FinalCashFlow = finalCashFlow,
                ProfitLossFrom3M = profitFrom3M,
                ProfitLossFrom6M = profitFrom6M,
                ProfitLossFrom1Y = profitFrom1Y,
                TotalProfitLoss = totalProfitLoss
            };
        }

        private void ComputeReinvestmentModelCashFlows()
        {
            var listOfReinvestmentModelResultEntries = new List<ReinvestmentModelResultEntry>();

            foreach (var cashflow in _premiumComputationResult.PremiumCalculationCashFlows)
            {
                var reinvestmentModelResultEntry = new ReinvestmentModelResultEntry();
                var period = cashflow.Period;

                var RateOneMonth = _reinvestmentOptionsParameters.OneMonthRate;
                var RateThreeMonth = _reinvestmentOptionsParameters.ThreeMonthRate;
                var RateSixMonth = _reinvestmentOptionsParameters.SixMonthRate;
                var RateOneYear = _reinvestmentOptionsParameters.TwelveMonthRate;

                var PortionCash = _reinvestmentOptionsParameters.PortionInCash;
                var Portion1M = _reinvestmentOptionsParameters.PortionIn1M;
                var Portion3M = _reinvestmentOptionsParameters.PortionIn3M;
                var Portion6M = _reinvestmentOptionsParameters.PortionIn6M;
                var Portion1Y = _reinvestmentOptionsParameters.PortionIn12M;

                reinvestmentModelResultEntry.Period = period;

                var begBalance = period == 0 ? cashflow.PremiumAvailableForReinvestment : cashflow.PremiumAvailableForReinvestment + listOfReinvestmentModelResultEntries[period - 1].EndingCashFlow;

                reinvestmentModelResultEntry.BeginningCashFlow = begBalance;

                var portionCash = begBalance * PortionCash;
                var portion1M = begBalance * Portion1M;
                var portion3M = begBalance * Portion3M;
                var portion6M = begBalance * Portion6M;
                var portion1Y = begBalance * Portion1Y;

                reinvestmentModelResultEntry.PortionInCash = portionCash;
                reinvestmentModelResultEntry.PortionInOneMonth = portion1M;
                reinvestmentModelResultEntry.PortionInThreeMonth = portion3M;
                reinvestmentModelResultEntry.PortionInSixMonth = portion6M;
                reinvestmentModelResultEntry.PortionInOneYear = portion1Y;

                var interest1M = portion1M * RateOneMonth / 1200;
                var interest3M = portion3M * (Math.Pow((1 + RateThreeMonth / 1200), 3) - 1);
                var interest6M = portion6M * (Math.Pow((1 + RateSixMonth / 1200), 6) - 1);
                var interest1Y = portion1Y * (Math.Pow((1 + RateOneYear / 1200), 12) - 1);

                reinvestmentModelResultEntry.InterestFromOneMonth = interest1M;
                reinvestmentModelResultEntry.InterestFromThreeMonth = interest3M;
                reinvestmentModelResultEntry.InterestFromSixMonth = interest6M;
                reinvestmentModelResultEntry.InterestFromOneYear = interest1Y;

                var cashFlowReturningFromThreeMonth = period < 3 ? 0 : listOfReinvestmentModelResultEntries[period - 2].PortionInThreeMonth;

                var cashFlowReturningFromSixMonth = period < 6 ? 0 : listOfReinvestmentModelResultEntries[period - 5].PortionInSixMonth;

                var cashFlowReturningFromOneYear = period < 12 ? 0 : listOfReinvestmentModelResultEntries[period - 11].PortionInOneYear;

                reinvestmentModelResultEntry.CashFlowReturningFromThreeMonth = cashFlowReturningFromThreeMonth;
                reinvestmentModelResultEntry.CashFlowReturningFromSixMonth = cashFlowReturningFromSixMonth;
                reinvestmentModelResultEntry.CashFlowReturningFromOneYear = cashFlowReturningFromOneYear;

                var totalReturningCash =
                    portionCash +
                    portion1M +
                    cashFlowReturningFromThreeMonth +
                    cashFlowReturningFromSixMonth +
                    cashFlowReturningFromOneYear;

                var totalInterestEarned = interest1M + interest3M + interest6M + interest1Y;

                reinvestmentModelResultEntry.EndingCashFlow = period == 0 ? 0 : totalReturningCash + totalInterestEarned;

                listOfReinvestmentModelResultEntries.Add(reinvestmentModelResultEntry);
            }

            ReinvestmentModelCashFlows = listOfReinvestmentModelResultEntries;
        }
    }
}
