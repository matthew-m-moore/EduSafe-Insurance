using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using EduSafe.Common;
using EduSafe.IO.Database.Entities.Servicing.Institutions;

namespace EduSafe.IO.Database.Mappings.Servicing.Institutions
{
    public class InstitutionsPaymentHistoryEntryMapping : EntityTypeConfiguration<InstitutionsPaymentHistoryEntryEntity>
    {
        public InstitutionsPaymentHistoryEntryMapping()
        {
            HasKey(t => t.Id);

            ToTable("InstitutionsPaymentHistoryEntry", Constants.DatabaseOwnerSchemaName);

            Property(t => t.Id)
                .HasColumnName("Id")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(t => t.InstitutionsAccountNumber).HasColumnName("InstitutionsAccountNumber");
            Property(t => t.PaymentAmount).HasColumnName("PaymentAmount");
            Property(t => t.PaymentDate).HasColumnName("PaymentDate");
            Property(t => t.PaymentStatusTypeId).HasColumnName("PaymentStatusTypeId");
            Property(t => t.PaymentComments).HasColumnName("PaymentComments");

            MapToStoredProcedures(s =>
                s.Insert(i => i.HasName("SP_InsertInstitutionsPaymentHistoryEntry", Constants.DatabaseOwnerSchemaName)
                    .Parameter(p => p.InstitutionsAccountNumber, "InstitutionsAccountNumber")
                    .Parameter(p => p.PaymentAmount, "PaymentAmount")
                    .Parameter(p => p.PaymentDate, "PaymentDate")
                    .Parameter(p => p.PaymentStatusTypeId, "PaymentStatusTypeId")
                    .Parameter(p => p.PaymentComments, "PaymentComments")
                    ));
        }
    }
}
