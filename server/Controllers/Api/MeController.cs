using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Server.Models;

namespace Server.Controllers.Api
{
  [Authorize]
  [Route("api/v1/[controller]")]
  public class MeController : Controller
  {
    private readonly UserManager<User> _userManager;
    public MeController(UserManager<User> userManager)
    {
      _userManager = userManager;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
      var user = await _userManager.GetUserAsync(HttpContext.User);
      if (user != null)
      {
        return Json(new UserViewModel { Email = user.Email });
      }
      return Forbid();
    }
  }
}
