using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationAPI.Domain.Models
{
    public class RegisterUserRequest
    {
        public string ExternalId { get; set; }
        public string Name { get; set; }
    }
}
