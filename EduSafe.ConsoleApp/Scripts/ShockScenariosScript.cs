using System;
using System.Collections.Generic;
using System.Linq;
using EduSafe.ConsoleApp.Interfaces;
using EduSafe.Core.BusinessLogic.Containers;
using EduSafe.Core.BusinessLogic.Scenarios;
using EduSafe.Core.Repositories;
using EduSafe.Core.Repositories.Excel;
using EduSafe.IO.Excel;

namespace EduSafe.ConsoleApp.Scripts
{
    public class ShockScenariosScript : IScript
    {
        public List<string> GetArgumentsList()
        {
            return new List<string>
            {
                "[1] Enter path to the Excel file input",
                "[2] Use numerical computation method (TRUE/FALSE)"
            };
        }

        public string GetFriendlyName()
        {
            return "Shock Scenarios Runner Tool";
        }

        public bool GetVisibilityStatus()
        {
            return true;
        }

        public void RunScript(string[] args)
        {
            Console.WriteLine("Loading file and scenarios...");
            var pathToExcelFile = args[1];
            var useNumericalComputationMethod = bool.Parse(args[2]);
            var premiumComputationRepository = new PremiumComputationRepository(pathToExcelFile);
            var premiumComputationScenarios = premiumComputationRepository.GetPremiumComputationScenarios(useNumericalComputationMethod);
            Console.WriteLine("Scenarios Loaded.");

            Console.WriteLine("Loading shock parameters...");
            var shockScenariosRepostory = new ShockScenariosRepository(pathToExcelFile);
            var shockScenariosDictionary = shockScenariosRepostory.RetrieveAllScenarios();
            Console.WriteLine("Shock Parameters Loaded.");

            Console.WriteLine("Running shock scenarios...");
            var shockScenarioResultsList = new List<PremiumComputationResult>();
            foreach (var premiumComputationScenario in premiumComputationScenarios.Values)
            {
                var premiumComputationShocksEngine = new PremiumComputationShocksEngine(premiumComputationScenario, shockScenariosDictionary);
                premiumComputationShocksEngine.RunShockScenarios();
                shockScenarioResultsList.AddRange(premiumComputationShocksEngine.ScenarioResultsDictionary.Values);
            }
            
            var shocksScenarioResultsSummaryList = shockScenarioResultsList.Select(r => r.ResultSummary).ToList();
            Console.WriteLine("Shocks Scenarios Completed.");

            Console.WriteLine("Writing to Excel...");
            var excelFileWriter = new ExcelFileWriter();
            excelFileWriter.AddWorksheetForListOfData(shocksScenarioResultsSummaryList, "Results");
            excelFileWriter.ExportWorkbook(openFileOnSave: true);
            Console.WriteLine("Run All Scenarios Complete.");
        }
    }
}
