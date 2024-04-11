namespace ChessCloneBack.BLL.Interfaces
{
    public interface IAuthenticationService
    {
        bool IsValidCredentials(string username, string password, out string returnMessage);
        void AddNewCredentials(string username, string password);
        bool IsNameAvailable(string username);
    }

}
