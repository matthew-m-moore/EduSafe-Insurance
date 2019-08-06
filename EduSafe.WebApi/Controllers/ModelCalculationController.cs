using System.Collections.Generic;
using System.Web.Http;
using EduSafe.WebApi.Adapters;
using EduSafe.WebApi.Models.Individuals;
using EduSafe.WebApi.Models.Institutions;

namespace Dream.WebApp.Controllers
{
    [RoutePrefix("api/calculate")]
    public class ModelCalculationController : ApiController
    {
        private static List<ModelOutputEntry> _modelOutputEntries = new List<ModelOutputEntry>();

        // PUT: api/calculate/premiums
        [Route("premiums")]
        [HttpPut]
        public ModelOutputSummary CalculatePremiums(ModelInputEntry modelInputEntry)
        {
            var premiumComputationAdapter = new IndividualPremiumComputationAdapter();
            var modelOutputSummary = premiumComputationAdapter.RunPremiumComputationScenarios(modelInputEntry);

            return modelOutputSummary;
        }

        // PUT: api/calculate/institutional-premiums
        [Route("institutional-premiums")]
        [HttpPut]
        public InstitutionOutputSummary CalculateInstitutionalPremiums(InstitutionInputEntry institutionInputEntry)
        {
            var premiumComputationAdapter = new InstitutionalPremiumComputationAdapter();
            var institutionOutputSummary = premiumComputationAdapter.RunPremiumComputationScenarios(institutionInputEntry);

            return institutionOutputSummary;
        }
    }
}