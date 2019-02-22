using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using EduSafe.Common;
using EduSafe.IO.Database.Entities;

namespace EduSafe.IO.Database.Mappings
{
    public class WebSiteInquiryIpAddressMapping : EntityTypeConfiguration<WebSiteInquiryIpAddressEntity>
    {
        public WebSiteInquiryIpAddressMapping()
        {
            HasKey(t => t.Id);

            ToTable("WebSiteInquiryIpAddress", Constants.DatabaseOwnerSchemaName);

            Property(t => t.Id)
                .HasColumnName("Id")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(t => t.IpAddress).HasColumnName("IpAddress");

            MapToStoredProcedures(s =>
                s.Insert(i => i.HasName("SP_InsertWebSiteInquiryIpAddress", Constants.DatabaseOwnerSchemaName)
                    .Parameter(p => p.IpAddress, "IpAddress")
                    ));
        }
    }
}
