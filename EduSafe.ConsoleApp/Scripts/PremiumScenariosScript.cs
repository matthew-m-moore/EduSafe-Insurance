using System;
using System.Linq;
using System.Collections.Generic;
using EduSafe.ConsoleApp.Interfaces;
using EduSafe.Core.BusinessLogic.Containers.CashFlows;
using EduSafe.Core.BusinessLogic.Scenarios;
using EduSafe.Core.Repositories;
using EduSafe.IO.Excel;

namespace EduSafe.ConsoleApp.Scripts
{
    public class PremiumScenariosScript : IScript
    {
        private const string _runAllCommand = "All";
        private const string _exitCommand = "Exit";

        public List<string> GetArgumentsList()
        {
            return new List<string>
            {
                "[1] Enter path to the Excel file input",
            };
        }

        public string GetFriendlyName()
        {
            return "Premium Scenarios Analysis Tool";
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
            Console.WriteLine("Scenarios Loaded.");
            

            while (true)
            {
                Console.WriteLine("=====================================");
                Console.WriteLine("Here are your options:");
                Console.WriteLine("1. Enter Scenario Ids (separated by commas or spaces) for detailed analysis on each");
                Console.WriteLine("2. Type '" + _runAllCommand + "' or press [Enter] to run all scenarios");
                Console.WriteLine("3. Type '" + _exitCommand + "' to quit");
                Console.WriteLine("=====================================");
                var userCommand = Console.ReadLine();

                if (userCommand.ToUpper() == _exitCommand.ToUpper()) break;

                if (string.IsNullOrWhiteSpace(userCommand) ||
                    userCommand.ToUpper() == _runAllCommand.ToUpper())
                {
                    RunAllScenarios(premiumComputationScenarios);
                    continue;
                }

                var parsedScenarioIds = userCommand.Split(',', ' ');
                foreach (var parsedScenarioId in parsedScenarioIds)
                {
                    if (!int.TryParse(parsedScenarioId, out int scenarioId))
                    {
                        Console.WriteLine("ERROR: Could not convert '" + parsedScenarioId + "' to a scenario Id.");
                        continue;
                    }

                    RunSpecificScenarioById(premiumComputationRepository, scenarioId);
                }
            }
        }

        private void RunAllScenarios(Dictionary<int, PremiumComputationEngine> premiumComputationScenarios)
        {
            var listOfPremiumScenariosOutput = new List<PremiumScenariosOutput>();
            foreach (var premiumComputationScenario in premiumComputationScenarios)
            {
                Console.WriteLine(string.Format("Running scenario '{0}'...", premiumComputationScenario.Value.ScenarioName));
                var premiumComputationResult = premiumComputationScenario.Value.ComputePremiumResult();
                var premiumScenariosOutput = new PremiumScenariosOutput
                {
                    ScenarioId = premiumComputationScenario.Key,
                    MonthlyPremium = premiumComputationResult.CalculatedMonthlyPremium
                };

                listOfPremiumScenariosOutput.Add(premiumScenariosOutput);
                Console.WriteLine("Scenario Finished.");
            }

            Console.WriteLine("Writing to Excel...");
            var excelFileWriter = new ExcelFileWriter();
            excelFileWriter.AddWorksheetForListOfData(listOfPremiumScenariosOutput, "Results");
            excelFileWriter.ExportWorkbook(openFileOnSave: true);
            Console.WriteLine("Run All Scenarios Complete.");
        }

        private class PremiumScenariosOutput
        {
            public int ScenarioId { get; set; }
            public double MonthlyPremium { get; set; }
        }

        private void RunSpecificScenarioById(PremiumComputationRepository premiumComputationRepository, int scenarioId)
        {
            var premiumComputationScenario = premiumComputationRepository.GetPremiumComputationScenarioById(scenarioId);
            var premiumComputationResult = premiumComputationScenario.ComputePremiumResult();
            var premiumNumericalComputationScenario = premiumComputationRepository.GetPremiumComputationScenarioById(scenarioId, true);
            var premiumNumericalComputationResult = premiumNumericalComputationScenario.ComputePremiumResult();
            Console.WriteLine(string.Format("Scenario Completed: {0}", premiumComputationResult.ScenarioName));

            Console.WriteLine("Preparing results...");
            var analyticalPremiumCalculationCashFlows =
                premiumComputationResult.PremiumCalculationCashFlows.Select(c => (AnalyticalPremiumCalculationCashFlow)c).ToList();

            var totalPaidInPremiums = premiumComputationResult.PremiumCalculationModelInput.PreviouslyPaidInPremiums;
            var premiumScenarioSummary = new PremiumScenarioSummary
            {
                ScenarioId = premiumComputationResult.ScenarioId.GetValueOrDefault(),
                ScenarioName = premiumComputationResult.ScenarioName,
                Numerator = analyticalPremiumCalculationCashFlows.Sum(c => c.DiscountedTotalNumerator) - totalPaidInPremiums,
                Denominator = analyticalPremiumCalculationCashFlows.Sum(c => c.DiscountedTotalDenominator),
                CalculatedMonthlyPremium = premiumComputationResult.CalculatedMonthlyPremium
            };

            Console.WriteLine("Writing to Excel...");
            var excelFileWriter = new ExcelFileWriter();
            excelFileWriter.AddWorksheetForListOfData(new List<PremiumScenarioSummary> { premiumScenarioSummary }, "Summary");
            excelFileWriter.AddWorksheetForListOfData(premiumComputationResult.EnrollmentStateTimeSeries, "Enrollment Model");
            excelFileWriter.AddWorksheetForDataTable(premiumComputationResult.ServicingCosts, "Servicing Costs");
            excelFileWriter.AddWorksheetForListOfData(analyticalPremiumCalculationCashFlows, "Analytical Cash Flows");
            excelFileWriter.AddWorksheetForListOfData(premiumNumericalComputationResult.PremiumCalculationCashFlows, "Numerical Cash Flows");
            excelFileWriter.ExportWorkbook(openFileOnSave: true);
            Console.WriteLine("Run Scenario Id #" + scenarioId + " Complete.");
        }

        private class PremiumScenarioSummary
        {
            public int ScenarioId { get; set; }
            public string ScenarioName { get; set; }
            public double Numerator { get; set; }
            public double Denominator { get; set; }
            public double CalculatedMonthlyPremium { get; set; }
        }
    }
}
