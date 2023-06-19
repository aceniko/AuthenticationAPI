
using AuthenticationAPI.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections;

namespace AuthenticationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpPost("register")]
        public async Task<ActionResult<HttpResponseMessage>> RegisterUser(RegisterUserRequest registerUserRequest) {
            return null;
        }

        [HttpDelete("/delete/{id}")]
        public async Task<ActionResult<HttpResponseMessage>> DeleteUserById(int id)
        {
            return null;
        }

        [HttpDelete("/delete-external/{externalId}")]
        public async Task<ActionResult<HttpResponseMessage>> DeleteUserByExternalId(int externalId) {
            return null;
        }

        [HttpGet("list")]
        public async Task<IEnumerable<UserDTO>> UserList() {
            return null;
        }
    }
}
