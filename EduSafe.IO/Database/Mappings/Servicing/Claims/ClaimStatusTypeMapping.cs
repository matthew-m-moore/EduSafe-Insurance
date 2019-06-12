using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using EduSafe.Common;
using EduSafe.IO.Database.Entities.Servicing.Claims;

namespace EduSafe.IO.Database.Mappings.Servicing.Claims
{
    public class ClaimStatusTypeMapping : EntityTypeConfiguration<ClaimStatusTypeEntity>
    {
        public ClaimStatusTypeMapping()
        {
            HasKey(t => t.Id);

            ToTable("ClaimStatusType", Constants.DatabaseOwnerSchemaName);

            Property(t => t.Id)
                .HasColumnName("Id")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(t => t.ClaimStatusType).HasColumnName("ClaimStatusType");
            Property(t => t.Description).HasColumnName("Description");
        }
    }
}
