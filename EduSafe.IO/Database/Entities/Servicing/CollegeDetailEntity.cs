namespace EduSafe.IO.Database.Entities.Servicing
{
    public partial class CollegeDetailEntity
    {
        public int Id { get; set; }
        public string CollegeName { get; set; }
        public int CollegeTypeId { get; set; }
        public int CollegeAcademicTermTypeId { get; set; }
    }
}
