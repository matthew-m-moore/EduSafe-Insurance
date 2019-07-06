using System;
using System.ComponentModel;

namespace EduSafe.IO.Excel.Records
{
    public class StudentInformationRecord
    {
        [Description("School ID")]
        public string StudentSchoolId { get; set; }
        [Description("Policy ID")]
        public long StudentPolicyId { get; set; }
        [Description("Name")]
        public string StudentName { get; set; }
        [Description("Major")]
        public string StudentMajor { get; set; }

        [Description("Start Date")]
        public DateTime CollegeStartDate { get; set; }
        [Description("Grad. Date")]
        public DateTime CollegeGraduationDate { get; set; }

        [Description("Payment ($)")]
        public double MonthlyPayment { get; set; }
        [Description("Total Paid-In ($)")]
        public double TotalPaidInPremiums { get; set; }
        [Description("Coverage ($)")]
        public double TotalCoverage { get; set; }
        [Description("Remaining ($)")]
        public double? RemainingCoverage { get; set; }

        [Description("Enrolled")]
        public bool IsEnrolled { get; set; }
        [Description("Graduated")]
        public bool HasGraduated { get; set; }
        [Description("Has Claims")]
        public bool HasClaims { get; set; }
    }
}
