using AuthenticationAPI.Data.Data;
using AuthenticationAPI.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationAPI.Data.Services
{
    public class DataRepository : IDataRepository
    {
        private readonly AuthenticationDbContext _context;

        public DataRepository(AuthenticationDbContext context)
        {
            _context = context;
        }
        #region User
        public async Task<int> AddUser(string name, string externalId)
        {
            var user = new User { Name = name, ExternalId = externalId };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user.UserId;
        }

        public async Task DeleteUser(int userId)
        {
            var user = _context.Users.FirstOrDefault(x => x.UserId == userId);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }

        #endregion User

        #region Token
        public async Task ImportTokens(IEnumerable<string> tokens)
        {
            _context.Tokens.AddRange(
                (from n in tokens
                 select new Token
                 {
                     TokenSerial = n,
                     IsActive = false
                 }).ToArray()
                );
            await _context.SaveChangesAsync();
        }
        #endregion Token

        #region Device
        public async Task<int> AddDevice(string activationCode, int status, int userId, int tokenId) {

            var token = _context.Tokens.FirstOrDefault(x=>x.TokenId == tokenId);
            var user = _context.Users.FirstOrDefault(x => x.UserId == userId);
            var device = new Device
            {
                ActivationCode = activationCode,
                Status = status,
                Token = token,
                User = user
            };
            _context.Devices.Add(device);

            await _context.SaveChangesAsync();

            return device.DeviceId;
        }

        public async Task<Device> GetDeviceByToken(int tokenId) {
            var device = await _context.Devices.Include(x=>x.User).FirstOrDefaultAsync(x => x.TokenId == tokenId);
            if (device == null) {
                throw new Exception($"Device with TokenId: {tokenId} does not exists.");
            }
            return device;
        }

        #endregion Device

        #region QrCode
        public async Task<Guid> AddQrCode(Guid id, string content, DateTime expire, bool isValidated) {
            var qrCode = new QrCode {
                QrCodeId = id,
                Content = content,
                Expire = expire,
                IsValidated = isValidated
            };
            _context.QrCodes.Add(qrCode);
            await _context.SaveChangesAsync();
            return qrCode.QrCodeId;
        }

        public async Task<QrCode> GetQrCode(Guid id) {
            var qrCode = await _context.QrCodes.FindAsync(id);
            if(qrCode == null) { throw new Exception($"Unable to find Qr Code with ID: {id}"); }

            return qrCode;
        }
        public async Task UpdateQrCode(Guid id, bool isValidated) {
            var qrCode = await _context.QrCodes.FindAsync(id);
            if(qrCode == null)
            {
                throw new Exception($"Unable to find Qr Code with ID: {id}");
            }
            qrCode.IsValidated = isValidated;
            await _context.SaveChangesAsync();
        }
        public Task<int> AddUser(string name, string externalId, int? deviceId)
        {
            throw new NotImplementedException();
        }

        public async Task<Device> GetDeviceByActivationCode(string activationCode)
        {
            return await _context.Devices.Include(x=>x.Token).FirstOrDefaultAsync(x=>x.ActivationCode == activationCode);
        }

        public async Task ChangeDeviceStatus(int deviceId, int status, byte[] secret)
        {
            var device = await _context.Devices.FindAsync(deviceId);
            device.Status = status;
            device.SecretKey = secret;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteDevice(int deviceId)
        {
            var device = _context.Devices.Find(deviceId);
            _context.Devices.Remove(device);
            await _context.SaveChangesAsync();
        }


        #endregion QrCode

    }
}
