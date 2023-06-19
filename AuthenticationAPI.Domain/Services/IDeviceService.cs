using AuthenticationAPI.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationAPI.Domain.Services
{
    public interface IDeviceService
    {
        Task<int> AddDevice(int userId, int tokenId);

        Task<ActivateDeviceResponse> ActivateDevice(string activationCode, string clientPublicKey);

        Task DeleteDevice(int deviceId);
    }
}
