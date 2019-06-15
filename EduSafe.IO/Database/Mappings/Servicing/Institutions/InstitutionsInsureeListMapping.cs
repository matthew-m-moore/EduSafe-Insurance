using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using EduSafe.Common;
using EduSafe.IO.Database.Entities.Servicing.Institutions;

namespace EduSafe.IO.Database.Mappings.Servicing.Institutions
{
    public class InstitutionsInsureeListMapping : EntityTypeConfiguration<InstitutionsInsureeListEntity>
    {
        public InstitutionsInsureeListMapping()
        {
            HasKey(t => t.Id);

            ToTable("InstitutionsInsureeList", Constants.DatabaseOwnerSchemaName);

            Property(t => t.Id)
                .HasColumnName("Id")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(t => t.InstitutionsAccountNumber).HasColumnName("InstitutionsAccountNumber");
            Property(t => t.InsureeAccountNumber).HasColumnName("InsureeAccountNumber");

            MapToStoredProcedures(s =>
                s.Insert(i => i.HasName("SP_InsertInstitutionsInsureeList", Constants.DatabaseOwnerSchemaName)
                    .Parameter(p => p.InstitutionsAccountNumber, "InstitutionsAccountNumber")
                    .Parameter(p => p.InsureeAccountNumber, "InsureeAccountNumber")
                    ));
        }
    }
}
