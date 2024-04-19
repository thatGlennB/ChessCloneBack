using ChessCloneBack.DAL.Enums;
using System.ComponentModel.DataAnnotations;

namespace ChessCloneBack.API.DataTransferObject
{
    public class RegistrationInfo
    {
        [Length(1,50)]
        public required string UserName { get; set; }
        [Length(8,50)]
        public required string Password { get; set; }
        [EmailAddress]
        public required string Email { get; set; }
        public required BoardStyle ChessboardTheme { get; set; }
        public required bool Premium { get; set; }
        public required bool Notify { get; set; }
    }
}
