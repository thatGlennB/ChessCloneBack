namespace ChessCloneBack.API.DataTransferObject
{
    public class RegistrationInfoDTO
    {
        public required string UserName { get; set; }
        public required string Password { get; set; }
        public string? Email { get; set; }
    }
}
