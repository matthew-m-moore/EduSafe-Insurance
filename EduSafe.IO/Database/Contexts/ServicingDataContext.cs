using System.Data.Entity;
using EduSafe.IO.Database.Entities.Servicing;
using EduSafe.IO.Database.Mappings.WebApp;

namespace EduSafe.IO.Database.Contexts
{
    public class ServicingDataContext : DbContext
    {
        public ServicingDataContext(string connectionString) : base(connectionString)
        {
            System.Data.Entity.Database.SetInitializer<ServicingDataContext>(null);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

        }
    }
}
