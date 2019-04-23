namespace EduSafe.IO.Excel.Records
{
    public class ShockParametersRecord
    {
        public int? StartingForecastPeriod { get; set; }
        public int? EndingForecastPeriod { get; set; }
        public string ForecastingScenario { get; set; }

        public string ShockScenarioName { get; set; }
        public bool AllowPremiumsToAdjust { get; set; }

        public string ShockType { get; set; }
        public double ShockValue { get; set; }
        public string ShockLogicType { get; set; }

        public string StartEnrollmentState { get; set; }
        public string EndEnrollmentState { get; set; }
        public string EnrollmentTargetState { get; set; }
        public int? MonthlyTargetPeriod { get; set; }
        public string SpecificCostOrFeeName { get; set; }
    }
}
