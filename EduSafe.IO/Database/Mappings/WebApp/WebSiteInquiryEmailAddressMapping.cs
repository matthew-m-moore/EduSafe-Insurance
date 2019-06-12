using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using EduSafe.Common;
using EduSafe.IO.Database.Entities.WebApp;

namespace EduSafe.IO.Database.Mappings.WebApp
{
    public class WebSiteInquiryEmailAddressMapping : EntityTypeConfiguration<WebSiteInquiryEmailAddressEntity>
    {
        public WebSiteInquiryEmailAddressMapping()
        {
            HasKey(t => t.Id);

            ToTable("WebSiteInquiryEmailAddress", Constants.DatabaseOwnerSchemaName);

            Property(t => t.Id)
                .HasColumnName("Id")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(t => t.EmailAddress).HasColumnName("EmailAddress");
            Property(t => t.IpAddress).HasColumnName("IpAddress");
            Property(t => t.OptOut).HasColumnName("OptOut");
            Property(t => t.ContactName).HasColumnName("ContactName");

            MapToStoredProcedures(s =>
                s.Insert(i => i.HasName("SP_InsertWebSiteInquiryEmailAddress", Constants.DatabaseOwnerSchemaName)
                    .Parameter(p => p.EmailAddress, "EmailAddress")
                    .Parameter(p => p.IpAddress, "IpAddress")
                    .Parameter(p => p.OptOut, "OptOut")
                    .Parameter(p => p.ContactName, "ContactName")
                    ));
        }
    }
}
