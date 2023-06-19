using AuthenticationAPI.Data.Models;
using AuthenticationAPI.Data.Services;
using AuthenticationAPI.Domain.Models;
using AuthenticationAPI.Domain.Services;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationAPI.BusinessLogic.Services
{
    public class QrCodeService : IQRCodeService
    {
        private readonly IDataRepository _repository;

        public QrCodeService(IDataRepository repository)
        {
            _repository = repository;
        }

        public async Task<QrCodeResponse> GenerateQrCode(QrCodeRequest qrCodeRequest)
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            var qrCodeData = await Task.Run(() => qrGenerator.CreateQrCode(qrCodeRequest.SessionId, QRCodeGenerator.ECCLevel.Q)).ConfigureAwait(false);

            //QRCodeData qrCodeData = qrGenerator.CreateQrCode(qrCodeRequest.SessionId, QRCodeGenerator.ECCLevel.Q);
            //BitmapByteQRCode qrCode = new BitmapByteQRCode(qrCodeData);
            //byte[] qrCodeAsBitmapByteArr = qrCode.GetGraphic(20);
            PngByteQRCode qrCode = new PngByteQRCode(qrCodeData);
            byte[] qrCodeAsPngByteArr = qrCode.GetGraphic(20);

            await _repository.AddQrCode(Guid.Parse(qrCodeRequest.SessionId), Convert.ToBase64String(qrCodeAsPngByteArr), DateTime.Now.AddMinutes(1), false);

            return new QrCodeResponse
            {
                Code = Convert.ToBase64String(qrCodeAsPngByteArr)
            };
        }
    }
}
