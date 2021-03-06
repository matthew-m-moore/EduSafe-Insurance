﻿using System;

namespace EduSafe.IO.Database.Entities.Servicing.Individuals
{
    public partial class InsureesPremiumCalculationDetailsEntity
    {
        public int Id { get; set; }
        public double PremiumCalculated { get; set; }
        public DateTime PremiumCalculationDate { get; set; }
        public double TotalCoverageAmount { get; set; }
        public int CoverageMonths { get; set; }
        public DateTime CollegeStartDate { get; set; }
        public DateTime ExpectedGraduationDate { get; set; }
        public int CollegeDetailId { get; set; }
        public int InsureesMajorMinorDetailsSetId { get; set; }
        public DateTime MajorDeclarationDate { get; set; }
        public int UnitsCompleted { get; set; }
    }
}
