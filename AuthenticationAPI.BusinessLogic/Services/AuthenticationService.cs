using AuthenticationAPI.Core.Cryptography;
using AuthenticationAPI.Data.Models;
using AuthenticationAPI.Data.Services;
using AuthenticationAPI.Domain.Models;
using AuthenticationAPI.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationAPI.BusinessLogic.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IDataRepository _repository;

        public AuthenticationService(IDataRepository repository)
        {
            _repository = repository;
        } 

        public async Task<LoginResult> ValidateLoginRequestAsync(LoginRequest loginRequest)
        {

            if (!int.TryParse(loginRequest.TokenId, out _)) {
                throw new ArgumentException($"Missing or invalid tokenId. {loginRequest.TokenId}");
            }

            var qrCode = await _repository.GetQrCode(loginRequest.Id);
            
            if(qrCode.Expire < DateTime.Now)
            {
                throw new Exception("Qr Code expired");
            }

            var device = await _repository.GetDeviceByToken(int.Parse(loginRequest.TokenId));

            var loginResult = new LoginResult
            {
                Status = 0,
                UserId = device.UserId,
                ExternalUserId = device.User.ExternalId
            };

            var hmac = Hmac.Digest(qrCode.QrCodeId.ToString(), device.SecretKey);

            if(loginRequest.Hash.Equals(hmac))
            {
                await _repository.UpdateQrCode(loginRequest.Id, true);
                loginResult.Status = 1;
            }

            return loginResult;
        }


    }
}
