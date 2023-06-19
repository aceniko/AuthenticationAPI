using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationAPI.Domain.Models
{
    public class QrCodeRequest
    {
        public string SessionId { get; set; }
        public string CallbackUrl { get; set; }
    }
}
