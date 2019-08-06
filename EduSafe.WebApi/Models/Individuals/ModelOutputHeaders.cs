namespace EduSafe.WebApi.Models.Individuals
{
    public class ModelOutputHeaders
    {
        public string CoverageAmount { get; set; }

        public string YearOneName { get; set; }
        public string YearTwoName { get; set; }
        public string YearThreeName { get; set; }

        public ModelOutputHeaders(string yearOneName, string yearTwoName, string yearThreeName)
        {
            CoverageAmount = "Coverage Amount";

            YearOneName = yearOneName;
            YearTwoName = yearTwoName;
            YearThreeName = yearThreeName;
        }
    }
}