using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EduSafe.ConsoleApp.Interfaces;
using EduSafe.Core.BusinessLogic.Containers;
using EduSafe.Core.BusinessLogic.CostsOrFees;
using EduSafe.Core.Repositories;
using EduSafe.IO.Excel;

namespace EduSafe.ConsoleApp.Scripts
{
    public class InvestmentScenariosScript : IScript
    {
        private const double RateOneMonth = 2.43;
        private const double RateThreeMonth = 2.46;
        private const double RateSixMonth = 2.51;
        private const double RateOneYear = 2.55;

        private const double PortionCash = .20;
        private const double Portion1M = .20;
        private const double Portion3M = .20;
        private const double Portion6M = .20;
        private const double Portion1Y = .20;
        

        public List<string> GetArgumentsList()
        {
            return new List<string>
            {
                "[1] Enter path to the Excel file input",
            };
        }

        public string GetFriendlyName()
        {
            return "Risk Scenarios Analysis Tool";
        }

        public bool GetVisibilityStatus()
        {
            return true;
        }

        public void RunScript(string[] args)
        {
            Console.WriteLine("Loading file and scenarios...");
            var pathToExcelFile = args[1];
            var premiumComputationRepository = new PremiumComputationRepository(pathToExcelFile);
            var premiumComputationScenarios = premiumComputationRepository.GetPremiumComputationScenarios();
            Console.WriteLine("Scenario Loaded.");

            var runScenario = RunSpecificScenarioById(premiumComputationRepository, 1);
            var createInputs = CreateInputsForReinvestmentModel(runScenario);
            var totalPremiumRemaining = GetTotalPremiumRemaining(createInputs);
            var reinvestmentCashFlows = GetReinvestmentCashFlows(totalPremiumRemaining);

            var finalCashFlow = reinvestmentCashFlows.Select(x => x.EndingCashFlow).Last();

            var profitFrom3M = reinvestmentCashFlows.Sum(x => x.PortionInThreeMonth) -
                reinvestmentCashFlows.Sum(x => x.CashFlowReturningFromThreeMonth);

            var profitFrom6M = reinvestmentCashFlows.Sum(x => x.PortionInSixMonth)-
                reinvestmentCashFlows.Sum(x => x.CashFlowReturningFromSixMonth);

            var profitFrom1Y = reinvestmentCashFlows.Sum(x => x.PortionInOneYear)-
                reinvestmentCashFlows.Sum(x => x.CashFlowReturningFromOneYear);

            var totalPL = finalCashFlow + profitFrom3M + profitFrom6M + profitFrom1Y;

        }

        private PremiumComputationResult RunSpecificScenarioById
            (PremiumComputationRepository premiumComputationRepository, int scenarioId)
        {
            var premiumComputationScenario = premiumComputationRepository.GetPremiumComputationScenarioById(scenarioId);
            var premiumComputationResult = premiumComputationScenario.ComputePremiumResult();
            var premiumNumericalComputationScenario = premiumComputationRepository.GetPremiumComputationScenarioById(scenarioId, true);

            return premiumNumericalComputationScenario.ComputePremiumResult();
        }

        private List<ReinvestmentModelResults> GetReinvestmentCashFlows
            (List<ReinvestmentModelTotalPremiumRemaining> reinvestmentModelTotalPremiumRemaining)
        {
            var outputList = new List<ReinvestmentModelResults>();

            foreach(var cashflow in reinvestmentModelTotalPremiumRemaining)
            {
                var output = new ReinvestmentModelResults();
                var period = cashflow.Period;

                output.Period = period;

                 var begBalance = period == 0 ? cashflow.TotalPremiumUsedForReinvestment :
                    outputList.Where(x => x.Period == period - 1).Select(x => x.EndingCashFlow).First();

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

                var interest1M = portion1M * RateOneMonth/1200;
                var interest3M = portion3M * (Math.Pow((1 + RateThreeMonth/1200), 3) - 1);
                var interest6M = portion6M * (Math.Pow((1 + RateSixMonth / 1200), 6) - 1);
                var interest1Y = portion1Y * (Math.Pow((1 + RateOneYear / 1200), 12) - 1);

                output.InterestFromOneMonth = interest1M;
                output.InterestFromThreeMonth = interest3M;
                output.InterestFromSixMonth = interest6M;
                output.InterestFromOneYear = interest1Y;

                var cashFlowReturningFromThreeMonth = period < 3 ? 0 :
                    outputList.Where(x => x.Period < period - 3).Select(x => x.PortionInThreeMonth).First();

                var cashFlowReturningFromSixMonth = period < 6 ? 0 :
                    outputList.Where(x => x.Period < period - 6).Select(x => x.PortionInSixMonth).First();

                var cashFlowReturningFromOneYear = period < 12 ? 0 :
                    outputList.Where(x => x.Period < period - 12).Select(x => x.PortionInOneYear).First();

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

                output.EndingCashFlow = totalReturningCash + totalInterestEarned;

                outputList.Add(output);
               
            }

            return outputList;
        }

        private class ReinvestmentModelResults
        {
            public int Period { get; set; }

            public double BeginningCashFlow { get; set; }
            public double EndingCashFlow { get; set; }

            public double PortionInCash { get; set; }
            public double PortionInOneMonth { get; set; }
            public double PortionInThreeMonth { get; set; }
            public double PortionInSixMonth { get; set; }
            public double PortionInOneYear { get; set; }

            public double InterestFromOneMonth { get; set; }
            public double InterestFromThreeMonth { get; set; }
            public double InterestFromSixMonth { get; set; }
            public double InterestFromOneYear { get; set; }

            public double CashFlowReturningFromThreeMonth { get; set; }
            public double CashFlowReturningFromSixMonth { get; set; }
            public double CashFlowReturningFromOneYear { get; set; }
        }

        private List<ReinvestmentModelTotalPremiumRemaining> GetTotalPremiumRemaining
            (List<ReinvestmentModelInputs> reinvestmentModelInputs)
        {
            var outputList = new List<ReinvestmentModelTotalPremiumRemaining>();
            var numerator = reinvestmentModelInputs.Select(x => x.CostsOperational).Sum();
            var denominator = reinvestmentModelInputs.Select(x => x.TotalPremiums).Sum();

            var factorForOperationalCosts = numerator / denominator;

            foreach(var cashFlow in reinvestmentModelInputs)
            {
                var output = new ReinvestmentModelTotalPremiumRemaining();
                var period = cashFlow.Period;

                var nonOperationalCashFlow = cashFlow.NonOperationalOutFlows;
                output.NonOperationalOutFlows = nonOperationalCashFlow;

                var totalPremiumUseForOps = factorForOperationalCosts * cashFlow.TotalPremiums;
                output.TotalPremiumUsedForOperations = totalPremiumUseForOps;

                output.TotalPremiumUsedForReinvestment = totalPremiumUseForOps - nonOperationalCashFlow;

                outputList.Add(output);
            }

            return outputList;
        }

        private class ReinvestmentModelTotalPremiumRemaining
        {
            public int Period { get; set; }

            public double NonOperationalOutFlows { get; set; }

            // Premium * SUM(CostsOperational)/SUM(TotalPremiums)
            public double TotalPremiumUsedForOperations { get; set; }

            // TotalPremiums - TotalPremiumUsedForOperations - NonOperationalOutFlows
            public double TotalPremiumUsedForReinvestment { get; set; }
        }

        private List<ReinvestmentModelInputs> CreateInputsForReinvestmentModel
            (PremiumComputationResult premiumComputationResult)
        {
            var inputList = new List<ReinvestmentModelInputs>();


            foreach (var cashFlow in premiumComputationResult.PremiumCalculationCashFlows)
            {

                var input = new ReinvestmentModelInputs();
                var period = cashFlow.Period;

                if (period == 72) continue;
                else
                {
                    var enrollmentFactor = premiumComputationResult.EnrollmentStateTimeSeries[period].Enrolled;

                    input.Period = period;
                    input.EnrollmentFactor = enrollmentFactor;

                    var costBackground = premiumComputationResult.ServicingCosts.Rows[period]["Background Check Fee"];
                    var costCredit = premiumComputationResult.ServicingCosts.Rows[period]["Credit Score Fee"];
                    var costTranscripts = premiumComputationResult.ServicingCosts.Rows[period]["Transcripts Fee"];
                    var costServicing = premiumComputationResult.ServicingCosts.Rows[period]["Servicing Fee"];
                    input.CostsOperational =
                        Convert.ToDouble(costBackground)
                        + Convert.ToDouble(costCredit)
                        + Convert.ToDouble(costTranscripts)
                        + Convert.ToDouble(costServicing);

                    var costDropOut = premiumComputationResult.ServicingCosts.Rows[period]["Drop Out Verification Fee"];
                    var costGradSchool = premiumComputationResult.ServicingCosts.Rows[period]["Grad School Verification Fee"];
                    var costEarlyHire = premiumComputationResult.ServicingCosts.Rows[period]["Early Hire Rate Verification Fee"];
                    var costUnemployment = premiumComputationResult.ServicingCosts.Rows[period]["Unemployment Verification Fee"];
                    input.CostsEvents =
                        Convert.ToDouble(costDropOut)
                        + Convert.ToDouble(costGradSchool)
                        + Convert.ToDouble(costEarlyHire)
                        + Convert.ToDouble(costUnemployment);

                    input.CostsTotal = input.CostsOperational + input.CostsEvents;

                    var claimDropOut = cashFlow.ProbabilityAdjustedDropOutClaims;
                    var claimGradSchool = cashFlow.ProbabilityAdjustedGradSchoolClaims;
                    var claimEarlyHire = cashFlow.ProbabilityAdjustedEarlyHireClaims;
                    var claimUnemployment = cashFlow.ProbabilityAdjustedUnemploymentClaims;
                    input.ClaimOutflows = claimDropOut + claimGradSchool + claimEarlyHire + claimUnemployment;

                    input.NonOperationalOutFlows = input.CostsEvents + input.ClaimOutflows;

                    input.TotalPremiums = premiumComputationResult.CalculatedMonthlyPremium * enrollmentFactor;

                    inputList.Add(input);
                }
            }

            return inputList;
        }

        private class ReinvestmentModelInputs
        {
            public int Period { get; set; }

            public double EnrollmentFactor { get; set; }

            public double CostsOperational { get; set; }

            public double CostsEvents { get; set; }

            //CostsOperational + CostsEvents
            public double CostsTotal { get; set; } 

            public double ClaimOutflows { get; set; }

            // CostsEvets + ClaimOutFlows
            public double NonOperationalOutFlows { get; set; }

            public double TotalPremiums { get; set; }
        }
    }
}
