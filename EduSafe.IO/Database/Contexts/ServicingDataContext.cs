using System.Data.Entity;
using EduSafe.IO.Database.Entities.Servicing;
using EduSafe.IO.Database.Entities.Servicing.Claims;
using EduSafe.IO.Database.Entities.Servicing.Individuals;
using EduSafe.IO.Database.Entities.Servicing.Institutions;
using EduSafe.IO.Database.Mappings.Servicing;
using EduSafe.IO.Database.Mappings.Servicing.Claims;
using EduSafe.IO.Database.Mappings.Servicing.Individuals;
using EduSafe.IO.Database.Mappings.Servicing.Institutions;

namespace EduSafe.IO.Database.Contexts
{
    public class ServicingDataContext : DbContext
    {
        public ServicingDataContext(string connectionString) : base(connectionString)
        {
            System.Data.Entity.Database.SetInitializer<ServicingDataContext>(null);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ClaimAccountEntryMapping());
            modelBuilder.Configurations.Add(new ClaimDocumentEntryMapping());
            modelBuilder.Configurations.Add(new ClaimOptionEntryMapping());
            modelBuilder.Configurations.Add(new ClaimPaymentEntryMapping());
            modelBuilder.Configurations.Add(new ClaimStatusEntryMapping());
            modelBuilder.Configurations.Add(new ClaimStatusTypeMapping());

            modelBuilder.Configurations.Add(new InsureesAccountDataMapping());
            modelBuilder.Configurations.Add(new InsureesEnrollmentVerificationDetailsMapping());
            modelBuilder.Configurations.Add(new InsureesGraduationVerificationDetailsMapping());
            modelBuilder.Configurations.Add(new InsureesMajorMinorDetailsMapping());
            modelBuilder.Configurations.Add(new InsureesMajorMinorDetailsSetMapping());
            modelBuilder.Configurations.Add(new InsureesNextPaymentAndBalanceInformationMapping());
            modelBuilder.Configurations.Add(new InsureesNotificationHistoryEntryMapping());
            modelBuilder.Configurations.Add(new InsureesPaymentHistoryEntryMapping());
            modelBuilder.Configurations.Add(new InsureesPremiumCalculationDetailsMapping());
            modelBuilder.Configurations.Add(new InsureesPremiumCalculationDetailsSetMapping());
            modelBuilder.Configurations.Add(new InsureesPremiumCalculationOptionDetailsMapping());
            modelBuilder.Configurations.Add(new InsureesPremiumCalculationOptionDetailsSetMapping());

            modelBuilder.Configurations.Add(new InstitutionsAccountDataMapping());
            modelBuilder.Configurations.Add(new InstitutionsInsureeListMapping());
            modelBuilder.Configurations.Add(new InstitutionsNextPaymentAndBalanceInformationMapping());
            modelBuilder.Configurations.Add(new InstitutionsNotificationHistoryEntryMapping());
            modelBuilder.Configurations.Add(new InstitutionsPaymentHistoryEntryMapping());

            modelBuilder.Configurations.Add(new CollegeAcademicTermTypeMapping());
            modelBuilder.Configurations.Add(new CollegeDetailMapping());
            modelBuilder.Configurations.Add(new CollegeMajorMapping());
            modelBuilder.Configurations.Add(new CollegeTypeMapping());

            modelBuilder.Configurations.Add(new FileVerificationStatusTypeMapping());
            modelBuilder.Configurations.Add(new NotificationTypeMapping());
            modelBuilder.Configurations.Add(new OptionTypeMapping());
            modelBuilder.Configurations.Add(new PaymentStatusTypeMapping());
        }

        public virtual DbSet<ClaimAccountEntryEntity> ClaimAccountEntryEntities { get; set; }
        public virtual DbSet<ClaimDocumentEntryEntity> ClaimDocumentEntryEntities { get; set; }
        public virtual DbSet<ClaimOptionEntryEntity> ClaimOptionEntryEntities { get; set; }
        public virtual DbSet<ClaimPaymentEntryEntity> ClaimPaymentEntryEntities { get; set; }
        public virtual DbSet<ClaimStatusEntryEntity> ClaimStatusEntryEntities { get; set; }
        public virtual DbSet<ClaimStatusTypeEntity> ClaimStatusTypeEntities { get; set; }

        public virtual DbSet<InsureesAccountDataEntity> InsureesAccountDataEntities { get; set; }
        public virtual DbSet<InsureesEnrollmentVerificationDetailsEntity> InsureesEnrollmentVerificationDetailsEntities { get; set; }
        public virtual DbSet<InsureesGraduationVerificationDetailsEntity> InsureesGraduationVerificationDetailsEntities { get; set; }
        public virtual DbSet<InsureesMajorMinorDetailsEntity> InsureesMajorMinorDetailsEntities { get; set; }
        public virtual DbSet<InsureesMajorMinorDetailsSetEntity> InsureesMajorMinorDetailsSetEntities { get; set; }
        public virtual DbSet<InsureesNextPaymentAndBalanceInformationEntity> InsureesNextPaymentAndBalanceInformationEntities { get; set; }
        public virtual DbSet<InsureesNotificationHistoryEntryEntity> InsureesNotificationHistoryEntryEntities { get; set; }
        public virtual DbSet<InsureesPaymentHistoryEntryEntity> InsureesPaymentHistoryEntryEntities { get; set; }
        public virtual DbSet<InsureesPremiumCalculationDetailsEntity> InsureesPremiumCalculationDetailsEntities { get; set; }
        public virtual DbSet<InsureesPremiumCalculationDetailsSetEntity> InsureesPremiumCalculationDetailsSetEntities { get; set; }
        public virtual DbSet<InsureesPremiumCalculationOptionDetailsEntity> InsureesPremiumCalculationOptionDetailsEntities { get; set; }
        public virtual DbSet<InsureesPremiumCalculationOptionDetailsSetEntity> InsureesPremiumCalculationOptionDetailsSetEntities { get; set; }

        public virtual DbSet<InstitutionsAccountDataEntity> InstitutionsAccountDataEntities { get; set; }
        public virtual DbSet<InstitutionsInsureeListEntity> InstitutionsInsureeListEntities { get; set; }
        public virtual DbSet<InstitutionsNextPaymentAndBalanceInformationEntity> InstitutionsNextPaymentAndBalanceInformationEntities { get; set; }
        public virtual DbSet<InstitutionsNotificationHistoryEntryEntity> InstitutionsNotificationHistoryEntryEntities { get; set; }
        public virtual DbSet<InstitutionsPaymentHistoryEntryEntity> InstitutionsPaymentHistoryEntryEntities { get; set; }

        public virtual DbSet<CollegeAcademicTermTypeEntity> CollegeAcademicTermTypeEntities { get; set; }
        public virtual DbSet<CollegeDetailEntity> CollegeDetailEntities { get; set; }
        public virtual DbSet<CollegeMajorEntity> CollegeMajorEntities { get; set; }
        public virtual DbSet<CollegeTypeEntity> CollegeTypeEntities { get; set; }

        public virtual DbSet<FileVerificationStatusTypeEntity> FileVerificationStatusTypeEntities { get; set; }
        public virtual DbSet<NotificationTypeEntity> NotificationTypeEntities { get; set; }
        public virtual DbSet<OptionTypeEntity> OptionTypeEntities { get; set; }
        public virtual DbSet<PaymentStatusTypeEntity> PaymentStatusTypeEntities { get; set; }
    }
}
