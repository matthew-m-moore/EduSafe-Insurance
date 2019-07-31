using System.Collections.Generic;
using System.Web.Http;
using EduSafe.WebApi.Adapters;
using EduSafe.WebApi.Models;

namespace Dream.WebApp.Controllers
{
    [RoutePrefix("api/calculate")]
    public class ModelCalculationController : ApiController
    {
        private static List<ModelOutputEntry> _modelOutputEntries = new List<ModelOutputEntry>();

        // GET: api/calculate
        [Route("")]
        [HttpGet]
        public ModelOutputSummary CheckApiTest()
        {
            var modelOutputSummary = new ModelOutputSummary
            {
                OutputTitle = "The Model Calculation API Works",
                ModelOutputEntries = _modelOutputEntries
            };

            return modelOutputSummary;
        }

        // PUT: api/calculate/premiums
        [Route("premiums")]
        [HttpPut]
        public ModelOutputSummary CalculatePremiums(ModelInputEntry modelInputEntry)
        {
            var premiumComputationAdapter = new IndividualPremiumComputationAdapter();
            var modelOutputSummary = premiumComputationAdapter.RunPremiumComputationScenarios(modelInputEntry);

            return modelOutputSummary;
        }
    }
}