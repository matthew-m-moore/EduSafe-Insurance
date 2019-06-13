using EduSafe.Common;
using EduSafe.IO.Database.Entities.Servicing;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace EduSafe.IO.Database.Mappings
{
    public class EmailsSetMapping : EntityTypeConfiguration<EmailsSetEntity>
    {
        public EmailsSetMapping()
        {
            HasKey(t => t.SetId);

            ToTable("EmailsSet", Constants.IndividualCustomerSchemaName);

            Property(t => t.SetId)
                .HasColumnName("SetId")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            MapToStoredProcedures(s =>
                s.Insert(i => i.HasName("SP_InsertEmailsSet", Constants.IndividualCustomerSchemaName)));
        }
    }
}
