using System.Collections.Generic;
using System.Web.Http;
using EduSafe.WebApi.Models;
using EduSafe.WebApi.Adapters;

namespace EduSafe.WebApi.Controllers
{
    [RoutePrefix("api/articles")]
    public class AriticlesLoadingController : ApiController
    {
        // GET: api/articles
        [Route("")]
        [HttpGet]
        public List<ArticleInformationEntry> LoadArticles()
        {
            var articleInformationEntries = new List<ArticleInformationEntry>();
            var articlesList = ArticlesLoadingAdapter.ArticleInformationEntries;
            return articlesList;            
        }
    }
}