namespace EduSafe.IO.Excel.Records
{
    public class EnrollmentModelScenarioRecord
    {
        public int Id { get; set; }
        public string Scenario { get; set; }
        public string VectorSetName { get; set; }
        public string RateCurveSet { get; set; }
        public string DiscountCurve { get; set; }

        public int StartPeriod { get; set; }
        public int EarlyHireStartPeriod { get; set; }
        public int TotalMonths { get; set; }

        public double PaidIn { get; set; }
        public double Margin { get; set; }
        public double AnnualIncome { get; set; }
        public int CoverageMonths { get; set; }

        public double EarlyHireOptionRatio { get; set; }
        public double DropOutOptionRatio { get; set; }
        public double GradSchoolOptionRatio { get; set; }

        public double FourYearGradTarget { get; set; }
        public double FiveYearGradTarget { get; set; }
        public double SixYearGradTarget { get; set; }
        public double DropOutTarget { get; set; }
        public double HireEarlyTarget { get; set; }
        public double HireTarget { get; set; }
        public double GradSchoolTarget { get; set; }
        public double UnemploymentTarget { get; set; }            
    }
}
