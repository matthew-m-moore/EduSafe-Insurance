﻿namespace EduSafe.WebApi.Models.Institutions
{
    public class InstitutionOutputEntry
    {
        public double UnenrolledStudentsMonthlyDebtPayment { get; set; }
        public double GraduatedStudentsMonthlyDebtPayment { get; set; }
        public double UnenrollmentWarrantyCoverage { get; set; }
        public double UndergraduateUnemploymentCoverage { get; set; }
        public double UnenrollmentPaybackOption { get; set; }
        public double AverageMonthlyPayment { get; set; }
        public double EndingCohortDefaultRate { get; set; }
    }
}