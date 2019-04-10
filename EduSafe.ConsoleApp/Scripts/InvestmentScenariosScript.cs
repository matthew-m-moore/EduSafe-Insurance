using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EduSafe.ConsoleApp.Interfaces;
using EduSafe.Core.BusinessLogic.Containers;
using EduSafe.Core.BusinessLogic.CostsOrFees;
using EduSafe.Core.Repositories;
using EduSafe.Core.Repositories.Excel;
using EduSafe.IO.Excel;

namespace EduSafe.ConsoleApp.Scripts
{
    public class InvestmentScenariosScript : IScript
    {
        private const int _premiuimScenario = 1;
        private const int _numberOfReinvestmentScenario = 1;

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
            Console.WriteLine("Loading file and scenarios for Premium Model");
            var pathToExcelFile = args[1];
            var premiumComputationRepository = new PremiumComputationRepository(pathToExcelFile);
            var premiumComputationScenarios = premiumComputationRepository.GetPremiumComputationScenarios();
            var runPremiumScenario = RunSpecificPremiumScenarioById(premiumComputationRepository, _premiuimScenario);
            Console.WriteLine("Base Scenario for Premium Model has Loaded");

            Console.WriteLine("Loading file and scenarios for Reinvestment Model");
            var reinvestmentOptionsRepository = new ReinvestmentOptionsRepository(pathToExcelFile);
            Console.WriteLine("Reinvestment Parameters have Loaded");

            RunSpecificReinvestmentScenarioById(reinvestmentOptionsRepository, runPremiumScenario, 1);
            //RunAllReinvestmentScenarios(reinvestmentOptionsRepository, runPremiumScenario, _numberOfReinvestmentScenario);
        }

        private void RunSpecificReinvestmentScenarioById(
            ReinvestmentOptionsRepository reinvestmentOptionsRepository,
            PremiumComputationResult premiumComputationResult,
            int scenarioId)
        {
            var reinvestmentOptionsParameters = reinvestmentOptionsRepository.GetReinvestmentOptionsParametersFromId(scenarioId);
            var totalPremiumRemaining = GetPremiumAvailableForReinvestment(premiumComputationResult);
            var reinvestmentCashFlows = GetReinvestmentCashFlows(totalPremiumRemaining, reinvestmentOptionsParameters);
            var reinvestmentModlPnL = new ReinvestmentModelPnLObject();

            Console.WriteLine("Writing to Excel...");
            var excelFileWriter = new ExcelFileWriter();
            excelFileWriter.AddWorksheetForListOfData(totalPremiumRemaining, "Premium Cash Flows");
            excelFileWriter.AddWorksheetForListOfData(reinvestmentCashFlows, "Reinvestment Cash Flows");
            excelFileWriter.ExportWorkbook(openFileOnSave: true);
        }

        private void RunAllReinvestmentScenarios(
            ReinvestmentOptionsRepository reinvestmentOptionsRepository,
            PremiumComputationResult premiumComputationResults,
            int NumberOfReinvestmentScenario)
        {
            var listOfScenarioPnL = new List<ReinvestmentModelPnLObject>();
            var listOfScenarioBeginningCashFlows = new List<ReinvestmentScenarioCashFlowOutputObject>();

            Console.WriteLine("Writing to Excel...");
            var excelFileWriter = new ExcelFileWriter();

            for (int i = 1; i <= NumberOfReinvestmentScenario; i++)
            {
                var reinvestmentOptionsParameters = reinvestmentOptionsRepository.GetReinvestmentOptionsParametersFromId(i);
                var totalPremiumRemaining = GetPremiumAvailableForReinvestment(premiumComputationResults);

                var reinvestmentCashFlows = GetReinvestmentCashFlows(totalPremiumRemaining, reinvestmentOptionsParameters);
                var reinvestmentModelPnL = GetReinvestmentModelPnL(reinvestmentCashFlows);

                var scenarioPnL = new ReinvestmentModelPnLObject
                {
                    ScenarioId = i,
                    FinalCashFlow = reinvestmentModelPnL.FinalCashFlow,
                    ProfitFrom3M = reinvestmentModelPnL.ProfitFrom3M,
                    ProfitFrom6M = reinvestmentModelPnL.ProfitFrom6M,
                    ProfitFrom1Y = reinvestmentModelPnL.ProfitFrom1Y,
                    TotalPnL = reinvestmentModelPnL.TotalPnL
                };
                listOfScenarioPnL.Add(scenarioPnL);

                var reinvestmentModelBeginningCashFlows = GetReinvestmentModelBeginningCashFlows(i, reinvestmentCashFlows);

                excelFileWriter.AddWorksheetForListOfData(reinvestmentModelBeginningCashFlows, "Beginning Cash Flows_"+i);
            }

            excelFileWriter.AddWorksheetForListOfData(listOfScenarioPnL, "PnL");
            excelFileWriter.ExportWorkbook(openFileOnSave: true);
        }

        private List<ReinvestmentScenarioCashFlowOutputObject> GetReinvestmentModelBeginningCashFlows
            (int scenarioId, List<ReinvestmentModelResultsObject> reinvestmentModelResults)
        {
            var listOfCashFlows = new List<ReinvestmentScenarioCashFlowOutputObject>();

            foreach(var item in reinvestmentModelResults)
            {

                var cashFlow = new ReinvestmentScenarioCashFlowOutputObject
                {
                    Period = item.Period,
                    BeginningCashFlow = item.BeginningCashFlow
                };

                listOfCashFlows.Add(cashFlow);
            }
            return listOfCashFlows;
        }

        private ReinvestmentModelPnLObject GetReinvestmentModelPnL (List<ReinvestmentModelResultsObject> reinvestmentCashFlows)
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

        private PremiumComputationResult RunSpecificPremiumScenarioById
            (PremiumComputationRepository premiumComputationRepository, int scenarioId)
        {
            var premiumComputationScenario = premiumComputationRepository.GetPremiumComputationScenarioById(scenarioId);
            var premiumComputationResult = premiumComputationScenario.ComputePremiumResult();
            var premiumNumericalComputationScenario = premiumComputationRepository.GetPremiumComputationScenarioById(scenarioId, true);

            return premiumNumericalComputationScenario.ComputePremiumResult();
        }

        private List<ReinvestmentModelResultsObject> GetReinvestmentCashFlows
            (List<ReinvestmentModelInputsObject> reinvestmentModelInputsObjects,
            ReinvestmentOptionsParameters reinvestmentOptionsParameters)
        {
            var outputList = new List<ReinvestmentModelResultsObject>();

            foreach(var cashflow in reinvestmentModelInputsObjects)
            {
                var output = new ReinvestmentModelResultsObject();
                var period = cashflow.Period;

                var RateOneMonth = reinvestmentOptionsParameters.OneMonthRate;
                var RateThreeMonth = reinvestmentOptionsParameters.ThreeMonthRate;
                var RateSixMonth = reinvestmentOptionsParameters.SixMonthRate;
                var RateOneYear = reinvestmentOptionsParameters.TwelveMonthRate;

                var PortionCash = reinvestmentOptionsParameters.PortionInCash;
                var Portion1M = reinvestmentOptionsParameters.PortionIn1M;
                var Portion3M = reinvestmentOptionsParameters.PortionIn3M;
                var Portion6M = reinvestmentOptionsParameters.PortionIn6M;
                var Portion1Y = reinvestmentOptionsParameters.PortionIn12M;

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

                var interest1M = portion1M * RateOneMonth/1200;
                var interest3M = portion3M * (Math.Pow((1 + RateThreeMonth/1200), 3) - 1);
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

        private List<ReinvestmentModelInputsObject> GetPremiumAvailableForReinvestment(PremiumComputationResult premiumComputationResult)
        {
            var inputList = new List<ReinvestmentModelInputsObject>();
            
            foreach (var cashFlow in premiumComputationResult.PremiumCalculationCashFlows)
            {
                var input = new ReinvestmentModelInputsObject
                {
                    Period = cashFlow.Period,
                    PremiumAvailableForReinvestment = cashFlow.PremiumAvailableForReinvestment,
                    TotalCostsAndClaims = cashFlow.TotalCostsAndClaims,
                    TotalClaims = cashFlow.TotalClaims
                };
                inputList.Add(input);
                
            }

            return inputList;
        }

        private class ReinvestmentScenarioCashFlowOutputObject
        {
            public int Period { get; set; }
            public double BeginningCashFlow { get; set; }
        }

        private class ReinvestmentModelPnLObject
        {
            public int ScenarioId { get; set; }
            public double FinalCashFlow { get; set; }
            public double ProfitFrom3M { get; set; }
            public double ProfitFrom6M { get; set; }
            public double ProfitFrom1Y { get; set; }
            public double TotalPnL { get; set; }
        }

        private class ReinvestmentModelResultsObject
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

        private class ReinvestmentModelInputsObject
        {
            public int Period { get; set; }

            public double PremiumAvailableForReinvestment { get; set; }

            public double TotalCostsAndClaims { get; set; }

            public double TotalClaims { get; set; }

        }
    }
}
