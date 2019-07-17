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
            Property(t => t.IsPrimary).HasColumnName("IsPrimary");

            MapToStoredProcedures(s =>
                s.Insert(i => i.HasName("SP_InsertEmails", Constants.IndividualCustomerSchemaName)
                    .Parameter(p => p.EmailsSetId, "EmailsSetId")
                    .Parameter(p => p.Email, "Email")
                    ));

            MapToStoredProcedures(s =>
                s.Update(i => i.HasName("SP_UpdateEmails", Constants.IndividualCustomerSchemaName)
                    .Parameter(p => p.Id, "Id")            
                    .Parameter(p => p.EmailsSetId, "EmailsSetId")
                    .Parameter(p => p.Email, "Email")
                    .Parameter(p => p.IsPrimary, "IsPrimary")
                    ));

            MapToStoredProcedures(s =>
                s.Delete(i => i.HasName("SP_DeleteEmails", Constants.IndividualCustomerSchemaName)
                    .Parameter(p => p.Id, "Id")
                    ));
        }
    }
}
