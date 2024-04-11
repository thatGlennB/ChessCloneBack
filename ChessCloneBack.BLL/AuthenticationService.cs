using ChessCloneBack.BLL.Infrastructure;
using ChessCloneBack.BLL.Interfaces;
using ChessCloneBack.DAL.Entities;
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

        public void AddNewCredentials(string username, string password)
        {
            if (IsNameAvailable(username))
                throw new ArgumentException("Username has already been taken", nameof(username));

            _repo.Add(new User
            {
                UserName = username,
                PasswordSaltHash = AuthenticationUtil.GetPasswordHash(password)
            });
        }

        public bool IsNameAvailable(string username)
        {
            User? user = _repo.Get(o => o.UserName == username);
            return user == null;
        }

        public bool IsValidCredentials(string username, string password, out string returnMessage)
        {
            // if no user is found, return false
            if (IsNameAvailable(username))
            {
                returnMessage = "User not found";
                return false;
            }

            // if password is invalid, return false
            User? user = _repo.Get(o => o.UserName == username);
            if (user != null && !AuthenticationUtil.IsValid(user.PasswordSaltHash, password))
            {
                returnMessage = "Wrong password";
                return false;
            }

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
