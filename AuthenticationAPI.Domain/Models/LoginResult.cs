using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationAPI.Domain.Models
{
    public class LoginResult
    {
        public int Status { get; set; }
        public int UserId { get; set; }
        public string ExternalUserId { get; set; }
    }
}
