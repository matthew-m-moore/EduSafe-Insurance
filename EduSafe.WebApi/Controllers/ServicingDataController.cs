using System.Linq;
using System.Web.Http;
using EduSafe.IO.Database;
using EduSafe.IO.Database.Contexts;
using EduSafe.IO.Database.Entities.Servicing;
using EduSafe.WebApi.Adapters;
using EduSafe.WebApi.Models;

namespace EduSafe.WebApi.Controllers
{
    [RoutePrefix("api/servicing")]
    public class ServicingDataController : ApiController
    {
        // GET: api/servicing/institution/{customerIdentifier}
        [Route("institution/{customerIdentifier}")]
        [HttpGet]
        public InstitutionProfileEntry GetInstitutionProfile(string customerIdentifier)
        {
            var institutionServicingDataAdapter = new InstitutionServicingDataAdapter();
            if (long.TryParse(customerIdentifier, out var customerNumber))
            {
                var institutionProfileEntry = institutionServicingDataAdapter.GetInstitutionProfileData(customerNumber);
                return institutionProfileEntry;
            }

            return new InstitutionProfileEntry();
        }

        // GET: api/servicing/individual/{customerIdentifier}
        [Route("individual/{customerIdentifier}")]
        [HttpGet]
        public CustomerProfileEntry GetCustomerProfile(string customerIdentifier)
        {
            var individualServicingDataAdapter = new IndividualServicingDataAdapter();
            if (long.TryParse(customerIdentifier, out var customerNumber))
            {
                var customerProfileEntry = individualServicingDataAdapter.GetIndividualProfileData(customerNumber);
                return customerProfileEntry;
            }

            return new CustomerProfileEntry();
        }

        // PUT: api/servicing/email/make-primary
        [Route("email/make-primary")]
        [HttpPut]
        public bool MakeEmailAddressPrimary(CustomerEmailEntry customerEmailEntry)
        {
            try
            {
                using (var servicingDataContext = DatabaseContextRetriever.GetServicingDataContext())
                {
                    UpdateEmailAddressToPrimary(customerEmailEntry, servicingDataContext);
                    servicingDataContext.SaveChanges();
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        // PUT: api/servicing/email/remove
        [Route("email/remove")]
        [HttpPut]
        public bool DeleteEmailAddress(CustomerEmailEntry customerEmailEntry)
        {
            try
            {
                using (var servicingDataContext = DatabaseContextRetriever.GetServicingDataContext())
                {
                    var emailEntityToRemove = servicingDataContext.EmailsEntities
                        .SingleOrDefault(e => e.Email == customerEmailEntry.EmailAddress);

                    if (emailEntityToRemove == null) return false;

                    servicingDataContext.EmailsEntities.Remove(emailEntityToRemove);
                    servicingDataContext.SaveChanges();
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        // POST: api/servicing/email/add
        [Route("email/add")]
        [HttpPost]
        public int AddNewEmailAddress(CustomerEmailEntry customerEmailEntry)
        {
            try
            {
                var emailEntityToAdd = new EmailsEntity
                {
                    EmailsSetId = customerEmailEntry.EmailSetId,
                    Email = customerEmailEntry.EmailAddress,
                    IsPrimary = customerEmailEntry.IsPrimary,
                };

                using (var servicingDataContext = DatabaseContextRetriever.GetServicingDataContext())
                {
                    if (customerEmailEntry.IsPrimary)
                        UpdateEmailAddressToPrimary(customerEmailEntry, servicingDataContext);

                    servicingDataContext.EmailsEntities.Add(emailEntityToAdd);
                    servicingDataContext.SaveChanges();
                }

                return emailEntityToAdd.Id;
            }
            catch
            {
                return 0;
            }
        }

        private void UpdateEmailAddressToPrimary(CustomerEmailEntry customerEmailEntry, ServicingDataContext servicingDataContext)
        {
            var emailEntities = servicingDataContext.EmailsEntities
                .Where(e => e.EmailsSetId == customerEmailEntry.EmailSetId).ToList();

            foreach (var emailEntity in emailEntities)
            {
                if (emailEntity.Email == customerEmailEntry.EmailAddress)
                    emailEntity.IsPrimary = true;
                else
                    emailEntity.IsPrimary = false;
            }
        }
    }
}