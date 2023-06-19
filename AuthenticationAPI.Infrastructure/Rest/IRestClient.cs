using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationAPI.Infrastructure.Rest
{
    public interface IRestClient
    {
        Task<TResponse> GetAsync<TResponse>(string endpoint);
        Task<TResponse> PostAsync<TRequest, TResponse>(string endpoint, TRequest request);
        Task<TResponse> PutAsync<TRequest, TResponse>(string endpoint, TRequest request);
        Task DeleteAsync(string endpoint);
    }
}
