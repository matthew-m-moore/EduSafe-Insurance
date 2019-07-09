namespace EduSafe.IO.Files
{
    public static class FileServerSettings
    {
        private const string _accountKey = "AccountKey";
        private const string _accountName = "AccountName";
        private const string _defaultEndpointsProtocol = "DefaultEndpointsProtocol";
        private const string _endpointSuffix = "EndpointSuffix";

        public static string FileShareName => InputOutput.Default.FilesShareName;

        public static string IndividualCustomersDirectory => InputOutput.Default.FilesIndividualCustomersDirectory;
        public static string InstitutionalCustomersDirectory => InputOutput.Default.FilesInstitutionalCustomersDirectory;
        public static string ReportsDirectory => InputOutput.Default.FilesReportsFolder;

        public static string AssembleConnectionString()
        {
            var defaultEndpointsProtocol = _defaultEndpointsProtocol + "=" + InputOutput.Default.FilesDefaultEndpointsProtocol + ";";
            var accountName = _accountName + "=" + InputOutput.Default.FilesAccountName + ";";
            var accountKey = _accountKey + "=" + InputOutput.Default.FilesAccountKey + "==;";
            var endpointSuffix = _endpointSuffix + "=" + InputOutput.Default.FilesEndpointSuffix;

            return string.Concat(defaultEndpointsProtocol, accountName, accountKey, endpointSuffix);
        }
    }
}
