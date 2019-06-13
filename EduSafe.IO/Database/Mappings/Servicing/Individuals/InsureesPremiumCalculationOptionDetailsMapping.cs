using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using EduSafe.Common;
using EduSafe.IO.Database.Entities.Servicing.Individuals;

namespace EduSafe.IO.Database.Mappings.Servicing.Individuals
{
    public class InsureesPremiumCalculationOptionDetailsMapping : EntityTypeConfiguration<InsureesPremiumCalculationOptionDetailsEntity>
    {
        public InsureesPremiumCalculationOptionDetailsMapping()
        {
            HasKey(t => t.Id);

            ToTable("InsureesPremiumCalculationOptionDetails", Constants.DatabaseOwnerSchemaName);

            Property(t => t.Id)
                .HasColumnName("Id")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(t => t.OptionTypeId).HasColumnName("OptionTypeId")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);

            Property(t => t.InsureesPremiumCalculationOptionDetailsSetId).HasColumnName("InsureesPremiumCalculationOptionDetailsSetId");
            Property(t => t.OptionPercentage).HasColumnName("OptionPercentage");

            MapToStoredProcedures(s =>
                s.Insert(i => i.HasName("SP_InsertInsureesPremiumCalculationOptionDetails", Constants.DatabaseOwnerSchemaName)
                    .Parameter(p => p.InsureesPremiumCalculationOptionDetailsSetId, "InsureesPremiumCalculationOptionDetailsSetId")
                    .Parameter(p => p.OptionType, "OptionType")
                    .Parameter(p => p.OptionPercentage, "OptionPercentage")
                    ));
        }
    }
}
