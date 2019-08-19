using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using EduSafe.Common;
using EduSafe.IO.Database.Entities.WebApp;

namespace EduSafe.IO.Database.Mappings.WebApp
{
    public class WebSiteInquiryInstitutionalInputsMapping : EntityTypeConfiguration<WebSiteInquiryInstitutionalInputsEntity>
    {
        public WebSiteInquiryInstitutionalInputsMapping()
        {
            HasKey(t => t.Id);

            ToTable("WebSiteInquiryInstitutionalInputs", Constants.DatabaseOwnerSchemaName);

            Property(t => t.Id)
                .HasColumnName("Id")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(t => t.IpAddressId).HasColumnName("IpAddressId")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
            Property(t => t.CollegeNameId).HasColumnName("CollegeNameId")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
            Property(t => t.CollegeTypeId).HasColumnName("CollegeTypeId")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
            Property(t => t.DegreeTypeId).HasColumnName("DegreeTypeId")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);

            Property(t => t.StudentsPerStartingClass).HasColumnName("StudentsPerStartingClass");
            Property(t => t.GraduationWithinYears1).HasColumnName("GraduationWithinYears1");
            Property(t => t.GraduationWithinYears2).HasColumnName("GraduationWithinYears2");
            Property(t => t.GraduationWithinYears3).HasColumnName("GraduationWithinYears3");
            Property(t => t.StartingCohortDefaultRate).HasColumnName("StartingCohortDefaultRate");
            Property(t => t.AverageLoanDebtAtGraduation).HasColumnName("AverageLoanDebtAtGraduation");

            // Note: There's an important caveat to the way this stored procedure mapping works below.
            // Although IpAddres, CollegeName, etc. are marked as "DatabaseGenerated", EF6 will still
            // included them in an SELECT query, which will cause a run-time error. So, this mapping
            // can only work so long as only INSERT functionality is needed. Apparently, implicit in the
            // paramater mapping below is also a property mapping. In other words, anything mappeed as
            // a stored procedure parameter will be treated as if it's a column on the table for SELECT.
            MapToStoredProcedures(s =>
                s.Insert(i => i.HasName("SP_InsertWebSiteInquiryInstitutionalInputs", Constants.DatabaseOwnerSchemaName)
                    .Parameter(p => p.CollegeName, "CollegeName")
                    .Parameter(p => p.CollegeType, "CollegeType")
                    .Parameter(p => p.IpAddress, "IpAddress")
                    .Parameter(p => p.DegreeType, "DegreeType")
                    .Parameter(p => p.StudentsPerStartingClass, "StudentsPerStartingClass")
                    .Parameter(p => p.GraduationWithinYears1, "GraduationWithinYears1")
                    .Parameter(p => p.GraduationWithinYears2, "GraduationWithinYears2")
                    .Parameter(p => p.GraduationWithinYears3, "GraduationWithinYears3")
                    .Parameter(p => p.StartingCohortDefaultRate, "StartingCohortDefaultRate")
                    .Parameter(p => p.AverageLoanDebtAtGraduation, "AverageLoanDebtAtGraduation")
                    ));
        }
    }
}
