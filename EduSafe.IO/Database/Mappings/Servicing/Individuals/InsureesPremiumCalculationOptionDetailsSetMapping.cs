using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using EduSafe.Common;
using EduSafe.IO.Database.Entities.Servicing.Individuals;

namespace EduSafe.IO.Database.Mappings.Servicing.Individuals
{
    public class InsureesPremiumCalculationOptionDetailsSetMapping : EntityTypeConfiguration<InsureesPremiumCalculationOptionDetailsSetEntity>
    {
        public InsureesPremiumCalculationOptionDetailsSetMapping()
        {
            HasKey(t => t.SetId);

            ToTable("InsureesPremiumCalculationOptionDetailsSet", Constants.DatabaseOwnerSchemaName);

            Property(t => t.SetId)
                .HasColumnName("SetId")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(t => t.AccountNumber).HasColumnName("AccountNumber");
            Property(t => t.Description).HasColumnName("Description");

            MapToStoredProcedures(s =>
                s.Insert(i => i.HasName("SP_InsertInsureesPremiumCalculationOptionDetailsSet", Constants.DatabaseOwnerSchemaName)
                    .Parameter(p => p.AccountNumber, "AccountNumber")
                    .Parameter(p => p.Description, "Description")
                    ));
        }
    }
}
