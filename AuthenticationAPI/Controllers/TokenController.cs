using Microsoft.AspNetCore.Mvc;

namespace AuthenticationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        [HttpPut("import")]
        public async Task<ActionResult<HttpResponseMessage>> ImportTokens() {
            return null;
        }

        [HttpPost("status")]
        public async Task<ActionResult<HttpResponseMessage>> ChangeTokenStatus(int status) {
            return null;
        }
    }
}
