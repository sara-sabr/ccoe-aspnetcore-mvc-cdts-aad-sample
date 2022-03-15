using GoC.WebTemplate.Components.Core.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Mvc;

namespace Esdc.Ccoe.AspNetCore.Mvc.Cdts.Aad.Controllers
{
    public class AccountController : BaseGocTemplateController
    {
        public AccountController(IModelAccessor modelAccessor) : base(modelAccessor)
        {
        }

        public IActionResult SignIn()
        {
            var redirectUri = Url.Action("Index", "Home");

            return Challenge(
                new AuthenticationProperties { RedirectUri = redirectUri },
                OpenIdConnectDefaults.AuthenticationScheme);
        }

        public IActionResult SignOut()
        {
            var callbackUrl = Url.Action("Index", "Home");
            
            return SignOut(
                 new AuthenticationProperties {
                     RedirectUri = callbackUrl,
                 },
                 CookieAuthenticationDefaults.AuthenticationScheme, 
                 OpenIdConnectDefaults.AuthenticationScheme);
        }
    }
}
