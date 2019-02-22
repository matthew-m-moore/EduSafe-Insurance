using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EduSafe.IO.Database.Contexts;

namespace EduSafe.IO.Database
{
    public static class DatabaseContextSettings
    {
        private const int _databaseConnectionTimeoutInMilliseconds = 300;

        // Note, new contexts can be added to this dictionary as needed
        public static Dictionary<Type, SqlConnectionStringBuilder> DatabaseContextConnectionsDictionary =
            new Dictionary<Type, SqlConnectionStringBuilder>
            {
                [typeof(WebSiteInquiryContext)] = new SqlConnectionStringBuilder
                {
                    Encrypt = true,
                    PersistSecurityInfo = false,
                    MultipleActiveResultSets = false,
                    TrustServerCertificate = false,
                    
                    ConnectTimeout = _databaseConnectionTimeoutInMilliseconds,

                    DataSource = "tcp:" + InputOutput.Default.WebsiteInquiryServerName + "," + InputOutput.Default.WebsiteInquiryPort,
                    InitialCatalog = InputOutput.Default.WebsiteInquiryDatabaseName,
                    UserID = InputOutput.Default.WebsiteInquiryUserName,
                    Password = InputOutput.Default.WebsiteInquiryPassword,
                },
            };
    }
}
