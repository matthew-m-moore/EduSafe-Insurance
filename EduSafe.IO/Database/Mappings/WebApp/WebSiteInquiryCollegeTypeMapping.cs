using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using EduSafe.Common;
using EduSafe.IO.Database.Entities.WebApp;

namespace EduSafe.IO.Database.Mappings.WebApp
{
    public class WebSiteInquiryCollegeTypeMapping : EntityTypeConfiguration<WebSiteInquiryCollegeTypeEntity>
    {
        public WebSiteInquiryCollegeTypeMapping()
        {
            HasKey(t => t.Id);

            ToTable("WebSiteInquiryCollegeType", Constants.DatabaseOwnerSchemaName);

            Property(t => t.Id)
                .HasColumnName("Id")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(t => t.CollegeType).HasColumnName("CollegeType");
        }
    }
}
