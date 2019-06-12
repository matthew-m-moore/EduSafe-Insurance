namespace EduSafe.IO.Database.Entities.Servicing.Institutions
{
    public class InstitutionsAccountDataEntity
    {
        public long InstitutionsAccountNumber { get; set; }
        public string FolderPath { get; set; }
        public string InstitutionName { get; set; }
        public string Emails { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
    }
}
