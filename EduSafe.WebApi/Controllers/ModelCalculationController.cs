using EduSafe.WebApi.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Dream.WebApp.Controllers
{
    [RoutePrefix("api/calculate")]
    public class ModelCalculationController : ApiController
    {
        private static List<ModelOutputEntry> _modelOutputEntries = new List<ModelOutputEntry>();

        // GET: api/calculate
        [Route("")]
        [HttpGet]
        public ModelOutputSummary Get()
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
        public ModelOutputSummary Put(ModelInputEntry modelInputEntry)
        {
            var modelOutputSummary = new ModelOutputSummary
            {
                OutputTitle = "The Model Calculation Submission Works",
                ModelOutputEntries = _modelOutputEntries
            };

            return modelOutputSummary;
        }
    }
}