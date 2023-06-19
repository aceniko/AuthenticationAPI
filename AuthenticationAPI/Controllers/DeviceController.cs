using AuthenticationAPI.Domain.Models;
using AuthenticationAPI.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeviceController : ControllerBase
    {
        private readonly IDeviceService _deviceService;

        public DeviceController(IDeviceService deviceService)
        {
            _deviceService = deviceService;
        }

        [HttpPost("create")]
        public async Task<ActionResult<HttpResponseMessage>> CreateDevice(CreateDeviceRequest createDeviceRequest) {
            var result = await _deviceService.AddDevice(createDeviceRequest.UserId, createDeviceRequest.TokenId);
            return Ok(result);
        }

        [HttpPost("activate")]
        public async Task<ActionResult<HttpResponseMessage>> ActivateDevice(ActivateDeviceRequest activateDeviceRequest) { 
            var activationCode = activateDeviceRequest.Code1 + activateDeviceRequest.Code2;
            var result = await _deviceService.ActivateDevice(activationCode, activateDeviceRequest.PublicKey);
            return Ok(result);
        }

        [HttpDelete("delete")]
        public async Task<ActionResult<HttpResponseMessage>> DeleteDevice(int deviceId) {
            await _deviceService.DeleteDevice(deviceId);
            return Ok();
        }


    }
}
