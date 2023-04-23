using System.Collections.Generic;

namespace Employees.Model.DataAccess
{
    public interface IReadDA<in TKey, out TEntity>
    {
        IEnumerable<TEntity> GetAll();
        TEntity Get(TKey key);
    }
}