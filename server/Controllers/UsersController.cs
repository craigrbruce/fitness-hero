using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers
{
    [Authorize]
    [Route("api/v1/[controller]")]
    public class UsersController : Controller
    {
        [HttpGet]
        public IActionResult Get()
        {
            return new OkResult();
        }
    }
}
