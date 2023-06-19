using AuthenticationAPI.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationAPI.Data.Services
{
    public interface IDataRepository
    {
        Task<int> AddUser(string name, string externalId, int? deviceId);
        Task DeleteUser(int userId);

        Task ImportTokens(IEnumerable<string> tokens);
        Task<int> AddDevice(string activationCode, int status, int userId, int tokenId);
        Task<Device> GetDeviceByActivationCode(string activationCode);
        Task ChangeDeviceStatus(int deviceId,  int status, byte[] secret);
        Task<Device> GetDeviceByToken(int tokenId);

        Task DeleteDevice(int deviceId);
        Task<Guid> AddQrCode(Guid id, string content, DateTime expire, bool isValidated);
        Task UpdateQrCode(Guid id, bool isValidated);

        Task<QrCode> GetQrCode(Guid id);
    }
}
