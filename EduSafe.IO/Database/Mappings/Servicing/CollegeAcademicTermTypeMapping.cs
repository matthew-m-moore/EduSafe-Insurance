using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using EduSafe.Common;
using EduSafe.IO.Database.Entities.Servicing;

namespace EduSafe.IO.Database.Mappings.Servicing
{
    public class CollegeAcademicTermTypeMapping : EntityTypeConfiguration<CollegeAcademicTermTypeEntity>
    {
        public CollegeAcademicTermTypeMapping()
        {
            HasKey(t => t.Id);

            ToTable("CollegeAcademicTermType", Constants.DatabaseOwnerSchemaName);

            Property(t => t.Id)
                .HasColumnName("Id")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(t => t.CollegeAcademicTermType).HasColumnName("CollegeAcademicTermType");
            Property(t => t.Description).HasColumnName("Description");
        }
    }
}
