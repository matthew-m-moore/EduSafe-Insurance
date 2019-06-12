﻿using System;

namespace EduSafe.IO.Database.Entities.Servicing.Claims
{
    public class ClaimPaymentEntryEntity
    {
        public int Id { get; set; }
        public long ClaimNumber { get; set; }
        public double ClaimPaymentAmount { get; set; }
        public DateTime ClaimPaymentDate { get; set; }
        public int ClaimPaymentStatusTypeId { get; set; }
        public string ClaimPaymentComments { get; set; }

        public string ClaimPaymentStatusType { get; set; }
    }
}
