namespace EduSafe.IO.Database.Entities.Servicing.Claims
{
    public class ClaimOptionEntryEntity
    {
        public int Id { get; set; }
        public long ClaimNumber { get; set; }
        public int ClaimOptionTypeId { get; set; }
        public double ClaimOptionPercentage { get; set; }
    }
}
