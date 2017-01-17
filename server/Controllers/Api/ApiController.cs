using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Models.Core;
using Server.Persistence;

namespace Server.Controllers.Api.Clients
{
  [Authorize]
  public class ApiController<T> : Controller
  where T : Model
  {
    protected readonly IRepository<T> Repository;
    public ApiController(IRepository<T> repository)
    {
      Repository = repository;
    }

    [HttpGet()]
    public IActionResult Get()
    {
      var models = Repository.GetList();
      return Json(models);
    }

    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
      var result = Repository.Get(id);
      if (result == null)
      {
        return NotFound($"Item with id '{id}' not found");
      }
      return Json(result);
    }

    [HttpPost]
    public IActionResult Post([FromBody]T model)
    {
      var saved = Repository.Save(model);
      return Created($"{Request.Path.Value}/{saved.Id}", saved);
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody]T model)
    {
      var existing = Repository.Get(id);
      if (existing == null)
      {
        return NotFound();
      }
      var updated = Repository.Update(model);
      return Json(updated);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
      Repository.Delete(new[] { id });
      return NoContent();
    }

    [HttpDelete]
    public IActionResult BulkDelete(int[] ids)
    {
      Repository.Delete(ids);
      return NoContent();
    }
  }
}
