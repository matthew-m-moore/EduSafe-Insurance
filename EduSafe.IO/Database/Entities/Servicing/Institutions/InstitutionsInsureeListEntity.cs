namespace EduSafe.IO.Database.Entities.Servicing.Institutions
{
    public class InstitutionsInsureeListEntity
    {
        public int Id { get; set; }
        public long InstitutionsAccountNumber { get; set; }
        public long InsureeAccountNumber { get; set; }
    }
}
