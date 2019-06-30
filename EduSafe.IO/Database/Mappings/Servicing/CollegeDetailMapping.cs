using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using EduSafe.Common;
using EduSafe.IO.Database.Entities.Servicing;

namespace EduSafe.IO.Database.Mappings.Servicing
{
    public class CollegeDetailMapping : EntityTypeConfiguration<CollegeDetailEntity>
    {
        public CollegeDetailMapping()
        {
            HasKey(t => t.Id);

            ToTable("CollegeDetail", Constants.DatabaseOwnerSchemaName);

            Property(t => t.Id)
                .HasColumnName("Id")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(t => t.CollegeName).HasColumnName("CollegeName");
            Property(t => t.CollegeTypeId).HasColumnName("CollegeTypeId");
            Property(t => t.CollegeAcademicTermTypeId).HasColumnName("CollegeAcademicTermTypeId");

            MapToStoredProcedures(s =>
                s.Insert(i => i.HasName("SP_InsertCollegeDetail", Constants.DatabaseOwnerSchemaName)
                    .Parameter(p => p.CollegeName, "CollegeName")
                    .Parameter(p => p.CollegeTypeId, "CollegeTypeId")
                    .Parameter(p => p.CollegeAcademicTermTypeId, "CollegeAcademicTermTypeId")
                    ));
        }
    }
}
