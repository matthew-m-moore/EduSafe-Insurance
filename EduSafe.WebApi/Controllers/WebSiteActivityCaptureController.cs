using System.Web.Http;
using EduSafe.IO.Database;
using EduSafe.IO.Database.Entities;
using EduSafe.WebApi.Models;

namespace EduSafe.WebApi.Controllers
{
    [RoutePrefix("api/activity")]
    public class WebSiteActivityCaptureController : ApiController
    {
        // POST: api/activity/{ipAddress}
        [Route("record")]
        [HttpPost]
        public bool RecordIpAddress(ActivityInputEntry activityInputEntry)
        {
            using (var databaseContext = DatabaseContextRetriever.GetWebSiteInquiryContext())
            {
                var webSiteInquiryIpAddressEntity = new WebSiteInquiryIpAddressEntity { IpAddress = activityInputEntry.IpAddress };
                databaseContext.WebSiteInquiryIpAddressEntities.Add(webSiteInquiryIpAddressEntity);
                databaseContext.SaveChanges();
            }

            return true;
        }

        // POST: api/activity/calc
        [Route("calc")]
        [HttpPost]
        public bool RecordCalculationInputs(ModelInputEntry modelInputEntry)
        {
            using (var databaseContext = DatabaseContextRetriever.GetWebSiteInquiryContext())
            {
                databaseContext.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
                var webSiteInquiryAnswersToQuestionsEntity = new WebSiteInquiryAnswersToQuestionsEntity
                {
                    IpAddress = modelInputEntry.IpAddress,
                    CollegeName = modelInputEntry.CollegeName,
                    CollegeType = modelInputEntry.PublicOrPrivateSchool,
                    Major = modelInputEntry.CollegeMajor,
                    CollegeStartDate = modelInputEntry.CollegeStartDate,
                    GraduationDate = modelInputEntry.ExpectedGraduationDate,
                    AnnualCoverage = modelInputEntry.IncomeCoverageAmount
                };

                databaseContext.WebSiteInquiryAnswersToQuestionsEntities.Add(webSiteInquiryAnswersToQuestionsEntity);
                databaseContext.SaveChanges();
            }

            return true;
        }

        // POST: api/activity/email-inquiry
        [Route("email-inquiry")]
        [HttpPost]
        public bool RecordInquiryEmailAddress(InquiryEmailEntry inquiryEmailEntry)
        {
            using (var databaseContext = DatabaseContextRetriever.GetWebSiteInquiryContext())
            {
                var webSiteInquiryEmailAddressEntity = new WebSiteInquiryEmailAddressEntity
                {
                    IpAddress = inquiryEmailEntry.IpAddress,
                    EmailAddress = inquiryEmailEntry.ContactAddress,
                    ContactName = inquiryEmailEntry.ContactName,
                    OptOut = false
                };

                databaseContext.WebSiteInquiryEmailAddressEntities.Add(webSiteInquiryEmailAddressEntity);
                databaseContext.SaveChanges();
            }

            return true;
        }

        // POST: api/activity/email-results
        [Route("email-results")]
        [HttpPost]
        public bool RecordResultsEmailAddress(ResultsEmailEntry resultsEmailEntry)
        {
            using (var databaseContext = DatabaseContextRetriever.GetWebSiteInquiryContext())
            {
                var webSiteInquiryEmailAddressEntity = new WebSiteInquiryEmailAddressEntity
                {
                    IpAddress = resultsEmailEntry.ModelInputEntry.IpAddress,
                    EmailAddress = resultsEmailEntry.RecipientAddress,
                    ContactName = resultsEmailEntry.RecipientName,
                    OptOut = false
                };

                databaseContext.WebSiteInquiryEmailAddressEntities.Add(webSiteInquiryEmailAddressEntity);
                databaseContext.SaveChanges();
            }

            return true;
        }
    }
}