using AuthenticationAPI.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationAPI.Domain.Services
{
    public interface IAuthenticationService
    {
        Task<LoginResult> ValidateLoginRequestAsync(LoginRequest loginRequest);
    }
}
