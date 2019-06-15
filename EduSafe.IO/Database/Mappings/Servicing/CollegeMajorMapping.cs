using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using EduSafe.Common;
using EduSafe.IO.Database.Entities.Servicing;

namespace EduSafe.IO.Database.Mappings.Servicing
{
    public class CollegeMajorMapping : EntityTypeConfiguration<CollegeMajorEntity>
    {
        public CollegeMajorMapping()
        {
            HasKey(t => t.Id);

            ToTable("CollegeMajor", Constants.DatabaseOwnerSchemaName);

            Property(t => t.Id)
                .HasColumnName("Id")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(t => t.CollegeMajor).HasColumnName("CollegeMajor");

            MapToStoredProcedures(s =>
                s.Insert(i => i.HasName("SP_InsertCollegeMajor", Constants.DatabaseOwnerSchemaName)
                    .Parameter(p => p.CollegeMajor, "CollegeMajor")
                    ));
        }
    }
}
