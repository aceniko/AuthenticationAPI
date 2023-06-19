using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationAPI.Domain.Models
{
    public class CreateDeviceRequest
    {
        public int TokenId { get; set; }
        public int UserId { get; set; }
    }
}
