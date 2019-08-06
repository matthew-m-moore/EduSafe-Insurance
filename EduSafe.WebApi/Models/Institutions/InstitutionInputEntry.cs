namespace EduSafe.WebApi.Models.Institutions
{
    public class InstitutionInputEntry
    {
        public string IpAddress { get; set; }
        public string CollegeName { get; set; }
        public string PublicOrPrivateSchool { get; set; }
        public string TwoYearOrFourYearSchool { get; set; }
        public double StudentsPerStartingClass { get; set; }
        public double GraduationWithinYears1 { get; set; }
        public double GraduationWithinYears2 { get; set; }
        public double GraduationWithinYears3 { get; set; }
        public double AverageLoanDebtAtGraduation { get; set; }
        public double StartingCohortDefaultRate { get; set; }

        public double IncrementalTargetYear2 => GraduationWithinYears2 - GraduationWithinYears1;
        public double IncrementalTargetYear3 => GraduationWithinYears3 - GraduationWithinYears2;
    }
}