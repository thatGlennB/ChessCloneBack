using ChessCloneBack.DAL.Interfaces;
using ChessCloneBack.Test.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace ChessCloneBack.Test.Mocks
{
    public class MockUserRepository : IUserRepository
    {
        public void Add(User user)
        {
            if (user.UserName == TestUtil.ExistingUserName)
                throw new DbUpdateException();
        }

        public User? GetByUsername(string username)
        {
            List<User> dummyDB = new List<User>
            {
                new User
                {
                    UserName = TestUtil.ExistingUserName,
                    PasswordSaltHash = TestUtil.FakePasswordHash,
                    ELO = 800,
                    Email = "a@b.c",
                    Style = 0,
                    Premium = false,
                    PremiumExpiration = null,
                    Notify = false
                }
            };
            return dummyDB.SingleOrDefault(o => o.UserName == username);
        }
    }
}
