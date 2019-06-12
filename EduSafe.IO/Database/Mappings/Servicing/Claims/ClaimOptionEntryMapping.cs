using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using EduSafe.Common;
using EduSafe.IO.Database.Entities.Servicing.Claims;

namespace EduSafe.IO.Database.Mappings.Servicing.Claims
{
    public class ClaimOptionEntryMapping : EntityTypeConfiguration<ClaimOptionEntryEntity>
    {
        public ClaimOptionEntryMapping()
        {
            HasKey(t => t.Id);

            ToTable("ClaimOptionEntry", Constants.DatabaseOwnerSchemaName);

            Property(t => t.Id)
                .HasColumnName("Id")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(t => t.ClaimOptionTypeId).HasColumnName("ClaimOptionTypeId")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);

            Property(t => t.ClaimNumber).HasColumnName("ClaimNumber");
            Property(t => t.ClaimOptionPercentage).HasColumnName("ClaimOptionPercentage");

            MapToStoredProcedures(s =>
                s.Insert(i => i.HasName("SP_InsertClaimOptionEntry", Constants.DatabaseOwnerSchemaName)
                    .Parameter(p => p.ClaimNumber, "ClaimNumber")
                    .Parameter(p => p.ClaimOptionType, "ClaimOptionType")
                    .Parameter(p => p.ClaimOptionPercentage, "ClaimOptionPercentage")
                    ));
        }
    }
}
