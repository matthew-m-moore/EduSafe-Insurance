namespace EduSafe.IO.Interfaces
{
    public interface IAccountData
    {
        long AccountNumber { get; }
        string FolderPath { get; }
        int EmailsSetId { get; }
    }
}
