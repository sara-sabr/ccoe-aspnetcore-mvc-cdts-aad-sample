using Esdc.Ccoe.AspNetCore.Mvc.Cdts.Aad.Models;
using GoC.WebTemplate.Components.Core.Services;
using GoC.WebTemplate.CoreMVC.ActionFilters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Esdc.Ccoe.AspNetCore.Mvc.Cdts.Aad.Controllers
{
    //[Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IModelAccessor _modelAccessor;

        public HomeController(ILogger<HomeController> logger, IModelAccessor modelAccessor)
        {
            _logger = logger;
            _modelAccessor = modelAccessor;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext filterContext, ActionExecutionDelegate next)
        {
            ViewBag.WebTemplateModel = _modelAccessor.Model;
            _modelAccessor.Model.HeaderTitle = "hellO";
            _modelAccessor.Model.ApplicationTitle.Text = "app title";
            _modelAccessor.Model.AppSettingsURL = Url.Action("Index", "Home");

            await base.OnActionExecutionAsync(filterContext, next);

            if (User.Identity.IsAuthenticated) {
                _modelAccessor.Model.ShowSignOutLink = true;
                //modelAccessor.Model.Settings.SignOutLinkUrl = Url.Action(nameof(AccountController.SignOut), "Account");
                _modelAccessor.Model.Settings.SignOutLinkUrl = "somelink/";

            } else {
                _modelAccessor.Model.ShowSignInLink = true;
                //_modelAccessor.Model.ShowSignInLink = Url.Action(nameof(AccountController.SignIn), "Account");
                _modelAccessor.Model.Settings.SignInLinkUrl = "somelink/";
            }
        }
    }
}
