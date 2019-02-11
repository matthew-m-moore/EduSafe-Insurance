using EduSafe.WebApi.Adapters;
using System.Web.Http;
using System.Web.Http.Cors;

namespace EduSafe.WebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors();
            config.MapHttpAttributeRoutes();

            CollegeDataAdapter.LoadDataRepository();
        }
    }
}
