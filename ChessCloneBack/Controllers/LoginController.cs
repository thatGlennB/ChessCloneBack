using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ChessCloneBack.DataTransferObject;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using ChessCloneBack.BLL;

namespace ChessCloneBack.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : Controller
    {
        private IConfiguration _config;
        public LoginController(IConfiguration config)
        {
            _config = config;
        }
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Register([FromBody] UserModel data)
        {
            byte[] output = PasswordUtil.GetPasswordHash(data.Password);
            // TODO: validate input
            return Ok(output);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login([FromBody] UserModel login)
        {
            //look in repo for user with matching username

           DummyPasswordDTO user = new DummyPasswordDTO
           {
               UserName = "string",
               PasswordSaltedHash = [39, 5, 14, 107, 184, 120, 74, 193, 194, 9, 90, 158, 45, 29, 52, 148, 55, 162, 95, 68, 164, 222, 188, 130, 32, 214, 113, 161, 164, 58, 94, 186, 89, 113, 138, 244, 190, 162, 162, 117, 102, 124, 183, 204, 157, 200, 94, 138, 195, 235, 118, 119, 54, 78, 89, 247, 161, 1, 250, 104, 200, 244, 199, 122, 66, 255, 248, 124, 58, 100, 125, 232, 210, 74, 81, 1, 101, 107, 24, 232, 201, 236, 59, 4, 137, 122, 129, 133, 137, 250, 55, 70, 23, 129, 248, 251, 34, 164, 43, 115, 237, 205, 217, 122, 185, 223, 182, 215, 38, 93, 162, 134, 75, 163, 250, 17, 148, 149, 5, 178, 97, 98, 88, 22, 27, 105, 4, 59]
           };

            // if no user is found return bad request
            if (user.UserName != login.UserName)
                return BadRequest("User not found");

            if (!PasswordUtil.IsValid(user.PasswordSaltedHash, login.Password))
                return BadRequest("Wrong password");

            string JwtToken = GenerateJSONWebToken(login);
            return Ok(new { token = JwtToken });

        }
        [ApiExplorerSettings(IgnoreApi = true)]
        private string GenerateJSONWebToken(UserModel userInfo)
        {
            // Example token generation: in practice, I will have to get claims based on service

            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
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
