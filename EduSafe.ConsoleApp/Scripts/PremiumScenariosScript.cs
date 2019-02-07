using System;
using System.Collections.Generic;
using EduSafe.ConsoleApp.Interfaces;
using EduSafe.Core.Repositories;
using EduSafe.IO.Excel;

namespace EduSafe.ConsoleApp.Scripts
{
    public class PremiumScenariosScript : IScript
    {
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
            Console.WriteLine("Script Complete.");
        }

        private class PremiumScenariosOutput
        {
            public int ScenarioId { get; set; }
            public double MonthlyPremium { get; set; }
        }
    }
}
