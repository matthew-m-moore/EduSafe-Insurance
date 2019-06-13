using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using EduSafe.Common;
using EduSafe.IO.Database.Entities.Servicing.Individuals;

namespace EduSafe.IO.Database.Mappings.Servicing.Individuals
{
    public class InsureesAccountDataMapping : EntityTypeConfiguration<InsureesAccountDataEntity>
    {
        public InsureesAccountDataMapping()
        {
            HasKey(t => t.AccountNumber);

            ToTable("InsureesAccountData", Constants.IndividualCustomerSchemaName);

            Property(t => t.AccountNumber)
                .HasColumnName("AccountNumber")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(t => t.EmailsSetId).HasColumnName("EmailsSetId")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);

            Property(t => t.FolderPath).HasColumnName("FolderPath");
            Property(t => t.FirstName).HasColumnName("FirstName");
            Property(t => t.MiddleName).HasColumnName("MiddleName");
            Property(t => t.LastName).HasColumnName("LastName");
            Property(t => t.SSN).HasColumnName("SSN");
            Property(t => t.Birthdate).HasColumnName("Birthdate");
            Property(t => t.Address1).HasColumnName("Address1");
            Property(t => t.Address2).HasColumnName("Address2");
            Property(t => t.Address3).HasColumnName("Address3");
            Property(t => t.City).HasColumnName("City");
            Property(t => t.State).HasColumnName("State");
            Property(t => t.ZipCode).HasColumnName("ZipCode");
            Property(t => t.IsInsuredViaInstitution).HasColumnName("IsInsuredViaInstitution");         

            MapToStoredProcedures(s =>
                s.Insert(i => i.HasName("SP_InsertInsureesAccountData", Constants.IndividualCustomerSchemaName)
                    .Parameter(p => p.FolderPath, "FolderPath")
                    .Parameter(p => p.FirstName, "FirstName")
                    .Parameter(p => p.MiddleName, "MiddleName")
                    .Parameter(p => p.LastName, "LastName")
                    .Parameter(p => p.SSN, "SSN")
                    .Parameter(p => p.Birthdate, "Birthdate")
                    .Parameter(p => p.Address1, "Address1")
                    .Parameter(p => p.Address2, "Address2")
                    .Parameter(p => p.Address3, "Address3")
                    .Parameter(p => p.City, "City")
                    .Parameter(p => p.State, "State")
                    .Parameter(p => p.ZipCode, "ZipCode")
                    .Parameter(p => p.IsInsuredViaInstitution, "IsInsuredViaInstitution")
                    ));
        }
    }
}
