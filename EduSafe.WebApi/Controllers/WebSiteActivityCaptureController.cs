using System.Web.Http;
using EduSafe.Core.Savers;
using EduSafe.WebApi.Models;

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
    }
}