using System.Data.Entity;

namespace EduSafe.Core.Repositories.Database
{
    public abstract class DatabaseRepository
    {
        public abstract DbContext DatabaseContext { get; }

        public DatabaseRepository() { }
    }
}
