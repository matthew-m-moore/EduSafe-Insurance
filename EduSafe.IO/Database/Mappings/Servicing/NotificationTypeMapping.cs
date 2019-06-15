using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using EduSafe.Common;
using EduSafe.IO.Database.Entities.Servicing;

namespace EduSafe.IO.Database.Mappings.Servicing
{
    public class NotificationTypeMapping : EntityTypeConfiguration<NotificationTypeEntity>
    {
        public NotificationTypeMapping()
        {
            HasKey(t => t.Id);

            ToTable("NotificationType", Constants.DatabaseOwnerSchemaName);

            Property(t => t.Id)
                .HasColumnName("Id")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(t => t.NotificationType).HasColumnName("NotificationType");
            Property(t => t.Description).HasColumnName("Description");
        }
    }
}
