﻿using System;

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

        // Important Note: Even though these properties below are not explicitely mapped to columns in the
        // table in the mapping class, EF6 will still include them in any SELECT query and map them according
        // to how they are used as parameters in any stored procedure mapping. Thus, any attempt to retrieve
        // this entity object from the database will cause a run-time error on the DbContext, because these
        // are not really columns on the table. An INSERT using the entity will still work though, oddly.
        public string CollegeName { get; set; }
        public string CollegeType { get; set; }
        public string IpAddress { get; set; }
        public string Major { get; set; }
    }
}
