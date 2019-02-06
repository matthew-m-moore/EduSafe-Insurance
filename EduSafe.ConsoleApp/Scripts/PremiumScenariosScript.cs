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
            var pathToExcelFile = args[1];
            var premiumComputationRepository = new PremiumComputationRepository(pathToExcelFile);
            var premiumComputationScenarios = premiumComputationRepository.GetPremiumComputationScenarios();

            var listOfPremiumScenariosOutput = new List<PremiumScenariosOutput>();
            foreach (var premiumComputationScenario in premiumComputationScenarios)
            {
                var premiumComputationResult = premiumComputationScenario.Value.ComputePremiumResult();
                var premiumScenariosOutput = new PremiumScenariosOutput
                {
                    ScenarioId = premiumComputationScenario.Key,
                    MonthlyPremium = premiumComputationResult.CalculatedMonthlyPremium
                };

                listOfPremiumScenariosOutput.Add(premiumScenariosOutput);
            }

            var excelFileWriter = new ExcelFileWriter();
            excelFileWriter.AddWorksheetForListOfData(listOfPremiumScenariosOutput, "Results");
            excelFileWriter.ExportWorkbook(openFileOnSave: true);
        }

        private class PremiumScenariosOutput
        {
            public int ScenarioId { get; set; }
            public double MonthlyPremium { get; set; }
        }
    }
}
