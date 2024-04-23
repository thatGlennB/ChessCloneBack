using ChessCloneBack.API.DataTransferObject;
using ChessCloneBack.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;



namespace ChessCloneBack.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserAccessController : ControllerBase
    {
        private IAuthenticationService _auth;
        private IEmailService _email;
        public UserAccessController(IAuthenticationService auth, IEmailService email)
        { 
            _auth = auth; 
            _email = email;
        }

        /// <summary>
        /// Allows new users to sign up. NB: this method does not return a token - users must log in to receive a token.
        /// </summary>
        /// <param name="data">User information - name, password, etc.</param>
        /// <returns>Ok if user information is valid, Bad Request if not valid (eg username is already taken)</returns>
        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody] RegistrationInfo data)
        {
            
            try
            {
                _auth.AddNewCredentials(data.UserName, data.Password, data.Email, data.ChessboardTheme, data.Premium, data.Notify);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
            return Ok();
        }

        /// <summary>
        /// Allows registered users to sign in.
        /// </summary>
        /// <param name="login">Username and password</param>
        /// <returns>JWT token if OK, or else a message explaining what went wrong</returns>
        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginCredentialsDTO login)
        {
            string result;
            // if no user is found return bad request
            if (!_auth.IsValidCredentials(login.UserName, login.Password, out result))
                return BadRequest(result);

            return Ok(new { token = result });

        }
        [AllowAnonymous]
        [HttpGet("available")]
        public IActionResult IsUsernameAvailable([FromQuery] string username)
        {
            bool output = _auth.IsNameAvailable(username);
            return Ok(output);
        }
        [AllowAnonymous]
        [HttpPost("email")]
        public IActionResult SendEmail() 
        {
            _email.Send();
            return Ok();
        }
    }
}
