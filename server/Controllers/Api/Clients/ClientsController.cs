using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Models;
using Server.Persistence;

namespace Server.Controllers.Api.Clients
{
  [Authorize]
  [Route("api/v1/[controller]")]
  public class ClientsController : Controller
  {
    private readonly IRepository<Client> _repository;
    public ClientsController(IRepository<Client> repository)
    {
        _repository = repository;
    }

    // api/v1/users/me
    [HttpGet()]
    public IActionResult Get()
    {
        // example code: we may need a common Response object with pagination or whatever we decide on
        var clients =  _repository.GetList();
        return Json(clients);
    }
  }
}
