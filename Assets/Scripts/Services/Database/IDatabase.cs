using System.Data;

namespace Employees.Services.Database
{
    public interface IDatabase
        {
            public IDbConnection NewConnection();
        }
 }