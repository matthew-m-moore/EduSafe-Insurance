using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using EduSafe.Common;
using EduSafe.IO.Database.Entities;

namespace EduSafe.IO.Database.Mappings
{
    public class WebSiteInquiryAnswersToQuestionsMapping : EntityTypeConfiguration<WebSiteInquiryAnswersToQuestionsEntity>
    {
        public WebSiteInquiryAnswersToQuestionsMapping()
        {
            HasKey(t => t.Id);

            ToTable("WebSiteInquiryAnswersToQuestions", Constants.DatabaseOwnerSchemaName);

            Property(t => t.Id)
                .HasColumnName("Id")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(t => t.IpAddressId).HasColumnName("IpAddressId")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
            Property(t => t.CollegeNameId).HasColumnName("CollegeNameId")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
            Property(t => t.CollegeTypeId).HasColumnName("CollegeTypeId")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
            Property(t => t.MajorId).HasColumnName("MajorId")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);

            Property(t => t.CollegeStartDate).HasColumnName("CollegeStartDate");
            Property(t => t.GraduationDate).HasColumnName("GraduationDate");
            Property(t => t.AnnualCoverage).HasColumnName("AnnualCoverage");

            MapToStoredProcedures(s =>
                s.Insert(i => i.HasName("SP_InsertWebSiteInquiryAnswersToQuestions", Constants.DatabaseOwnerSchemaName)
                    .Parameter(p => p.CollegeName, "CollegeName")
                    .Parameter(p => p.CollegeType, "CollegeType")
                    .Parameter(p => p.IpAddress, "IpAddress")
                    .Parameter(p => p.Major, "Major")
                    .Parameter(p => p.CollegeStartDate, "CollegeStartDate")
                    .Parameter(p => p.GraduationDate, "GraduationDate")
                    .Parameter(p => p.AnnualCoverage, "AnnualCoverage")
                    ));
        }
    }
}
