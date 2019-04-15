namespace EduSafe.Core.BusinessLogic.Containers
{
    public class ReinvestmentOptionsParameters
    {
        public double OneMonthRate { get; set; }
        public double ThreeMonthRate { get; set; }
        public double SixMonthRate { get; set; }
        public double TwelveMonthRate { get; set; }

        public double PortionInCash { get; set; }
        public double PortionIn1M { get; set; }
        public double PortionIn3M { get; set; }
        public double PortionIn6M { get; set; }
        public double PortionIn12M { get; set; }
    }
}
