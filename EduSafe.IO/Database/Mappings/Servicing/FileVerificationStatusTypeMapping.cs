using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using EduSafe.Common;
using EduSafe.IO.Database.Entities.Servicing;

namespace EduSafe.IO.Database.Mappings.Servicing
{
    public class FileVerificationStatusTypeMapping : EntityTypeConfiguration<FileVerificationStatusTypeEntity>
    {
        public FileVerificationStatusTypeMapping()
        {
            HasKey(t => t.Id);

            ToTable("FileVerificationStatusType", Constants.DatabaseOwnerSchemaName);

            Property(t => t.Id)
                .HasColumnName("Id")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(t => t.FileVerificationStatusType).HasColumnName("FileVerificationStatusType");
            Property(t => t.Description).HasColumnName("Description");
        }
    }
}
