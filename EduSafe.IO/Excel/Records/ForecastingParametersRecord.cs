namespace EduSafe.IO.Excel.Records
{
    public class ForecastingParametersRecord
    {
        public string ForecastName { get; set; }
        public bool ApplyFirstYearPercentGlobally { get; set; }
        public bool AllowPremiumsToAdjustWithScenarios { get; set; }
        public int MonthlyPeriodsToForecast { get; set; }
        public string ReinvestmentOptionsParameterSet { get; set; }
        public string ShockParameterSet { get; set; }
    }
}
