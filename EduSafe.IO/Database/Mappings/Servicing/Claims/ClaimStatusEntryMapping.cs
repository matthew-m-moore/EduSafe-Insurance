using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using EduSafe.Common;
using EduSafe.IO.Database.Entities.Servicing.Claims;

namespace EduSafe.IO.Database.Mappings.Servicing.Claims
{
    public class ClaimStatusEntryMapping : EntityTypeConfiguration<ClaimStatusEntryEntity>
    {
        public ClaimStatusEntryMapping()
        {
            HasKey(t => t.Id);

            ToTable("ClaimStatusEntry", Constants.DatabaseOwnerSchemaName);

            Property(t => t.Id)
                .HasColumnName("Id")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(t => t.ClaimStatusTypeId).HasColumnName("ClaimStatusTypeId")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);

            Property(t => t.ClaimNumber).HasColumnName("ClaimNumber");
            Property(t => t.IsClaimApproved).HasColumnName("IsClaimApproved");

            MapToStoredProcedures(s =>
                s.Insert(i => i.HasName("SP_InsertClaimStatusEntry", Constants.DatabaseOwnerSchemaName)
                    .Parameter(p => p.ClaimNumber, "ClaimNumber")
                    .Parameter(p => p.ClaimStatusType, "ClaimStatusType")
                    .Parameter(p => p.IsClaimApproved, "IsClaimApproved")
                    ));
        }
    }
}
