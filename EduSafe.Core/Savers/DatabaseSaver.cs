using System.Data.Entity;

namespace EduSafe.Core.Savers
{
    public abstract class DatabaseSaver
    {
        public abstract DbContext DatabaseContext { get; }
    }
}
