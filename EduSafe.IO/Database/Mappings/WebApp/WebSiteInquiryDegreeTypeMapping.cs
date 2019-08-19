using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using EduSafe.Common;
using EduSafe.IO.Database.Entities.WebApp;

namespace EduSafe.IO.Database.Mappings.WebApp
{
    public class WebSiteInquiryDegreeTypeMapping : EntityTypeConfiguration<WebSiteInquiryDegreeTypeEntity>
    {
        public WebSiteInquiryDegreeTypeMapping()
        {
            HasKey(t => t.Id);

            ToTable("WebSiteInquiryDegreeType", Constants.DatabaseOwnerSchemaName);

            Property(t => t.Id)
                .HasColumnName("Id")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(t => t.DegreeType).HasColumnName("DegreeType");
        }
    }
}
