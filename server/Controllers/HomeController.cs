using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using IdentityServer4.Services;
using Server.Models.HomeViewModels;
using Server.Controllers.Filters;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Antiforgery;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;

namespace Server.Controllers
{
  [SecurityHeaders]
  public class HomeController : Controller
  {
    private readonly IIdentityServerInteractionService _interaction;
    private readonly IHostingEnvironment _env;
    private readonly IAntiforgery _antiforgery;
    private dynamic _assets;

    public HomeController(
        IHostingEnvironment env,
        IIdentityServerInteractionService interaction,
        IAntiforgery antiforgery)
    {
      _interaction = interaction;
      _env = env;
      _antiforgery = antiforgery;
    }

    public async Task<IActionResult> Index()
    {
      if (User.Identity.IsAuthenticated)
      {
        if (_env.EnvironmentName != "Test" && (_env.IsDevelopment() || _assets == null))
        {
          var assetsFileName = Path.Combine(_env.WebRootPath, "./dist/assets.json");

          using (var stream = System.IO.File.OpenRead(assetsFileName))
          using (var reader = new StreamReader(stream))
          {
            var json = await reader.ReadToEndAsync();
            _assets = JsonConvert.DeserializeObject(json);
          }
        }

        ViewData["assets:main:js"] = (string)_assets.main.js;

        var tokens = _antiforgery.GetAndStoreTokens(Request.HttpContext);
        Response.Cookies.Append("XSRF-TOKEN", tokens.RequestToken, new CookieOptions { HttpOnly = false });

        return View();
      }

      return View("Landing");
    }
    public async Task<IActionResult> Error(string errorId)
    {
      var vm = new ErrorViewModel();

      // retrieve error details from identityserver
      var message = await _interaction.GetErrorContextAsync(errorId);
      if (message != null)
      {
        vm.Error = message;
      }

      return View("Error", vm);
    }
  }
}
