using System;

namespace EduSafe.WebApi.Models
{
    public class ModelInputEntry
    {
        public string CollegeName { get; set; }
        public string PublicOrPrivateSchool { get; set; }
        public string CollegeMajor { get; set; }
        public DateTime CollegeStartDate { get; set; }
        public DateTime ExpectedGraduationDate { get; set; }
        public double IncomeCoverageAmount { get; set; }
    }
}