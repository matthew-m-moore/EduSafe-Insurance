using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using EduSafe.Common;
using EduSafe.IO.Database.Entities.Servicing.Individuals;

namespace EduSafe.IO.Database.Mappings.Servicing.Individuals
{
    public class InsureesPremiumCalculationDetailsSetMapping : EntityTypeConfiguration<InsureesPremiumCalculationDetailsSetEntity>
    {
        public InsureesPremiumCalculationDetailsSetMapping()
        {
            HasKey(t => t.SetId);

            ToTable("InsureesPremiumCalculationDetailsSet", Constants.DatabaseOwnerSchemaName);

            Property(t => t.SetId)
                .HasColumnName("SetId")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(t => t.AccountNumber).HasColumnName("AccountNumber");
            Property(t => t.InsureesPremiumCalculationDetailsId).HasColumnName("InsureesPremiumCalculationDetailsId");
            Property(t => t.InsureesPremiumCalculationOptionDetailsSetId).HasColumnName("InsureesPremiumCalculationOptionDetailsSetId");
            Property(t => t.Description).HasColumnName("Description");

            MapToStoredProcedures(s =>
                s.Insert(i => i.HasName("SP_InsertInsureesPremiumCalculationDetailsSet", Constants.DatabaseOwnerSchemaName)
                    .Parameter(p => p.AccountNumber, "AccountNumber")
                    .Parameter(p => p.InsureesPremiumCalculationDetailsId, "InsureesPremiumCalculationDetailsId")
                    .Parameter(p => p.InsureesPremiumCalculationOptionDetailsSetId, "InsureesPremiumCalculationOptionDetailsSetId")
                    .Parameter(p => p.Description, "Description")
                    ));
        }
    }
}
