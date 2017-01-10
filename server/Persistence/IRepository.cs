using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Server.Models;

namespace Server.Persistence {
    public interface IRepository<T>
        where T: Model
     {

        T Get(Expression<Func<T, bool>> @where);

        T Get(int id);

        IList<T> GetList(Expression<Func<T, bool>> @where = null);

        IList<T> GetRange(IEnumerable<int> ids);

        T Save(T model);

        void SaveRange(IEnumerable<T> models);

        T Update(T model);

        void UpdateRange(IEnumerable<T> models);
        
        void Delete(int[] ids);
    }
}
