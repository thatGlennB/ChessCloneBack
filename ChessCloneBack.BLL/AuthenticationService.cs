using ChessCloneBack.BLL.Infrastructure;
using ChessCloneBack.BLL.Interfaces;
using ChessCloneBack.DAL;
using ChessCloneBack.DAL.Entities;
using ChessCloneBack.DAL.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            User? user = _repo.Get(o => o.UserName == username);
            if (user != null)
                throw new ArgumentException("Username has already been taken", nameof(username));
            _repo.Add(new User
            {
                UserName = username,
                PasswordSaltHash = AuthenticationUtil.GetPasswordHash(password)
            });
        }

        public bool IsValidCredentials(string username, string password, out string returnMessage)
        {
            User? user = _repo.Get(o => o.UserName == username);
            // if no user is found, return false
            if(user == null)
            {
                returnMessage = "User not found";
                return false;
            }

            // if password is invalid, return false
            if (!AuthenticationUtil.IsValid(user.PasswordSaltHash, password)) 
            {
                returnMessage = "Wrong password";
                return false;
            }

            returnMessage = GenerateJSONWebToken();
            return true;
        }





        //look in repo for user with matching username
        //User user = _auth.Lookup(login.UserName);

        private string GenerateJSONWebToken()
        {
            // Example token generation: in practice, I will have to get claims based on service
            if (_config["Jwt:Key"] == null)
                throw new NullReferenceException("Null JWT Key: could not generate JWT Token because no key value was found in configuration file");
            

            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"] ?? ""));
            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            JwtSecurityToken token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: null,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
