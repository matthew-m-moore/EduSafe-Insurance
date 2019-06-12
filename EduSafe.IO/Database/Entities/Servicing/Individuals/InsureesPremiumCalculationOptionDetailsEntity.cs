namespace EduSafe.IO.Database.Entities.Servicing.Individuals
{
    public class InsureesPremiumCalculationOptionDetailsEntity
    {
        public int Id { get; set; }
        public int InsureesPremiumCalculationOptionDetailsSetId { get; set; }
        public int OptionTypeId { get; set; }
        public double OptionPercentage { get; set; }
    }
}
