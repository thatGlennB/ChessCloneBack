using ChessCloneBack.DAL.Entities;
using ChessCloneBack.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessCloneBack.DAL.Repositories
{
    public class UserRepository:IUserRepository
    {
        private readonly DatabaseContext _dbContext;
        public UserRepository(DatabaseContext dbContext) {
            _dbContext = dbContext;
        }
        public void Add(User user) {
            _dbContext.Add(user);
            _dbContext.SaveChanges();
        }
        public User? GetByUsername(string username) {
            return _dbContext.Users.SingleOrDefault(o => o.UserName == username);
        }
        public User? Get(Func<User, bool> predicate) 
        {
            return _dbContext.Users.SingleOrDefault(predicate);
        }
    }
}
