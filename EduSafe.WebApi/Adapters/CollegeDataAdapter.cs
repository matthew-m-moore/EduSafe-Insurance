using System.IO;
using System.Reflection;
using EduSafe.Core.Repositories.Excel;

namespace EduSafe.WebApi.Adapters
{
    /// <summary>
    /// This singleton class will serve as a data adapter for the entire Web API. Hopefully, this will result in speed gains
    /// during serach filtering and computations on the website.
    /// </summary>
    internal static class CollegeDataAdapter
    {
        private const string _websiteCollegeDataFile = "EduSafe.WebApi.App_Data.EduSafe-Website-College-Data.xlsx";
        private static Stream _websiteCollegeDataFileStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(_websiteCollegeDataFile);

        internal static CollegeDataRepository CollegeMajorDataRepository;

        internal static void LoadDataRepository()
        {
            CollegeMajorDataRepository = new CollegeDataRepository(_websiteCollegeDataFileStream);
        }
    }
}