using System;
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

    [HttpGet()]
    public IActionResult Get()
    {
      // example code: we may need a common Response object with pagination or whatever we decide on
      var clients = _repository.GetList();
      return Json(clients);
    }

    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
      var result = _repository.Get(id);
      if (result == null)
      {
        return NotFound($"Client with id '{id}' not found");
      }
      return Json(result);
    }

    [HttpPost]
    public IActionResult Post([FromBody]Client client)
    {
      var saved = _repository.Save(client);
      return Created($"{Request.Path.Value}/{saved.Id}", saved);
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody]Client client)
    {
      var existing = _repository.Get(id);
      if (existing == null)
      {
        return NotFound();
      }
      var updated = _repository.Update(client);
      return Json(updated);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
      _repository.Delete(new[] { id });
      return NoContent();
    }

    [HttpDelete]
    public IActionResult BulkDelete(int[] ids)
    {
      _repository.Delete(ids);
      return NoContent();
    }
  }
}
