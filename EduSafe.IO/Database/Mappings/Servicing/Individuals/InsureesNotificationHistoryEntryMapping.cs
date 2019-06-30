using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using EduSafe.Common;
using EduSafe.IO.Database.Entities.Servicing.Individuals;

namespace EduSafe.IO.Database.Mappings.Servicing.Individuals
{
    public class InsureesNotificationHistoryEntryMapping : EntityTypeConfiguration<InsureesNotificationHistoryEntryEntity>
    {
        public InsureesNotificationHistoryEntryMapping()
        {
            HasKey(t => t.Id);

            ToTable("InsureesNotificationHistoryEntry", Constants.DatabaseOwnerSchemaName);

            Property(t => t.Id)
                .HasColumnName("Id")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(t => t.AccountNumber).HasColumnName("AccountNumber");
            Property(t => t.NotificationTypeId).HasColumnName("NotificationTypeId");
            Property(t => t.NotificationDate).HasColumnName("NotificationDate");

            MapToStoredProcedures(s =>
                s.Insert(i => i.HasName("SP_InsertInsureesNotificationHistoryEntry", Constants.DatabaseOwnerSchemaName)
                    .Parameter(p => p.AccountNumber, "AccountNumber")
                    .Parameter(p => p.NotificationTypeId, "NotificationTypeId")
                    .Parameter(p => p.NotificationDate, "NotificationDate")                    
                    ));
        }
    }
}
