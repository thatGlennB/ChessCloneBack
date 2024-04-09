using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessCloneBack.BLL
{
    public class DummyPasswordDTO
    {
        public required string UserName { get; set; }
        public required byte[] PasswordSaltedHash { get; set; }
    }
}
