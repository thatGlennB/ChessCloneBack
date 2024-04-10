using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessCloneBack.DAL.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public required string UserName { get; set; }
        public required byte[] PasswordSaltHash { get; set; }

    }
}
