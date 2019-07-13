using System.Data.Entity;
using EduSafe.IO.Database.Contexts;

namespace EduSafe.IO.Database
{
    public static class DatabaseContextRetriever
    {
        public static WebSiteInquiryContext GetWebSiteInquiryContext()
        {
            var databaseConnectionString = DatabaseConnectionSettings.CreateDatabaseConnectionString<WebSiteInquiryContext>();
            return new WebSiteInquiryContext(databaseConnectionString);
        }

        public static ServicingDataContext GetServicingDataContext()
        {
            var databaseConnectionString = DatabaseConnectionSettings.CreateDatabaseConnectionString<ServicingDataContext>();
            return new ServicingDataContext(databaseConnectionString);
        }

        public static void LogDatabaseActvityForDebug(DbContext databaseContext)
        {
            databaseContext.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
        }
    }
}
