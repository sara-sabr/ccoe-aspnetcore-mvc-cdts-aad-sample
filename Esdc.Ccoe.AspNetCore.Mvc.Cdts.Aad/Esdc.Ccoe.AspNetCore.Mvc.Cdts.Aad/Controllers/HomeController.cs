using Esdc.Ccoe.AspNetCore.Mvc.Cdts.Aad.Models;
using GoC.WebTemplate.Components.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Esdc.Ccoe.AspNetCore.Mvc.Cdts.Aad.Controllers
{
    [AllowAnonymous]
    public class HomeController : BaseGocTemplateController
    {
        public HomeController(IModelAccessor modelAccessor) : base(modelAccessor)
        {
        }

        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
