﻿using System.Data.Entity;
using EduSafe.IO.Database.Entities;
using EduSafe.IO.Database.Mappings;

namespace EduSafe.IO.Database.Contexts
{
    public class WebSiteInquiryContext : DbContext
    {
        public WebSiteInquiryContext(string connectionString) : base(connectionString)
        {
            System.Data.Entity.Database.SetInitializer<WebSiteInquiryContext>(null);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new WebSiteInquiryAnswersToQuestionsMapping());
            modelBuilder.Configurations.Add(new WebSiteInquiryCollegeNameMapping());
            modelBuilder.Configurations.Add(new WebSiteInquiryCollegeTypeMapping());
            modelBuilder.Configurations.Add(new WebSiteInquiryEmailAddressMapping());
            modelBuilder.Configurations.Add(new WebSiteInquiryIpAddressMapping());
            modelBuilder.Configurations.Add(new WebSiteInquiryMajorMapping());
        }

        public virtual DbSet<WebSiteInquiryAnswersToQuestionsEntity> WebSiteInquiryAnswersToQuestionsEntities { get; set; }
        public virtual DbSet<WebSiteInquiryCollegeNameEntity> WebSiteInquiryCollegeNameEntities { get; set; }
        public virtual DbSet<WebSiteInquiryCollegeTypeEntity> WebSiteInquiryCollegeTypeEntities { get; set; }
        public virtual DbSet<WebSiteInquiryEmailAddressEntity> WebSiteInquiryEmailAddressEntities { get; set; }
        public virtual DbSet<WebSiteInquiryIpAddressEntity> WebSiteInquiryIpAddressEntities { get; set; }
        public virtual DbSet<WebSiteInquiryMajorEntity> WebSiteInquiryMajorEntities { get; set; }
    }
}