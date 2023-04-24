using System;
using System.Data;
using Employees.Model.DataAccess;

namespace Employees.Model.Sql.DataAccess
{
    public class SqlDataRepository : IDataRepository
    {
        public IPositionDA Positions { get; }
        public ISeniorityDA Seniorities { get; }
        public IEmployeeDA Employees { get; }

        public SqlDataRepository(Func<IDbConnection> connectionFactory)
        {
            var executor = new SqlExecutor(connectionFactory);
            Positions = new SqlPositionDA(executor);
            Seniorities = new SqlSeniorityDA(executor, Positions);
            Employees = new SqlEmployeeDA(executor, Seniorities);
        }
    }
}