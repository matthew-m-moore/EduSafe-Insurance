using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using EduSafe.Common;
using EduSafe.IO.Database.Entities.Servicing.Claims;

namespace EduSafe.IO.Database.Mappings.Servicing.Claims
{
    public class ClaimAccountEntryMapping : EntityTypeConfiguration<ClaimAccountEntryEntity>
    {
        public ClaimAccountEntryMapping()
        {
            HasKey(t => t.ClaimNumber);

            ToTable("ClaimAccountEntry", Constants.DatabaseOwnerSchemaName);

            Property(t => t.ClaimNumber)
                .HasColumnName("ClaimNumber")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(t => t.AccountNumber).HasColumnName("AccountNumber");

            MapToStoredProcedures(s =>
                s.Insert(i => i.HasName("SP_InsertClaimAccountEntry", Constants.DatabaseOwnerSchemaName)
                    .Parameter(p => p.AccountNumber, "AccountNumber")
                    ));
        }
    }
}
