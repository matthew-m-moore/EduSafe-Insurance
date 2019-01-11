using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EduSafe.Common.Utilities
{
    public static class BuildIdentifierUtility
    {
        private const string _buildIdentifierTextFile = "EduSafe.Common.Resources.ChangeSet.txt";

        private static string _completeBuildId;
        public static string CompleteBuildId
        {
            get
            {
                if (_completeBuildId == null)
                {
                    ReadBuildIdFromManifest();
                }

                return _completeBuildId;
            }
        }

        private static string _abbreviateBuildId;
        public static string AbbreviatedBuildId
        {
            get
            {
                if (_abbreviateBuildId == null)
                {
                    ReadBuildIdFromManifest();
                }

                return _abbreviateBuildId;
            }
        }

        private static void ReadBuildIdFromManifest()
        {
            try
            {
                var manifestInfoStream = Assembly
                    .GetExecutingAssembly()
                    .GetManifestResourceStream(_buildIdentifierTextFile);

                var textFileReader = new StreamReader(manifestInfoStream);

                var buildIdentifierData = textFileReader.ReadToEnd().Split('\n');
                var buildIdentifierComponents = buildIdentifierData;

                _completeBuildId = buildIdentifierComponents[0];
                _abbreviateBuildId = buildIdentifierComponents[2];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
