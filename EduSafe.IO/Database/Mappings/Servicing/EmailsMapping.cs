using EduSafe.Common;
using EduSafe.IO.Database.Entities.Servicing;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace EduSafe.IO.Database.Mappings.Servicing
{
    public class EmailsMapping : EntityTypeConfiguration<EmailsEntity>
    {
        public EmailsMapping()
        {
            HasKey(t => t.Id);

            ToTable("Emails", Constants.IndividualCustomerSchemaName);

            Property(t => t.Id)
                .HasColumnName("Id")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(t => t.EmailsSetId).HasColumnName("EmailsSetId");
            Property(t => t.Email).HasColumnName("Email");

            MapToStoredProcedures(s =>
                s.Insert(i => i.HasName("SP_InsertEmails", Constants.IndividualCustomerSchemaName)
                    .Parameter(p => p.EmailsSetId, "EmailsSetId")
                    .Parameter(p => p.Email, "Email")
                    ));
        }
    }
}
