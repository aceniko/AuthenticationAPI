using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationAPI.Domain.Models
{
    public class ActivateDeviceResponse
    {
        public string ServerPublicKey { get; set; }
        public string TokenSerial { get; set; }

        public int TokenId { get; set; }
        
    }
}
