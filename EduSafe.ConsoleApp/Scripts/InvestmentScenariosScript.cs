using System;
using System.Collections.Generic;
using EduSafe.ConsoleApp.Interfaces;
using EduSafe.Core.BusinessLogic.Containers;
using EduSafe.Core.BusinessLogic.Investments;
using EduSafe.Core.BusinessLogic.Models.Investments;
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
            RunAllReinvestmentScenarios(reinvestmentOptionsRepository, runPremiumScenario, _numberOfReinvestmentScenario);
        }

        private void RunSpecificReinvestmentScenarioById(
            ReinvestmentOptionsRepository reinvestmentOptionsRepository,
            PremiumComputationResult premiumComputationResult,
            int scenarioId)
        {
            var reinvestmentOptionsParameters = reinvestmentOptionsRepository.GetReinvestmentOptionsParametersFromId(scenarioId);
            var reinvestmentModel = new ReinvestmentModel(premiumComputationResult.PremiumCalculationCashFlows, reinvestmentOptionsParameters);
            reinvestmentModel.GetReinvestmentModelProfitLoss();

            Console.WriteLine("Writing to Excel...");
            var excelFileWriter = new ExcelFileWriter();

            var reinvestmentCashFlows = reinvestmentModel.ReinvestmentModelCashFlows;
            excelFileWriter.AddWorksheetForListOfData(reinvestmentCashFlows, "Reinvestment Cash Flows");
            excelFileWriter.ExportWorkbook(openFileOnSave: true);
        }

        private void RunAllReinvestmentScenarios(
            ReinvestmentOptionsRepository reinvestmentOptionsRepository,
            PremiumComputationResult premiumComputationResults,
            int NumberOfReinvestmentScenario)
        {
            var listOfScenarioProfitLoss = new List<ReinvestmentModelProfitLossResult>();
            var listOfScenarioBeginningCashFlows = new List<ReinvestmentScenarioCashFlowOutputObject>();

            Console.WriteLine("Writing to Excel...");
            var excelFileWriter = new ExcelFileWriter();

            for (int i = 1; i <= NumberOfReinvestmentScenario; i++)
            {
                var reinvestmentOptionsParameters = reinvestmentOptionsRepository.GetReinvestmentOptionsParametersFromId(i);
                var reinvestmentModel = new ReinvestmentModel(premiumComputationResults.PremiumCalculationCashFlows, reinvestmentOptionsParameters);
                var reinvestmentModelProfitLoss = reinvestmentModel.GetReinvestmentModelProfitLoss();

                reinvestmentModelProfitLoss.ScenarioId = i;
                listOfScenarioProfitLoss.Add(reinvestmentModelProfitLoss);

                var reinvestmentCashFlows = reinvestmentModel.ReinvestmentModelCashFlows;
                var reinvestmentModelBeginningCashFlows = GetReinvestmentModelBeginningCashFlows(i, reinvestmentCashFlows);

                excelFileWriter.AddWorksheetForListOfData(reinvestmentModelBeginningCashFlows, "Beginning Cash Flows_"+i);
            }

            excelFileWriter.AddWorksheetForListOfData(listOfScenarioProfitLoss, "ProfitLoss");
            excelFileWriter.ExportWorkbook(openFileOnSave: true);
        }

        private List<ReinvestmentScenarioCashFlowOutputObject> GetReinvestmentModelBeginningCashFlows
            (int scenarioId, List<ReinvestmentModelResultEntry> reinvestmentModelResults)
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

        private class ReinvestmentScenarioCashFlowOutputObject
        {
            public int Period { get; set; }
            public double BeginningCashFlow { get; set; }
        }

        private PremiumComputationResult RunSpecificPremiumScenarioById
            (PremiumComputationRepository premiumComputationRepository, int scenarioId)
        {
            var premiumComputationScenario = premiumComputationRepository.GetPremiumComputationScenarioById(scenarioId);
            var premiumComputationResult = premiumComputationScenario.ComputePremiumResult();
            var premiumNumericalComputationScenario = premiumComputationRepository.GetPremiumComputationScenarioById(scenarioId, true);

            return premiumNumericalComputationScenario.ComputePremiumResult();
        }
    }
}
