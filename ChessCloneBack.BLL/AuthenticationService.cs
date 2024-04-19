using ChessCloneBack.BLL.Infrastructure;
using ChessCloneBack.BLL.Interfaces;
using ChessCloneBack.DAL.Entities;
using ChessCloneBack.DAL.Enums;
using ChessCloneBack.DAL.Interfaces;
using Microsoft.Extensions.Configuration;

namespace ChessCloneBack.BLL
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserRepository _repo;
        private IConfiguration _config;
        public AuthenticationService(IConfiguration config, IUserRepository repo)
        {
            _repo = repo;
            _config = config;
        }

        public void AddNewCredentials(string username, string password, string email, BoardStyle style, bool premium, bool notify)
        {
            if (!IsNameAvailable(username))
                throw new ArgumentException("Username has already been taken", nameof(username));

            _repo.Add(new User
            {
                UserName = username,
                PasswordSaltHash = AuthenticationUtil.GetPasswordHash(password),
                ELO = 800,
                Email = email,
                Style = (int)style,
                Premium = premium,
                PremiumExpiration = premium ? DateTime.Now.AddDays(7) : null,
                Notify = notify
            });
        }

        public bool IsNameAvailable(string username)
        {
            User? user = _repo.GetByUsername(username);
            return user == null;
        }

        public bool IsValidCredentials(string username, string password, out string returnMessage)
        {
            if (IsNameAvailable(username))
            {
                returnMessage = "User not found";
                return false;
            }

            User? user = _repo.GetByUsername(username);
            if (user != null && !AuthenticationUtil.IsValid(user.PasswordSaltHash, password))
            {
                returnMessage = "Wrong password";
                return false;
            }


            // TODO: what claims do I need?
            // token for new user
            // token for premium
            // token during gameplay - low priority
            JWTBuilder builder = new JWTBuilder();
            builder.Key = _config["Jwt:Key"] ?? "";
            builder.Audience = _config["Jwt:Audience"] ?? "";
            builder.Issuer = _config["Jwt:Issuer"] ?? "";
            builder.Expiration = DateTime.Now.AddDays(14);
            //builder.AddClaim(ClaimTypes.Role, )

            returnMessage = builder.Build();
            return true;
        }
    }
}
