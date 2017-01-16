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
    private readonly IRepository<T> _repository;
    public ApiController(IRepository<T> repository)
    {
      _repository = repository;
    }

    [HttpGet()]
    public IActionResult Get()
    {
      var models = _repository.GetList();
      return Json(models);
    }

    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
      var result = _repository.Get(id);
      if (result == null)
      {
        return NotFound($"Item with id '{id}' not found");
      }
      return Json(result);
    }

    [HttpPost]
    public IActionResult Post([FromBody]T model)
    {
      var saved = _repository.Save(model);
      return Created($"{Request.Path.Value}/{saved.Id}", saved);
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody]T model)
    {
      var existing = _repository.Get(id);
      if (existing == null)
      {
        return NotFound();
      }
      var updated = _repository.Update(model);
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
