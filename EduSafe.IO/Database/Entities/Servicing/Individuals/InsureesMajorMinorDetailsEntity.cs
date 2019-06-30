namespace EduSafe.IO.Database.Entities.Servicing.Individuals
{
    public partial class InsureesMajorMinorDetailsEntity
    {
        public int Id { get; set; }
        public int InsureesMajorMinorDetailsSetId { get; set; }
        public long AccountNumber { get; set; }
        public int CollegeMajorId { get; set; }
        public bool IsMinor { get; set; }
    }
}
