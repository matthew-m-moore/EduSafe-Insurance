using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using EduSafe.Common;
using EduSafe.IO.Database.Entities.Servicing.Claims;

namespace EduSafe.IO.Database.Mappings.Servicing.Claims
{
    public class ClaimDocumentEntryMapping : EntityTypeConfiguration<ClaimDocumentEntryEntity>
    {
        public ClaimDocumentEntryMapping()
        {
            HasKey(t => t.Id);

            ToTable("ClaimDocumentEntry", Constants.DatabaseOwnerSchemaName);

            Property(t => t.Id)
                .HasColumnName("Id")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(t => t.ClaimNumber).HasColumnName("ClaimNumber");
            Property(t => t.FileName).HasColumnName("FileName");
            Property(t => t.FileType).HasColumnName("FileType");
            Property(t => t.FileVerificationStatusTypeId).HasColumnName("FileVerificationStatusTypeId");
            Property(t => t.IsVerified).HasColumnName("IsVerified");
            Property(t => t.UploadDate).HasColumnName("UploadDate");
            Property(t => t.ExpirationDate).HasColumnName("ExpirationDate");

            MapToStoredProcedures(s =>
                s.Insert(i => i.HasName("SP_InsertClaimDocumentEntry", Constants.DatabaseOwnerSchemaName)
                    .Parameter(p => p.ClaimNumber, "ClaimNumber")
                    .Parameter(p => p.FileName, "FileName")
                    .Parameter(p => p.FileType, "FileType")
                    .Parameter(p => p.FileVerificationStatusTypeId, "FileVerificationStatusTypeId")
                    .Parameter(p => p.IsVerified, "IsVerified")
                    .Parameter(p => p.UploadDate, "UploadDate")
                    .Parameter(p => p.ExpirationDate, "ExpirationDate")
                    ));
        }
    }
}
