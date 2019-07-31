namespace EduSafe.WebApi.Models
{
    public class InstitutionInputEntry
    {
        public string IpAddress { get; set; }
        public string CollegeName { get; set; }
        public string PublicOrPrivateSchool { get; set; }
        public string TwoYearOrFourYearSchool { get; set; }
        public double GraduationWithinYears1 { get; set; }
        public double GraduationWithinYears2 { get; set; }
        public double GraduationWithinYears3 { get; set; }
        public double AverageLoanDebtAtGraduation { get; set; }
        public double StartingCohortDefaultRate { get; set; }
    }
}