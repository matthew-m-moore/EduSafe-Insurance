using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EduSafe.Core.BusinessLogic.Containers;

namespace EduSafe.Core.BusinessLogic.Models
{
    public class ReinvestmentModel
    {
        public PremiumComputationResult PremiumComputationResult;
        public ReinvestmentOptionsParameters ReinvestmentOptionsParameters;

        public ReinvestmentModel(PremiumComputationResult premiumComputationResult, ReinvestmentOptionsParameters reinvestmentOptionsParameters)
        {
            PremiumComputationResult = premiumComputationResult;
            ReinvestmentOptionsParameters = reinvestmentOptionsParameters;
        }

        public List<ReinvestmentModelResults> ReinvestmentModelCashFlows()
        {
            var outputList = new List<ReinvestmentModelResults>();

            foreach (var cashflow in PremiumComputationResult.PremiumCalculationCashFlows)
            {
                var output = new ReinvestmentModelResults();
                var period = cashflow.Period;

                var RateOneMonth = ReinvestmentOptionsParameters.OneMonthRate;
                var RateThreeMonth = ReinvestmentOptionsParameters.ThreeMonthRate;
                var RateSixMonth = ReinvestmentOptionsParameters.SixMonthRate;
                var RateOneYear = ReinvestmentOptionsParameters.TwelveMonthRate;

                var PortionCash = ReinvestmentOptionsParameters.PortionInCash;
                var Portion1M = ReinvestmentOptionsParameters.PortionIn1M;
                var Portion3M = ReinvestmentOptionsParameters.PortionIn3M;
                var Portion6M = ReinvestmentOptionsParameters.PortionIn6M;
                var Portion1Y = ReinvestmentOptionsParameters.PortionIn12M;

                output.Period = period;

                var begBalance = period == 0 ? cashflow.PremiumAvailableForReinvestment : cashflow.PremiumAvailableForReinvestment + outputList[period - 1].EndingCashFlow;

                output.BeginningCashFlow = begBalance;

                var portionCash = begBalance * PortionCash;
                var portion1M = begBalance * Portion1M;
                var portion3M = begBalance * Portion3M;
                var portion6M = begBalance * Portion6M;
                var portion1Y = begBalance * Portion1Y;

                output.PortionInCash = portionCash;
                output.PortionInOneMonth = portion1M;
                output.PortionInThreeMonth = portion3M;
                output.PortionInSixMonth = portion6M;
                output.PortionInOneYear = portion1Y;

                var interest1M = portion1M * RateOneMonth / 1200;
                var interest3M = portion3M * (Math.Pow((1 + RateThreeMonth / 1200), 3) - 1);
                var interest6M = portion6M * (Math.Pow((1 + RateSixMonth / 1200), 6) - 1);
                var interest1Y = portion1Y * (Math.Pow((1 + RateOneYear / 1200), 12) - 1);

                output.InterestFromOneMonth = interest1M;
                output.InterestFromThreeMonth = interest3M;
                output.InterestFromSixMonth = interest6M;
                output.InterestFromOneYear = interest1Y;

                var cashFlowReturningFromThreeMonth = period < 3 ? 0 : outputList[period - 2].PortionInThreeMonth;

                var cashFlowReturningFromSixMonth = period < 6 ? 0 : outputList[period - 5].PortionInSixMonth;

                var cashFlowReturningFromOneYear = period < 12 ? 0 : outputList[period - 11].PortionInOneYear;

                output.CashFlowReturningFromThreeMonth = cashFlowReturningFromThreeMonth;
                output.CashFlowReturningFromSixMonth = cashFlowReturningFromSixMonth;
                output.CashFlowReturningFromOneYear = cashFlowReturningFromOneYear;

                var totalReturningCash =
                    portionCash +
                    portion1M +
                    cashFlowReturningFromThreeMonth +
                    cashFlowReturningFromSixMonth +
                    cashFlowReturningFromOneYear;

                var totalInterestEarned = interest1M + interest3M + interest6M + interest1Y;

                output.EndingCashFlow = period == 0 ? 0 : totalReturningCash + totalInterestEarned;

                outputList.Add(output);
            }

            return outputList;
        }

        public ReinvestmentModelPnLObject GetReinvestmentModelPnL(List<ReinvestmentModelResults> reinvestmentCashFlows)
        {
            var finalCashFlow = reinvestmentCashFlows.Select(x => x.EndingCashFlow).Last();

            var profitFrom3M = reinvestmentCashFlows.Sum(x => x.PortionInThreeMonth) -
                reinvestmentCashFlows.Sum(x => x.CashFlowReturningFromThreeMonth);

            var profitFrom6M = reinvestmentCashFlows.Sum(x => x.PortionInSixMonth) -
                reinvestmentCashFlows.Sum(x => x.CashFlowReturningFromSixMonth);

            var profitFrom1Y = reinvestmentCashFlows.Sum(x => x.PortionInOneYear) -
                reinvestmentCashFlows.Sum(x => x.CashFlowReturningFromOneYear);

            var totalPnL = finalCashFlow + profitFrom3M + profitFrom6M + profitFrom1Y;

            return new ReinvestmentModelPnLObject
            {
                FinalCashFlow = finalCashFlow,
                ProfitFrom3M = profitFrom3M,
                ProfitFrom6M = profitFrom6M,
                ProfitFrom1Y = profitFrom1Y,
                TotalPnL = totalPnL
            };
        }
    }
}
