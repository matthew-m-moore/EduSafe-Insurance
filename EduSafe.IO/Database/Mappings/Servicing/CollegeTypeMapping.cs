using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using EduSafe.Common;
using EduSafe.IO.Database.Entities.Servicing;

namespace EduSafe.IO.Database.Mappings.Servicing
{
    public class CollegeTypeMapping : EntityTypeConfiguration<CollegeTypeEntity>
    {
        public CollegeTypeMapping()
        {
            HasKey(t => t.Id);

            ToTable("CollegeType", Constants.DatabaseOwnerSchemaName);

            Property(t => t.Id)
                .HasColumnName("Id")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(t => t.CollegeType).HasColumnName("CollegeType");
            Property(t => t.Description).HasColumnName("Description");
        }
    }
}
