using AuthenticationAPI.Infrastructure.Cache;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace DemoWeb.Controllers
{
    public class CallbackController : Controller
    {
        private readonly IHubContext<SignalRHub> _hubContext;
        private readonly ICacheService _cacheService;
        public CallbackController(IHubContext<SignalRHub> hubContext, ICacheService cacheService)
        {
            _hubContext = hubContext;
            _cacheService = cacheService;
        }

        public async Task<bool> IndexAsync(string userId, string sessionId)
        {
            await _hubContext.Clients.All.SendAsync("Connect", sessionId, "callback received.");
            _cacheService.Set(sessionId, userId);
            return await Task.FromResult(true);
        }
    }
}
