namespace ChessCloneBack.BLL.Interfaces
{
    public interface IEmailService
    {
        void Send();
        Task SendAsync();
    }
}
