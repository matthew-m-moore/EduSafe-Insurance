namespace EduSafe.WebApi.Models
{
    public class AuthenticationPackage
    {
        public string CustomerIdentifier { get; set; }
        public string EncryptedPassword { get; set; }
    }
}