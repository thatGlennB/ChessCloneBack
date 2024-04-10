using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessCloneBack.BLL.Interfaces
{
    public interface IAuthenticationService
    {
        bool IsValidCredentials(string username, string password, out string returnMessage);
        void AddNewCredentials(string username, string password);
    }

}
