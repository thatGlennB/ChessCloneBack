using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using ChessCloneBack.BLL.Interfaces;
using ChessCloneBack.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using ChessCloneBack.Test.Infrastructure;

namespace ChessCloneBack.Test.Mocks
{
    public class MockUserRepository:IUserRepository
    {
        public MockUserRepository()
        { }
    

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
                    PasswordSaltHash = TestUtil.FakePasswordHash
                }
            };
            return dummyDB.SingleOrDefault(o => o.UserName == username);
        }
    }
}
