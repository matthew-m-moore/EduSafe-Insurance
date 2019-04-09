using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            excelFileWriter.AddWorksheetForListOfData(forecastingEngine.ForecastedPremiumCalculationCashFlows);
            excelFileWriter.AddWorksheetForDataTable(forecastingEngine.ForecastedServicingCosts);
            excelFileWriter.AddWorksheetForListOfData(forecastingEngine.ForecastedEnrollmentTimeSeries);
            excelFileWriter.ExportWorkbook(openFileOnSave: true);
            Console.WriteLine("Writing completed.");
        }
    }
}
