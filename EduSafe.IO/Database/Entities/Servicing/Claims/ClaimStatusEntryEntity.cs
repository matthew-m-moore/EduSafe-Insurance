﻿namespace EduSafe.IO.Database.Entities.Servicing.Claims
{
    public class ClaimStatusEntryEntity
    {
        public int Id { get; set; }
        public long ClaimNumber { get; set; }
        public int ClaimStatusTypeId { get; set; }
        public bool IsClaimApproved { get; set; }

        public string ClaimStatusType { get; set; }
    }
}
