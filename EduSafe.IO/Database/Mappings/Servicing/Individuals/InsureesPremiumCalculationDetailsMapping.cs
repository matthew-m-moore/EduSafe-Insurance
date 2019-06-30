using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using EduSafe.Common;
using EduSafe.IO.Database.Entities.Servicing.Individuals;

namespace EduSafe.IO.Database.Mappings.Servicing.Individuals
{
    public class InsureesPremiumCalculationDetailsMapping : EntityTypeConfiguration<InsureesPremiumCalculationDetailsEntity>
    {
        public InsureesPremiumCalculationDetailsMapping()
        {
            HasKey(t => t.Id);

            ToTable("InsureesPremiumCalculationDetails", Constants.DatabaseOwnerSchemaName);

            Property(t => t.Id)
                .HasColumnName("Id")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(t => t.PremiumCalculated).HasColumnName("PremiumCalculated");
            Property(t => t.PremiumCalculationDate).HasColumnName("PremiumCalculationDate");
            Property(t => t.TotalCoverageAmount).HasColumnName("TotalCoverageAmount");
            Property(t => t.CoverageMonths).HasColumnName("CoverageMonths");
            Property(t => t.CollegeStartDate).HasColumnName("CollegeStartDate");
            Property(t => t.ExpectedGraduationDate).HasColumnName("ExpectedGraduationDate");
            Property(t => t.CollegeDetailId).HasColumnName("CollegeDetailId");
            Property(t => t.InsureesMajorMinorDetailsSetId).HasColumnName("InsureesMajorMinorDetailsSetId");
            Property(t => t.MajorDeclarationDate).HasColumnName("MajorDeclarationDate");
            Property(t => t.UnitsCompleted).HasColumnName("UnitsCompleted");

            MapToStoredProcedures(s =>
                s.Insert(i => i.HasName("SP_InsertInsureesPremiumCalculationDetails", Constants.DatabaseOwnerSchemaName)
                    .Parameter(p => p.PremiumCalculated, "PremiumCalculated")
                    .Parameter(p => p.PremiumCalculationDate, "PremiumCalculationDate")
                    .Parameter(p => p.TotalCoverageAmount, "TotalCoverageAmount")
                    .Parameter(p => p.CoverageMonths, "CoverageMonths")
                    .Parameter(p => p.CollegeStartDate, "CollegeStartDate")
                    .Parameter(p => p.ExpectedGraduationDate, "ExpectedGraduationDate")
                    .Parameter(p => p.CollegeDetailId, "CollegeDetailId")
                    .Parameter(p => p.InsureesMajorMinorDetailsSetId, "InsureesMajorMinorDetailsSetId")
                    .Parameter(p => p.MajorDeclarationDate, "MajorDeclarationDate")
                    .Parameter(p => p.UnitsCompleted, "UnitsCompleted")
                    ));
        }
    }
}
