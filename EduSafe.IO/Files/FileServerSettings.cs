using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduSafe.IO.Files
{
    public static class FileServerSettings
    {
        public static string IndividualCustomersDirectoryPath => InputOutput.Default.IndividualCustomersDirectory;
        public static string InstitutionalCustomersDirectoryPath => InputOutput.Default.InstitutionalCustomersDirectory;
    }
}
