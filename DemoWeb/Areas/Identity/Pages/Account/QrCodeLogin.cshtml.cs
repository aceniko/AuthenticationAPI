using AuthenticationAPI.Domain.Models;
using AuthenticationAPI.Infrastructure.Cache;
using AuthenticationAPI.Infrastructure.Rest;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;

namespace DemoWeb.Areas.Identity.Pages.Account
{
    public class QrCodeLoginModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ILogger<QrCodeLoginModel> _logger;
        private readonly IRestClient _restClient;
        private readonly ICacheService _cacheService;
        private readonly IConfiguration _configuration;
        public QrCodeLoginModel(SignInManager<IdentityUser> signInManager, ILogger<QrCodeLoginModel> logger, IRestClient restClient, ICacheService cacheService, IConfiguration configuration)
        {
            _signInManager = signInManager;
            _logger = logger;
            _restClient = restClient;
            _cacheService = cacheService;
            _configuration = configuration;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public async Task OnGetAsync()
        {
            Input = new InputModel();

            var request = new QrCodeRequest { 
                SessionId = HttpContext.Session.Id,
                CallbackUrl = string.Empty
            };
            var result = await _restClient.PostAsync<QrCodeRequest, QrCodeResponse>($"{_configuration.GetValue(typeof(string), "AuthenticationApiUrl")}api/authenticate/generate-qr", request);

            Input.SessionId = HttpContext.Session.Id;
            Input.QrCodeImageBase64 = result.Code;
            
        }

        public async Task<IActionResult> OnPostAsync()
        {
            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}
            var userId = _cacheService.Get<string>(Input.SessionId);
            var user = await _signInManager.UserManager.FindByIdAsync(userId);
            await _signInManager.SignInAsync(user, isPersistent: false);

            return RedirectToPage("./Index");
        }

        public class InputModel { 
            public string QrCodeImageBase64 { get; set; }
            public string SessionId { get; set; }
        }
    }
}
