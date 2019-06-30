using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using EduSafe.Common;
using EduSafe.IO.Database.Entities.Servicing.Claims;

namespace EduSafe.IO.Database.Mappings.Servicing.Claims
{
    public class ClaimPaymentEntryMapping : EntityTypeConfiguration<ClaimPaymentEntryEntity>
    {
        public ClaimPaymentEntryMapping()
        {
            HasKey(t => t.Id);

            ToTable("ClaimPaymentEntry", Constants.DatabaseOwnerSchemaName);

            Property(t => t.Id)
                .HasColumnName("Id")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(t => t.ClaimNumber).HasColumnName("ClaimNumber");
            Property(t => t.ClaimPaymentAmount).HasColumnName("ClaimPaymentAmount");
            Property(t => t.ClaimPaymentDate).HasColumnName("ClaimPaymentDate");
            Property(t => t.ClaimPaymentStatusTypeId).HasColumnName("ClaimPaymentStatusTypeId");
            Property(t => t.ClaimPaymentComments).HasColumnName("ClaimPaymentComments");

            MapToStoredProcedures(s =>
                s.Insert(i => i.HasName("SP_InsertClaimPaymentEntry", Constants.DatabaseOwnerSchemaName)
                    .Parameter(p => p.ClaimNumber, "ClaimNumber")
                    .Parameter(p => p.ClaimPaymentAmount, "ClaimPaymentAmount")
                    .Parameter(p => p.ClaimPaymentDate, "ClaimPaymentDate")
                    .Parameter(p => p.ClaimPaymentStatusTypeId, "ClaimPaymentStatusTypeId")
                    .Parameter(p => p.ClaimPaymentComments, "ClaimPaymentComments")
                    ));
        }
    }
}
