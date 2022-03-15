using GoC.WebTemplate.Components.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;

namespace Esdc.Ccoe.AspNetCore.Mvc.Cdts.Aad.Controllers
{
    public abstract class BaseGocTemplateController : Controller
    {
        private readonly IModelAccessor _modelAccessor;

        public BaseGocTemplateController(IModelAccessor modelAccessor)
        {
            _modelAccessor = modelAccessor;
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext filterContext, ActionExecutionDelegate next)
        {
            ViewBag.WebTemplateModel = _modelAccessor.Model;
            _modelAccessor.Model.HeaderTitle = "CCoE Sample App";
            _modelAccessor.Model.ApplicationTitle.Text = "CCoE | CDTS-AAD-MVC Sample";
            _modelAccessor.Model.ApplicationTitle.Href = Url.Action("Index", "Home");
            
            // To add account settings button.
            _modelAccessor.Model.AppSettingsURL = Url.Action("Index", "Home");

            await base.OnActionExecutionAsync(filterContext, next);

            if (User.Identity.IsAuthenticated) {
                _modelAccessor.Model.ShowSignOutLink = true;
                _modelAccessor.Model.Settings.SignOutLinkUrl = Url.Action(nameof(AccountController.SignOut), "Account");

            } else {
                _modelAccessor.Model.ShowSignInLink = true;
                _modelAccessor.Model.Settings.SignInLinkUrl = Url.Action(nameof(AccountController.SignIn), "Account");
            }
        }
    }
}
