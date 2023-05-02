using System.Data;

namespace Employees.Utilities.Database
{
    public interface IDatabase
        {
            public IDbConnection NewConnection();
        }
 }