using ChessCloneBack.DAL.Enums;

namespace ChessCloneBack.BLL.Interfaces
{
    /// <summary>
    /// Defines a service for registering and authenticating clients
    /// </summary>
    public interface IAuthenticationService
    {
        /// <summary>
        /// Checks if username and password are valid
        /// </summary>
        /// <param name="returnMessage">Returns a token if the credentials are valid, otherwise returns a message explaining why the credentials are invalid</param>
        /// <returns>true if valid, else false</returns>
        bool IsValidCredentials(string username, string password, out string returnMessage);
        
        /// <summary>
        /// Registers new clients in database. Throws an exception if credentials are invalid.
        /// </summary>
        void AddNewCredentials(string username, string password, string email, BoardStyle style, bool premium, bool notify);
        bool IsNameAvailable(string username);
    }

}
