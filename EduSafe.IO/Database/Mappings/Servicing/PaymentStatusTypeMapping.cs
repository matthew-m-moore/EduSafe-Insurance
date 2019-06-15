using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using EduSafe.Common;
using EduSafe.IO.Database.Entities.Servicing;

namespace EduSafe.IO.Database.Mappings.Servicing
{
    public class PaymentStatusTypeMapping : EntityTypeConfiguration<PaymentStatusTypeEntity>
    {
        public PaymentStatusTypeMapping()
        {
            HasKey(t => t.Id);

            ToTable("PaymentStatusType", Constants.DatabaseOwnerSchemaName);

            Property(t => t.Id)
                .HasColumnName("Id")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(t => t.PaymentStatusType).HasColumnName("PaymentStatusType");
            Property(t => t.Description).HasColumnName("Description");
        }
    }
}
