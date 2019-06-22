using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using EduSafe.Common;
using EduSafe.IO.Database.Entities.Servicing.Individuals;

namespace EduSafe.IO.Database.Mappings.Servicing.Individuals
{
    public class InsureesPaymentHistoryEntryMapping : EntityTypeConfiguration<InsureesPaymentHistoryEntryEntity>
    {
        public InsureesPaymentHistoryEntryMapping()
        {
            HasKey(t => t.Id);

            ToTable("InsureesPaymentHistoryEntry", Constants.DatabaseOwnerSchemaName);

            Property(t => t.Id)
                .HasColumnName("Id")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(t => t.PaymentStatusTypeId).HasColumnName("PaymentStatusTypeId")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);

            Property(t => t.AccountNumber).HasColumnName("AccountNumber");
            Property(t => t.PaymentAmount).HasColumnName("PaymentAmount");
            Property(t => t.PaymentDate).HasColumnName("PaymentDate");
            Property(t => t.PaymentComments).HasColumnName("PaymentComments");

            MapToStoredProcedures(s =>
                s.Insert(i => i.HasName("SP_InsertInsureesPaymentHistoryEntry", Constants.DatabaseOwnerSchemaName)
                    .Parameter(p => p.AccountNumber, "AccountNumber")
                    .Parameter(p => p.PaymentAmount, "PaymentAmount")
                    .Parameter(p => p.PaymentDate, "PaymentDate")
                    .Parameter(p => p.PaymentStatusType, "PaymentStatusType")
                    .Parameter(p => p.PaymentComments, "PaymentComments")
                    ));
        }
    }
}
