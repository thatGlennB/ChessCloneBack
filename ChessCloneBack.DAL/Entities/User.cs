using System.ComponentModel.DataAnnotations;

namespace ChessCloneBack.DAL.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public required string UserName { get; set; }
        public required byte[] PasswordSaltHash { get; set; }
        public required int ELO { get; set; }
        public required string Email { get; set; }
        public required int Style { get; set; }
        public required bool Premium { get; set; }
        public DateTime? PremiumExpiration { get; set; }
        public required bool Notify { get; set; }

    }
}
