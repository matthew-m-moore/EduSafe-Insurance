using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace EduSafe.IO.Database.Entities.WebApp
{
    public class WebSiteInquiryAnswersToQuestionsEntity
    {
        public int Id { get; set; }
        public int IpAddressId { get; set; }
        public int CollegeNameId { get; set; }
        public int CollegeTypeId { get; set; }
        public int MajorId { get; set; }
        public DateTime CollegeStartDate { get; set; }
        public DateTime GraduationDate { get; set; }
        public double AnnualCoverage { get; set; }

        public string CollegeName { get; set; }
        public string CollegeType { get; set; }
        public string IpAddress { get; set; }
        public string Major { get; set; }
    }
}
