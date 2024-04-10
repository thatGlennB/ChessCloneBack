using ChessCloneBack.DAL.Entities;

namespace ChessCloneBack.DAL.Interfaces
{
    public interface IUserRepository
    {
        void Add(User user);
        User? Get(Func<User, bool> predicate);
    }
}
