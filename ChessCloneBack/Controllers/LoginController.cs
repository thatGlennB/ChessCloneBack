using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ChessCloneBack.DataTransferObject;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using ChessCloneBack.BLL.Infrastructure;
using ChessCloneBack.BLL.Interfaces;
using ChessCloneBack.DAL.Entities;

namespace ChessCloneBack.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : Controller
    {
        private IConfiguration _config;
        private IAuthenticationService _auth;
        public LoginController(IConfiguration config, IAuthenticationService auth)
        {
            _config = config;
            _auth = auth;
        }
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Register([FromBody] UserModel data)
        {
            byte[] output = AuthenticationUtil.GetPasswordHash(data.Password);
            Console.WriteLine();
            // TODO: validate input
            return Ok(String.Join(",",output.Select(o => o.ToString())));
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login([FromBody] UserModel login)
        {
            string result;
            // if no user is found return bad request
            if (!_auth.IsValidCredentials(login.UserName, login.Password, out result))
                return BadRequest(result);
            
            return Ok(new { token = result });

        }
        
    }
}
