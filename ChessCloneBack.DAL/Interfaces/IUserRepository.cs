using ChessCloneBack.DAL.Entities;

namespace ChessCloneBack.DAL.Interfaces
{
    /// <summary>
    /// Defines database access actions for the table containing user data
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Add new user. Throws exception if username is not unique.
        /// </summary>
        void Add(User user);
        User? GetByUsername(string userName);
    }
}
