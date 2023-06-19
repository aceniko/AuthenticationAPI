using AuthenticationAPI.Domain.Models;
using AuthenticationAPI.Domain.Services;
using AuthenticationAPI.Infrastructure.Rest;
using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace AuthenticationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly IAuthenticationService _authenticatorService;
        private readonly IQRCodeService _qrCodeService;
        private readonly IRestClient _restClient;
        private readonly IConfiguration _configuration;
        public AuthenticateController(IAuthenticationService authenticatorService, IQRCodeService qrCodeService, IRestClient restClient, IConfiguration configuration)
        {
            _authenticatorService = authenticatorService;
            _qrCodeService = qrCodeService;
            _restClient = restClient;
            _configuration = configuration;
        }
        [HttpPost("generate-qr")]
        public async Task<ActionResult<QrCodeResponse>> GenerateQrCodeAsync(QrCodeRequest qrCodeRequest) {
            return await _qrCodeService.GenerateQrCode(qrCodeRequest);
        }

        [HttpPost("authenticate")]
        public async Task<ActionResult<LoginResult>> Authenticate(LoginRequest loginRequest) {
            var result = await _authenticatorService.ValidateLoginRequestAsync(loginRequest);

            if (result.Status == 1) {
                
                var httpResult = await _restClient.GetAsync<bool>($"{_configuration.GetValue(typeof(string), "CallbackUrl")}?userId={result.ExternalUserId}&sessionId={loginRequest.Id}");
                return Ok(httpResult);
            }

            return Unauthorized();
        }
    }
}
