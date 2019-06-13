using System;

namespace EduSafe.IO.Database.Entities.Servicing.Individuals
{
    public class InsureesAccountDataEntity
    {
        public long AccountNumber { get; set; }
        public string FolderPath { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public int EmailsSetId { get; set; }
        public long SSN { get; set; }
        public DateTime Birthdate { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public bool IsInsuredViaInstitution { get; set; }
    }
}
