using AuthenticationAPI.Core.Cryptography;
using AuthenticationAPI.Data.Services;
using AuthenticationAPI.Domain.Models;
using AuthenticationAPI.Domain.Services;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationAPI.BusinessLogic.Services
{
    public class DeviceService : IDeviceService
    {
        private readonly IDataRepository _repository;

        public DeviceService(IDataRepository repository)
        {
            this._repository = repository;
        }

        public async Task<ActivateDeviceResponse> ActivateDevice(string activationCode, string clientPublicKey)
        {
            var device = await _repository.GetDeviceByActivationCode(activationCode);
            if (device == null) {
                throw new Exception("Device not found");
            }
            var keyPair = SecretGenerator.GenerateSecret(WebEncoders.Base64UrlDecode(clientPublicKey));

            Console.WriteLine($"Secret: {WebEncoders.Base64UrlEncode(keyPair.Item2)}");
            await _repository.ChangeDeviceStatus(device.DeviceId, 1, keyPair.Item2);
            return new ActivateDeviceResponse
            {
                ServerPublicKey = WebEncoders.Base64UrlEncode(keyPair.Item1),
                TokenSerial = device.Token.TokenSerial,
                TokenId = device.Token.TokenId
            };
        }

        public async Task<int> AddDevice(int userId, int tokenId)
        {
            var secret = SecretGenerator.GenerateActivationCode();
            return await _repository.AddDevice(secret, 0, userId, tokenId);
        }

        public async Task DeleteDevice(int deviceId) {
            await _repository.DeleteDevice(deviceId);
        }
    }
}
