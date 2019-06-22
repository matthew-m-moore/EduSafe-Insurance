namespace EduSafe.IO.Database.Entities.Servicing
{
    public class EmailsEntity
    {
        public int Id { get; set; }
        public int EmailsSetId { get; set; }
        public string Email { get; set; }
        public bool IsPrimary { get; set; }
    }
}
