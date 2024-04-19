namespace ChessCloneBack.API.DataTransferObject
{
    public class LoginCredentialsDTO
    {
        public required string UserName { get; set; }
        public required string Password { get; set; }
    }
}
