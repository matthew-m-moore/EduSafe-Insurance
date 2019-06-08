using System;

namespace EduSafe.IO.Database.Entities.Servicing.Individuals
{
    public class InsureesAcademicHistoryEntity
    {
        public int Id { get; set; }
        public long AccountNumber { get; set; }
        public DateTime AcademicTermStartDate { get; set; }
        public DateTime AcademicTermEndDate { get; set; }
        public string CourseName { get; set; }
        public bool CourseInMajor { get; set; }
        public int CollegeMajorOrMinorId { get; set; }
        public int CourseUnits { get; set; }
        public string CourseGrade { get; set; }
    }
}
