using System;
using System.Collections.Generic;
using EduSafe.ConsoleApp.Interfaces;
using EduSafe.Core.Repositories.Excel;
using EduSafe.IO.Excel;

namespace EduSafe.ConsoleApp.Scripts
{
    public class ForecastingScenariosScript : IScript
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
            return "Forecasting Scenarios Analysis Tool";
        }

        public bool GetVisibilityStatus()
        {
            return true;
        }

        public void RunScript(string[] args)
        {
            Console.WriteLine("Loading file and scenarios for forecasting...");
            var pathToExcelFile = args[1];
            var forecastingRepository = new ForecastingRepository(pathToExcelFile);
            var forecastingEngine = forecastingRepository.PrepareForecastingEngine();
            Console.WriteLine("Loading completed.");

            Console.WriteLine("Running forecast...");
            forecastingEngine.RunForecast();
            Console.WriteLine("Run completed.");

            Console.WriteLine("Writing to Excel...");
            var excelFileWriter = new ExcelFileWriter();
            excelFileWriter.AddWorksheetForListOfData(forecastingEngine.ForecastedPremiumCalculationCashFlows, "Numerical Cash Flows");
            excelFileWriter.AddWorksheetForDataTable(forecastingEngine.ForecastedServicingCosts, "Servicing Costs");
            excelFileWriter.AddWorksheetForListOfData(forecastingEngine.ForecastedEnrollmentTimeSeries, "Enrollment Model");
            excelFileWriter.ExportWorkbook(openFileOnSave: true);
            Console.WriteLine("Writing completed.");
        }
    }
}
