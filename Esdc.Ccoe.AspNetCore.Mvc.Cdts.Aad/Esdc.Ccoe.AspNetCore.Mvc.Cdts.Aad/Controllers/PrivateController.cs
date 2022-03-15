using Esdc.Ccoe.AspNetCore.Mvc.Cdts.Aad.Models;
using GoC.WebTemplate.Components.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph;
using Microsoft.Identity.Web;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Esdc.Ccoe.AspNetCore.Mvc.Cdts.Aad.Controllers
{
    [Authorize]
    public class PrivateController : BaseGocTemplateController
    {
        private readonly GraphServiceClient _graphClient;

        public PrivateController(GraphServiceClient graphClient, IModelAccessor modelAccessor) : base(modelAccessor)
        {
            _graphClient = graphClient;
        }

        // This attribute will handle the incremental consent if needed, the token request, if not in memory cache.
        [AuthorizeForScopes(Scopes = new string[] { "user.readbasic.all" })]
        // Alternatively, you can use the appsettings to get the scopes if you don't want to hardcode them.
        // You can split the scopes in the configuration to ask only for specific scopes when requesting the token or all of them...
        //[AuthorizeForScopes(ScopeKeySection = "DownstreamApi:Scopes")]
        public async Task<IActionResult> Index()
        {
            var users = await _graphClient.Users.Request().GetAsync();

            var model = new PrivateViewModel {
                FirstUserDisplayName = users.CurrentPage?.FirstOrDefault().DisplayName ?? "Error getting display name."
            };

            return View(model);
        }

        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
