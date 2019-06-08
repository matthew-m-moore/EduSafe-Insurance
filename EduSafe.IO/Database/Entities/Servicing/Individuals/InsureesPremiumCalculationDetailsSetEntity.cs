namespace EduSafe.IO.Database.Entities.Servicing.Individuals
{
    public class InsureesPremiumCalculationDetailsSetEntity
    {
        public int SetId { get; set; }
        public long AccountNumber { get; set; }
        public int InsureesPremiumCalculationDetailsId { get; set; }
        public int InsureesPremiumCalculationOptionDetailsSetId { get; set; }
        public string Description { get; set; }
    }
}
