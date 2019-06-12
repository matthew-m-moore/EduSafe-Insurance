using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using EduSafe.Common;
using EduSafe.IO.Database.Entities.Servicing.Individuals;

namespace EduSafe.IO.Database.Mappings.Servicing.Individuals
{
    public class InsureesNextPaymentAndBalanceInformationMapping : EntityTypeConfiguration<InsureesNextPaymentAndBalanceInformationEntity>
    {
        public InsureesNextPaymentAndBalanceInformationMapping()
        {
            HasKey(t => t.Id);

            ToTable("InsureesNextPaymentAndBalanceInformation", Constants.DatabaseOwnerSchemaName);

            Property(t => t.Id)
                .HasColumnName("Id")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(t => t.NextPaymentStatusTypeId).HasColumnName("NextPaymentStatusTypeId")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);

            Property(t => t.AccountNumber).HasColumnName("AccountNumber");
            Property(t => t.NextPaymentAmount).HasColumnName("NextPaymentAmount");
            Property(t => t.NextPaymentDate).HasColumnName("NextPaymentDate");
            Property(t => t.CurrentBalance).HasColumnName("CurrentBalance");

            MapToStoredProcedures(s =>
                s.Insert(i => i.HasName("SP_InsertInsureesNextPaymentAndBalanceInformation", Constants.DatabaseOwnerSchemaName)
                    .Parameter(p => p.AccountNumber, "AccountNumber")
                    .Parameter(p => p.NextPaymentAmount, "NextPaymentAmount")
                    .Parameter(p => p.NextPaymentDate, "NextPaymentDate")
                    .Parameter(p => p.CurrentBalance, "CurrentBalance")
                    .Parameter(p => p.NextPaymentStatusType, "NextPaymentStatusType")
                    ));
        }
    }
}
