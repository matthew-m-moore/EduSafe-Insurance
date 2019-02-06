namespace EduSafe.IO.Excel.Records
{
    public class InterestRateTypeDetailRecord
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string EnumName { get; set; }
        public int? TenorInMonths { get; set; }
        public string DayCountConvention { get; set; }
        public bool IsZeroRate { get; set; }
        public bool IsDiscountFactor { get; set; }
    }
}
