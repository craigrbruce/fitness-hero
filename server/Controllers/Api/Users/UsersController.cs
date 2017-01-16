using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Server.Models;

namespace Server.Controllers.Api.Users
{
  [Authorize]
  [Route("api/v1/[controller]")]
  public class UsersController : Controller
  {
    private readonly UserManager<User> _userManager;
    public UsersController(UserManager<User> userManager)
    {
      _userManager = userManager;
    }

    // api/v1/users/me
    [HttpGet("me")]
    public async Task<ActionResult> Get()
    {
      var user = await _userManager.GetUserAsync(HttpContext.User);
      return Json(user.UserName);
    }
  }
}
