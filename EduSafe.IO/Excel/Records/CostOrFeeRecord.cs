namespace EduSafe.IO.Excel.Records
{
    public class CostOrFeeRecord
    {
        public string CostModelName { get; set; }
        public string CostOrFeeName { get; set; }
        public double Amount { get; set; }
        public int? FrequencyInMonths { get; set; }
        public string DrivingEvent { get; set; }
        public int? EndingPeriod { get; set; }
    }
}
