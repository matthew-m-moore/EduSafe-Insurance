using System;
using System.Data.Entity;
using EduSafe.IO.Database;
using EduSafe.IO.Database.Contexts;
using EduSafe.IO.Database.Entities.WebApp;

namespace EduSafe.Core.Savers
{
    public class WebSiteInquiryDatabaseSaver : DatabaseSaver
    {
        public override DbContext DatabaseContext => DatabaseContextRetriever.GetWebSiteInquiryContext();

        public void SaveIpAddress(string ipAddress)
        {
            using (var databaseContext = DatabaseContext as WebSiteInquiryContext)
            {
                var webSiteInquiryIpAddressEntity = new WebSiteInquiryIpAddressEntity { IpAddress = ipAddress};
                databaseContext.WebSiteInquiryIpAddressEntities.Add(webSiteInquiryIpAddressEntity);
                databaseContext.SaveChanges();
            }
        }

        public void SaveCalculationInputs(
            string ipAddress,
            string collegeName,
            string collegeType,
            string collegeMajor,
            DateTime collegeStartDate,
            DateTime graduationDate,
            double annualCoverage)
        {
            using (var databaseContext = DatabaseContext as WebSiteInquiryContext)
            {
                var webSiteInquiryAnswersToQuestionsEntity = new WebSiteInquiryAnswersToQuestionsEntity
                {
                    IpAddress = ipAddress,
                    CollegeName = collegeName,
                    CollegeType = collegeType,
                    Major = collegeMajor,
                    CollegeStartDate = collegeStartDate,
                    GraduationDate = graduationDate,
                    AnnualCoverage = annualCoverage
                };

                databaseContext.WebSiteInquiryAnswersToQuestionsEntities.Add(webSiteInquiryAnswersToQuestionsEntity);
                databaseContext.SaveChanges();
            }
        }

        public void SaveInstitutionalCalculationInputs(
            string ipAddress,
            string collegeName,
            string collegeType,
            string degreeType,
            int studentsPerStartingClass,
            double graduationWithinYears1,
            double graduationWithinYears2,
            double graduationWithinYears3,
            double averageLoanDebtAtGraduation,
            double startingCohortDefaultRate)
        {
            using (var databaseContext = DatabaseContext as WebSiteInquiryContext)
            {
                var webSiteInquiryInstitutionalInputsEntity = new WebSiteInquiryInstitutionalInputsEntity
                {
                    IpAddress = ipAddress,
                    CollegeName = collegeName,
                    CollegeType = collegeType,
                    DegreeType = degreeType,
                    StudentsPerStartingClass = studentsPerStartingClass,
                    GraduationWithinYears1 = graduationWithinYears1,
                    GraduationWithinYears2 = graduationWithinYears2,
                    GraduationWithinYears3 = graduationWithinYears3,
                    AverageLoanDebtAtGraduation = averageLoanDebtAtGraduation,
                    StartingCohortDefaultRate = startingCohortDefaultRate
                };

                databaseContext.WebSiteInquiryInstitutionalInputsEntities.Add(webSiteInquiryInstitutionalInputsEntity);
                databaseContext.SaveChanges();
            }
        }

        public void SaveEmailAddress(
            string ipAddress,
            string contactAddress,
            string contactName)
        {
            using (var databaseContext = DatabaseContext as WebSiteInquiryContext)
            {
                var webSiteInquiryEmailAddressEntity = new WebSiteInquiryEmailAddressEntity
                {
                    IpAddress = ipAddress,
                    EmailAddress = contactAddress,
                    ContactName = contactName,
                    OptOut = false
                };

                databaseContext.WebSiteInquiryEmailAddressEntities.Add(webSiteInquiryEmailAddressEntity);
                databaseContext.SaveChanges();
            }
        }
    }
}
