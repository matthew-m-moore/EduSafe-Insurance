using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using EduSafe.Common;
using EduSafe.IO.Database.Entities.Servicing.Individuals;

namespace EduSafe.IO.Database.Mappings.Servicing.Individuals
{
    public class InsureesMajorMinorDetailsMapping : EntityTypeConfiguration<InsureesMajorMinorDetailsEntity>
    {
        public InsureesMajorMinorDetailsMapping()
        {
            HasKey(t => t.Id);

            ToTable("InsureesMajorMinorDetails", Constants.DatabaseOwnerSchemaName);

            Property(t => t.Id)
                .HasColumnName("Id")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(t => t.CollegeMajorId).HasColumnName("CollegeMajorId")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);

            Property(t => t.InsureesMajorMinorDetailsSetId).HasColumnName("InsureesMajorMinorDetailsSetId");
            Property(t => t.AccountNumber).HasColumnName("AccountNumber");
            Property(t => t.IsMinor).HasColumnName("IsMinor");

            MapToStoredProcedures(s =>
                s.Insert(i => i.HasName("SP_InsertInsureesMajorMinorDetails", Constants.DatabaseOwnerSchemaName)
                    .Parameter(p => p.InsureesMajorMinorDetailsSetId, "InsureesMajorMinorDetailsSetId")
                    .Parameter(p => p.AccountNumber, "AccountNumber")
                    .Parameter(p => p.CollegeMajor, "CollegeMajor")
                    .Parameter(p => p.IsMinor, "IsMinor")
                    ));
        }
    }
}
