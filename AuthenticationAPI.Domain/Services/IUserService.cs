using AuthenticationAPI.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationAPI.Domain.Services
{
    public interface IUserService
    {
        void RegisterUser(RegisterUserRequest registerUserRequest);
        void DeleteUserByExternalId(string externalId);
        void DeleteUser(int userId);
    }
}
