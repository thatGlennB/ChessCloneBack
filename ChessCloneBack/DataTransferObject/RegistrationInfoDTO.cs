using ChessCloneBack.DAL.Enums;

namespace ChessCloneBack.API.DataTransferObject
{
    public class RegistrationInfoDTO
    {
        public required string UserName { get; set; }
        public required string Password { get; set; }
        public required string Email { get; set; }
        public required BoardStyle ChessboardTheme { get; set; }
        public required bool Premium { get; set; }
        public required bool Notify { get; set; }
    }
}
