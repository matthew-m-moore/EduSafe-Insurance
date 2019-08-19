using System.Web.Http;
using EduSafe.Core.Savers;
using EduSafe.WebApi.Models;
using EduSafe.WebApi.Models.Individuals;
using EduSafe.WebApi.Models.Institutions;

namespace EduSafe.WebApi.Controllers
{
    [RoutePrefix("api/activity")]
    public class WebSiteActivityCaptureController : ApiController
    {
        // POST: api/activity/record
        [Route("record")]
        [HttpPost]
        internal bool RecordIpAddress(ActivityInputEntry activityInputEntry)
        {
            var webSiteInquiryDatabaseSaver = new WebSiteInquiryDatabaseSaver();
            webSiteInquiryDatabaseSaver.SaveIpAddress(activityInputEntry.IpAddress);

            return true;
        }

        // POST: api/activity/calc
        [Route("calc")]
        [HttpPost]
        public bool RecordCalculationInputs(ModelInputEntry modelInputEntry)
        {
            var webSiteInquiryDatabaseSaver = new WebSiteInquiryDatabaseSaver();
            webSiteInquiryDatabaseSaver.SaveCalculationInputs(
                modelInputEntry.IpAddress,
                modelInputEntry.CollegeName,
                modelInputEntry.PublicOrPrivateSchool,
                modelInputEntry.CollegeMajor,
                modelInputEntry.CollegeStartDate,
                modelInputEntry.ExpectedGraduationDate,
                modelInputEntry.IncomeCoverageAmount);

            return true;
        }

        // POST: api/activity/calc-institution
        [Route("calc-institution")]
        [HttpPost]
        public bool RecordInstitutionCalculationInputs(InstitutionInputEntry institutionInputEntry)
        {
            var webSiteInquiryDatabaseSaver = new WebSiteInquiryDatabaseSaver();
            webSiteInquiryDatabaseSaver.SaveInstitutionalCalculationInputs(
                institutionInputEntry.IpAddress,
                institutionInputEntry.CollegeName,
                institutionInputEntry.PublicOrPrivateSchool,
                institutionInputEntry.TwoYearOrFourYearSchool,
                institutionInputEntry.StudentsPerStartingClass,
                institutionInputEntry.GraduationWithinYears1,
                institutionInputEntry.GraduationWithinYears2,
                institutionInputEntry.GraduationWithinYears3,
                institutionInputEntry.AverageLoanDebtAtGraduation,
                institutionInputEntry.StartingCohortDefaultRate);

            return true;
        }

        // POST: api/activity/email-inquiry
        [Route("email-inquiry")]
        [HttpPost]
        public bool RecordInquiryEmailAddress(InquiryEmailEntry inquiryEmailEntry)
        {
            var webSiteInquiryDatabaseSaver = new WebSiteInquiryDatabaseSaver();
            webSiteInquiryDatabaseSaver.SaveEmailAddress(
                inquiryEmailEntry.IpAddress,
                inquiryEmailEntry.ContactAddress,
                inquiryEmailEntry.ContactName);

            return true;
        }

        // POST: api/activity/email-results
        [Route("email-results")]
        [HttpPost]
        public bool RecordResultsEmailAddress(ResultsEmailEntry resultsEmailEntry)
        {
            var webSiteInquiryDatabaseSaver = new WebSiteInquiryDatabaseSaver();
            webSiteInquiryDatabaseSaver.SaveEmailAddress(
                resultsEmailEntry.ModelInputEntry.IpAddress,
                resultsEmailEntry.RecipientAddress,
                resultsEmailEntry.RecipientName);

            return true;
        }

        // POST: api/activity/email-institution
        [Route("email-institution")]
        [HttpPost]
        public bool RecordInstitutionResultEmailAddress(InstitutionResultEmailEntry institutionResultEmailEntry)
        {
            var webSiteInquiryDatabaseSaver = new WebSiteInquiryDatabaseSaver();
            webSiteInquiryDatabaseSaver.SaveEmailAddress(
                institutionResultEmailEntry.InstitutionInputEntry.IpAddress,
                institutionResultEmailEntry.RecipientAddress,
                institutionResultEmailEntry.RecipientName);

            return true;
        }
    }
}