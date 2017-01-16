using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Server.Models.Core;
using Microsoft.Extensions.Options;

namespace Server.Persistence
{
  public class Repository<T> : IRepository<T>
      where T : Model
  {
    private readonly ILogger _logger;
    private DbSet<T> _data;

    private readonly DatabaseContext _context;
    private readonly string _currentUserId;

    public Repository(
        DatabaseContext databaseContext,
        IOptions<Application> applicationOptions,
        ILoggerFactory loggerFactory)
    {
      _context = databaseContext;
      _logger = loggerFactory.CreateLogger<Repository<T>>();
      _currentUserId = applicationOptions.Value.LoggedInUserId;
      _data = databaseContext.Set<T>();

    }
    public void Delete(int[] ids)
    {
      var models = _data.Where(x => x.UserId == _currentUserId);
      _data.RemoveRange(models.Where(x => ids.Contains(x.Id)));
    }

    public T Get(int id)
    {
      var data = _data.AsNoTracking();
      data = data.Where(x => x.UserId == _currentUserId);
      return data.FirstOrDefault(x => x.Id == id);
    }

    public T Get(Expression<Func<T, bool>> where)
    {
      return _data
          .Where(x => x.UserId == _currentUserId)
          .Where(where).FirstOrDefault();
    }

    public IList<T> GetList(Expression<Func<T, bool>> where = null)
    {
      IQueryable<T> data = _data.Where(x => x.UserId == _currentUserId);
      if (where != null)
      {
        return data.Where(where).ToList();
      }
      return data.ToList();
    }

    public IList<T> GetRange(IEnumerable<int> ids)
    {
      var data = _data.AsNoTracking();
      return data
          .Where(x => x.UserId == _currentUserId)
          .Where(x => ids.Contains(x.Id)).ToList();
    }

    public T Save(T model)
    {
      SetCreated(model);
      _data.Add(model);
      _context.SaveChanges();
      return model;
    }

    public void SaveRange(IEnumerable<T> models)
    {
      var toSave = models.ToList();
      toSave.ForEach(SetCreated);
      _data.AddRange(toSave);
      _context.SaveChanges();
    }

    public T Update(T model)
    {
      SetUpdated(model);
      _context.SaveChanges();
      return model;
    }

    public void UpdateRange(IEnumerable<T> models)
    {
        var toUpdate = models.ToList();
        toUpdate.ForEach(SetUpdated);
        _context.SaveChanges();
    }

    private void SetCreated(T model)
    {
      model.UserId = _currentUserId;
      model.Created = model.Modified = DateTime.Now;
    }

    private void SetUpdated(T model)
    {
      model.UserId = _currentUserId;
      model.Modified = DateTime.Now;
      _data.Attach(model);
      _context.Entry(model).State = EntityState.Modified;
    }

  }
}
