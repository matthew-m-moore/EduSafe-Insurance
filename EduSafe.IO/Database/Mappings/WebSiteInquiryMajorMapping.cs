using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using EduSafe.Common;
using EduSafe.IO.Database.Entities;

namespace EduSafe.IO.Database.Mappings
{
    public class WebSiteInquiryMajorMapping : EntityTypeConfiguration<WebSiteInquiryMajorEntity>
    {
        public WebSiteInquiryMajorMapping()
        {
            HasKey(t => t.Id);

            ToTable("WebSiteInquiryMajor", Constants.DatabaseOwnerSchemaName);

            Property(t => t.Id)
                .HasColumnName("Id")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(t => t.Major).HasColumnName("Major");

            MapToStoredProcedures(s =>
                s.Insert(i => i.HasName("SP_InsertWebSiteInquiryMajor", Constants.DatabaseOwnerSchemaName)
                    .Parameter(p => p.Major, "Major")
                    ));
        }
    }
}
