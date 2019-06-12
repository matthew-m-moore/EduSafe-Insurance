using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using EduSafe.Common;
using EduSafe.IO.Database.Entities.Servicing.Individuals;

namespace EduSafe.IO.Database.Mappings.Servicing.Individuals
{
    public class InsureesGraduationVerificationDetailsMapping : EntityTypeConfiguration<InsureesGraduationVerificationDetailsEntity>
    {
        public InsureesGraduationVerificationDetailsMapping()
        {
            HasKey(t => t.Id);

            ToTable("InsureesGraduationVerificationDetails", Constants.DatabaseOwnerSchemaName);

            Property(t => t.Id)
                .HasColumnName("Id")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(t => t.AccountNumber).HasColumnName("AccountNumber");
            Property(t => t.IsVerified).HasColumnName("IsVerified");
            Property(t => t.VerificationDate).HasColumnName("VerificationDate");
            Property(t => t.Comments).HasColumnName("Comments");

            MapToStoredProcedures(s =>
                s.Insert(i => i.HasName("SP_InsertInsureesGraduationVerificationDetails", Constants.DatabaseOwnerSchemaName)
                    .Parameter(p => p.AccountNumber, "AccountNumber")
                    .Parameter(p => p.IsVerified, "IsVerified")
                    .Parameter(p => p.VerificationDate, "VerificationDate")
                    .Parameter(p => p.Comments, "Comments")
                    ));
        }
    }
}
