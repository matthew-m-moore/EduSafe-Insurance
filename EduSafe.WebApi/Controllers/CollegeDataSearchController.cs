using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using EduSafe.Core.BusinessLogic.Containers;
using EduSafe.WebApi.Adapters;

namespace EduSafe.WebApi.Controllers
{
    [RoutePrefix("api/search")]
    public class CollegeDataSearchController : ApiController
    {
        // GET: api/search/colleges?collegeName={collegeName}
        [Route("colleges")]
        [HttpGet]
        public List<string> SearchColleges([FromUri]string collegeName)
        {
            if (string.IsNullOrWhiteSpace(collegeName)) return new List<string>();

            var collegesList = CollegeDataAdapter.CollegeMajorDataRepository.CollegesList;
            var matchingColleges = collegesList.Where(c => c.ToUpper().Contains(collegeName)).ToList();

            return matchingColleges;
        }

        // GET: api/search/collegeMajor?description={description}
        [Route("collegeMajor")]
        [HttpGet]
        public List<string> SearchCollegeMajors([FromUri]string description)
        {
            if (string.IsNullOrWhiteSpace(description)) return new List<string>();

            var collegeMajorsDictionary = CollegeDataAdapter.CollegeMajorDataRepository.CollegeMajorDataDictionary;
            var matchingCollegeMajors = collegeMajorsDictionary.Keys
                .Where(k => k.ToUpper().Contains(description)).ToList();

            return matchingCollegeMajors;
        }

        // GET: api/search/collegeMajors
        [Route("collegeMajors")]
        [HttpGet]
        public List<CollegeMajorData> GetCollegeMajors()
        {
            var collegeMajorsDictionary = CollegeDataAdapter.CollegeMajorDataRepository.CollegeMajorDataDictionary;
            var collegeMajors = collegeMajorsDictionary.Values.ToList();

            return collegeMajors;
        }
    }
}