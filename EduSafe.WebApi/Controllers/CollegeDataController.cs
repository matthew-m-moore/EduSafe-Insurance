using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using EduSafe.Core.BusinessLogic.Containers;
using EduSafe.WebApi.Adapters;
using EduSafe.WebApi.Models.Institutions;

namespace EduSafe.WebApi.Controllers
{
    [RoutePrefix("api/data")]
    public class CollegeDataController : ApiController
    {
        // GET: api/data/search-colleges/?collegeName={collegeName}
        [Route("search-colleges")]
        [HttpGet]
        public List<string> SearchColleges([FromUri]string collegeName)
        {
            if (string.IsNullOrWhiteSpace(collegeName)) return new List<string>();

            var collegesList = CollegeDataAdapter.CollegeDataRepository.CollegesList;
            var matchingColleges = collegesList.Where(c => c.ToUpper().Contains(collegeName)).ToList();

            return matchingColleges;
        }

        // GET: api/data/search-collegeMajor/?description={description}
        [Route("search-collegeMajor")]
        [HttpGet]
        public List<string> SearchCollegeMajors([FromUri]string description)
        {
            if (string.IsNullOrWhiteSpace(description)) return new List<string>();

            var collegeMajorsDictionary = CollegeDataAdapter.CollegeDataRepository.CollegeMajorDataDictionary;
            var matchingCollegeMajors = collegeMajorsDictionary.Keys
                .Where(k => k.ToUpper().Contains(description)).ToList();

            return matchingCollegeMajors;
        }

        // GET: api/data/collegeMajors
        [Route("collegeMajors")]
        [HttpGet]
        public List<CollegeMajorData> GetCollegeMajors()
        {
            var collegeMajorsDictionary = CollegeDataAdapter.CollegeDataRepository.CollegeMajorDataDictionary;
            var collegeMajors = collegeMajorsDictionary.Values.ToList();

            return collegeMajors;
        }

        // PUT: api/data/institutional
        [Route("institutional")]
        [HttpPut]
        public InstitutionalGradData GetInstitutionalData(InstitutionInputEntry institutionInputEntry)
        {
            var institutionalDataDictionary = CollegeDataAdapter.CollegeDataRepository.InstitutionalDataDictionary;

            var degreeType = institutionInputEntry.TwoYearOrFourYearSchool;
            var collegeType = institutionInputEntry.PublicOrPrivateSchool;
            var institutionalGradData = institutionalDataDictionary[degreeType][collegeType];

            return institutionalGradData;
        }
    }
}