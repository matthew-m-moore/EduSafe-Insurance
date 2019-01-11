using System;

namespace EduSafe.Common
{
    public static class Constants
    {
        public const string DatabaseOwnerSchemaName = "dbo";
        public static DateTime SqlMinDate = new DateTime(1900, 1, 1);
        public static int ProcessorCount = Environment.ProcessorCount;
    }
}
