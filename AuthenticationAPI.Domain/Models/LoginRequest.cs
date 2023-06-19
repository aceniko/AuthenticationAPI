using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationAPI.Domain.Models
{
    public class LoginRequest
    {
        public Guid Id { get; set; }
        public string TokenId { get; set; }
        public string Hash { get; set; }
    }
}
