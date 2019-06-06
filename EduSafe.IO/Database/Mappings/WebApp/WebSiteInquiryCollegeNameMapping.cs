using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using EduSafe.Common;
using EduSafe.IO.Database.Entities.WebApp;

namespace EduSafe.IO.Database.Mappings.WebApp
{
    public class WebSiteInquiryCollegeNameMapping : EntityTypeConfiguration<WebSiteInquiryCollegeNameEntity>
    {
        public WebSiteInquiryCollegeNameMapping()
        {
            HasKey(t => t.Id);

            ToTable("WebSiteInquiryCollegeName", Constants.DatabaseOwnerSchemaName);

            Property(t => t.Id)
                .HasColumnName("Id")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(t => t.CollegeName).HasColumnName("CollegeName");

            MapToStoredProcedures(s =>
                s.Insert(i => i.HasName("SP_InsertWebSiteInquiryCollegeName", Constants.DatabaseOwnerSchemaName)
                    .Parameter(p => p.CollegeName, "CollegeName")
                    ));
        }
    }
}
