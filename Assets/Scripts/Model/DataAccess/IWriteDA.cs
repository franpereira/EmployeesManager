namespace Employees.Model.DataAccess
{
    public interface IWriteDA<in TKey, in TEntity>
    {
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        void Delete(TKey key);
    }
}