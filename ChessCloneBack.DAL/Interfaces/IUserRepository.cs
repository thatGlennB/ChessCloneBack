using ChessCloneBack.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessCloneBack.DAL.Interfaces
{
    public interface IUserRepository
    {
        void Add(User user);
        User? Get(Func<User, bool> predicate);
    }
}
