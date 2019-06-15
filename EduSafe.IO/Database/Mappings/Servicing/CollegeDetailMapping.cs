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

            Property(t => t.CollegeTypeId).HasColumnName("CollegeTypeId")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
            Property(t => t.CollegeAcademicTermTypeId).HasColumnName("CollegeAcademicTermTypeId")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);

            Property(t => t.CollegeName).HasColumnName("CollegeName");

            MapToStoredProcedures(s =>
                s.Insert(i => i.HasName("SP_InsertCollegeDetail", Constants.DatabaseOwnerSchemaName)
                    .Parameter(p => p.CollegeName, "CollegeName")
                    .Parameter(p => p.CollegeType, "CollegeType")
                    .Parameter(p => p.CollegeAcademicTermType, "CollegeAcademicTermType")
                    ));
        }
    }
}
