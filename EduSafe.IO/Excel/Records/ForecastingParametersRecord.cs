namespace EduSafe.IO.Excel.Records
{
    public class ForecastingParametersRecord
    {
        public string ForecastName { get; set; }
        public bool ApplyFirstYearPercentGlobally { get; set; }
        public bool IgnoreRollForwardOnRateCurves { get; set; }
        public int MonthlyPeriodsToForecast { get; set; }
        public int ReinvestmentOptionsParameterSetId { get; set; }
        public string ShockParameterSet { get; set; }
    }
}
