using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Models;
using Server.Models.Client;
using Server.Persistence;

namespace Server.Controllers.Api.Clients
{
    [Authorize]
    [Route("api/v1/[controller]")]
    public class ClientsController : ApiController<Client>
    {
        public ClientsController(IRepository<Client> repository) : base(repository)
        {
        }
    }
}
