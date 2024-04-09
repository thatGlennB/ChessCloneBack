namespace ChessCloneBack.DataTransferObject
{
    public class UserModel
    {
        public required string UserName { get; set; }
        public required string Password { get; set; }
        public string? Email { get; set; }
    }
}
