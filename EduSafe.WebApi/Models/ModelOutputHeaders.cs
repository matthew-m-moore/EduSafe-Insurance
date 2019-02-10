namespace EduSafe.WebApi.Models
{
    public class ModelOutputHeaders
    {
        public string CoverageMonths { get; set; }

        public string YearOneName { get; set; }
        public string YearTwoName { get; set; }
        public string YearThreeName { get; set; }

        public ModelOutputHeaders(string yearOneName, string yearTwoName, string yearThreeName)
        {
            CoverageMonths = "Coverage Months";

            YearOneName = yearOneName;
            YearTwoName = yearTwoName;
            YearThreeName = yearThreeName;
        }
    }
}