using ChessCloneBack.DAL.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChessCloneBack.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeatureController : ControllerBase
    {
        [AllowAnonymous]
        [HttpGet("boardstyles")]
        public IActionResult GetBoardStyles()
        {
            Dictionary<int, string> output = new();
            foreach(BoardStyle element in Enum.GetValues(typeof(BoardStyle))) 
            {
                output.Add((int)element, element.ToString());
            }
            return Ok(output);
        }
    }
}
