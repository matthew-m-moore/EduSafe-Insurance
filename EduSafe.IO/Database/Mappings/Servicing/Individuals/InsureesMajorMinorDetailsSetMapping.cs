using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using EduSafe.Common;
using EduSafe.IO.Database.Entities.Servicing.Individuals;

namespace EduSafe.IO.Database.Mappings.Servicing.Individuals
{
    public class InsureesMajorMinorDetailsSetMapping : EntityTypeConfiguration<InsureesMajorMinorDetailsSetEntity>
    {
        public InsureesMajorMinorDetailsSetMapping()
        {
            HasKey(t => t.SetId);

            ToTable("InsureesMajorMinorDetailsSet", Constants.DatabaseOwnerSchemaName);

            Property(t => t.SetId)
                .HasColumnName("SetId")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(t => t.AccountNumber).HasColumnName("AccountNumber");
            Property(t => t.Description).HasColumnName("Description");

            MapToStoredProcedures(s =>
                s.Insert(i => i.HasName("SP_InsertInsureesMajorMinorDetailsSet", Constants.DatabaseOwnerSchemaName)
                    .Parameter(p => p.AccountNumber, "AccountNumber")
                    .Parameter(p => p.Description, "Description")
                    ));
        }
    }
}
