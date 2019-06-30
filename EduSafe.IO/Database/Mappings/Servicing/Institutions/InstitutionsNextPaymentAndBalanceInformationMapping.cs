using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using EduSafe.Common;
using EduSafe.IO.Database.Entities.Servicing.Institutions;

namespace EduSafe.IO.Database.Mappings.Servicing.Institutions
{
    public class InstitutionsNextPaymentAndBalanceInformationMapping : EntityTypeConfiguration<InstitutionsNextPaymentAndBalanceInformationEntity>
    {
        public InstitutionsNextPaymentAndBalanceInformationMapping()
        {
            HasKey(t => t.Id);

            ToTable("InstitutionsNextPaymentAndBalanceInformation", Constants.DatabaseOwnerSchemaName);

            Property(t => t.Id)
                .HasColumnName("Id")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(t => t.InstitutionsAccountNumber).HasColumnName("InstitutionsAccountNumber");
            Property(t => t.NextPaymentAmount).HasColumnName("NextPaymentAmount");
            Property(t => t.NextPaymentDate).HasColumnName("NextPaymentDate");
            Property(t => t.CurrentBalance).HasColumnName("CurrentBalance");
            Property(t => t.NextPaymentStatusTypeId).HasColumnName("NextPaymentStatusTypeId");

            MapToStoredProcedures(s =>
                s.Insert(i => i.HasName("SP_InsertInstitutionsNextPaymentAndBalanceInformation", Constants.DatabaseOwnerSchemaName)
                    .Parameter(p => p.InstitutionsAccountNumber, "InstitutionsAccountNumber")
                    .Parameter(p => p.NextPaymentAmount, "NextPaymentAmount")
                    .Parameter(p => p.NextPaymentDate, "NextPaymentDate")
                    .Parameter(p => p.CurrentBalance, "CurrentBalance")
                    .Parameter(p => p.NextPaymentStatusTypeId, "NextPaymentStatusTypeId")
                    ));
        }
    }
}
