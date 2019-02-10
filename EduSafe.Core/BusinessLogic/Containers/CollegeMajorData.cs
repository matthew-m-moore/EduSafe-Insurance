namespace EduSafe.Core.BusinessLogic.Containers
{
    public class CollegeMajorData
    {
        public string CollegeMajor { get; }
        public double UnemploymentRate { get; }
        public double UnemploymentRateError { get; }
        public double MedianSalary { get; }

        public double LowEndUnemploymentRate => UnemploymentRate - UnemploymentRateError;
        public double HighEndUnemploymentRate => UnemploymentRate + UnemploymentRateError;

        public CollegeMajorData(
            string collegeMajor,
            double unemploymentRate,
            double unemploymentRateError,
            double medianSalary)
        {
            CollegeMajor = collegeMajor;
            UnemploymentRate = unemploymentRate;
            UnemploymentRateError = unemploymentRateError;
            MedianSalary = medianSalary;
        }
    }
}
