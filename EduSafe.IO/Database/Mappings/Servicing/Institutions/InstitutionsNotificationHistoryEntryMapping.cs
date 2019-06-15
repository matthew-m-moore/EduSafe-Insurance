﻿using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using EduSafe.Common;
using EduSafe.IO.Database.Entities.Servicing.Institutions;

namespace EduSafe.IO.Database.Mappings.Servicing.Institutions
{
    public class InstitutionsNotificationHistoryEntryMapping : EntityTypeConfiguration<InstitutionsNotificationHistoryEntryEntity>
    {
        public InstitutionsNotificationHistoryEntryMapping()
        {
            HasKey(t => t.Id);

            ToTable("InstitutionsNotificationHistoryEntry", Constants.DatabaseOwnerSchemaName);

            Property(t => t.Id)
                .HasColumnName("Id")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(t => t.NotificationTypeId).HasColumnName("NotificationTypeId")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);

            Property(t => t.InstitutionsAccountNumber).HasColumnName("InstitutionsAccountNumber");
            Property(t => t.NotificationDate).HasColumnName("NotificationDate");

            MapToStoredProcedures(s =>
                s.Insert(i => i.HasName("SP_InsertInstitutionsNotificationHistoryEntry", Constants.DatabaseOwnerSchemaName)
                    .Parameter(p => p.InstitutionsAccountNumber, "InstitutionsAccountNumber")
                    .Parameter(p => p.NotificationType, "NotificationType")
                    .Parameter(p => p.NotificationDate, "NotificationDate")
                    ));
        }
    }
}
