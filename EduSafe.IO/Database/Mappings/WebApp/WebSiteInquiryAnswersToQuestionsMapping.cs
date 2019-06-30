using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using EduSafe.Common;
using EduSafe.IO.Database.Entities.WebApp;

namespace EduSafe.IO.Database.Mappings.WebApp
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

            // Note: There's an important caveat to the way this stored procedure mapping works below.
            // Although IpAddres, CollegeName, etc. are marked as "DatabaseGenerated", EF6 will still
            // included them in an SELECT query, which will cause a run-time error. So, this mapping
            // can only work so long as only INSERT functionality is needed. Apparently, implicit in the
            // paramater mapping below is also a property mapping. In other words, anything mappeed as
            // a stored procedure parameter will be treated as if it's a column on the table for SELECT.
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
