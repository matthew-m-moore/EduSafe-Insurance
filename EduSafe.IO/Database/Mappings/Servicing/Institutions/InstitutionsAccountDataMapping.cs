using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using EduSafe.Common;
using EduSafe.IO.Database.Entities.Servicing.Institutions;

namespace EduSafe.IO.Database.Mappings.Servicing.Institutions
{
    public class InstitutionsAccountDataMapping : EntityTypeConfiguration<InstitutionsAccountDataEntity>
    {
        public InstitutionsAccountDataMapping()
        {
            HasKey(t => t.InstitutionsAccountNumber);

            ToTable("InstitutionsAccountData", Constants.DatabaseOwnerSchemaName);

            Property(t => t.InstitutionsAccountNumber)
                .HasColumnName("InstitutionsAccountNumber")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(t => t.EmailsSetId).HasColumnName("EmailsSetId")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);

            Property(t => t.FolderPath).HasColumnName("FolderPath");
            Property(t => t.InstitutionName).HasColumnName("InstitutionName");
            Property(t => t.Address1).HasColumnName("Address1");
            Property(t => t.Address2).HasColumnName("Address2");
            Property(t => t.Address3).HasColumnName("Address3");
            Property(t => t.City).HasColumnName("City");
            Property(t => t.State).HasColumnName("State");
            Property(t => t.ZipCode).HasColumnName("ZipCode");

            MapToStoredProcedures(s =>
                s.Insert(i => i.HasName("SP_InsertInstitutionsAccountData", Constants.DatabaseOwnerSchemaName)
                    .Parameter(p => p.FolderPath, "FolderPath")
                    .Parameter(p => p.InstitutionName, "InstitutionName")
                    .Parameter(p => p.Address1, "Address1")
                    .Parameter(p => p.Address2, "Address2")
                    .Parameter(p => p.Address3, "Address3")
                    .Parameter(p => p.City, "City")
                    .Parameter(p => p.State, "State")
                    .Parameter(p => p.ZipCode, "ZipCode")
                    ));
        }
    }
}
