using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using EduSafe.Common;
using EduSafe.IO.Database.Entities.Servicing;

namespace EduSafe.IO.Database.Mappings.Servicing
{
    public class OptionTypeMapping : EntityTypeConfiguration<OptionTypeEntity>
    {
        public OptionTypeMapping()
        {
            HasKey(t => t.Id);

            ToTable("OptionType", Constants.DatabaseOwnerSchemaName);

            Property(t => t.Id)
                .HasColumnName("Id")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(t => t.OptionType).HasColumnName("OptionType");
            Property(t => t.Description).HasColumnName("Description");
        }
    }
}
