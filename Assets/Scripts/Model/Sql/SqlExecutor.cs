using System;
using System.Data;

namespace Employees.Model.Sql
{
    public class SqlExecutor
    {
        readonly Func<IDbConnection> _connFactory;

        public SqlExecutor(Func<IDbConnection> connectionFactory)
        {
            _connFactory = connectionFactory;
        }
        
        /// <summary>
        /// Executes the specified SQL statement, with optional preparation and result handling.
        /// </summary>
        /// <param name="statement">The SQL statement to execute.</param>
        /// <param name="prepareCommand">An action to prepare the command before execution (optional).</param>
        /// <param name="handleResults">An action to handle the results after execution (optional).</param>
        public void Execute(string statement, Action<IDbCommand> prepareCommand = null, Action<IDataReader> handleResults = null)
        {
            using IDbConnection conn = _connFactory();
            conn.Open();
            using IDbCommand cmd = conn.CreateCommand();
            cmd.CommandText = statement;
            prepareCommand?.Invoke(cmd);

            if (handleResults != null)
            {
                using IDataReader r = cmd.ExecuteReader();
                handleResults(r);
            }
            else
                cmd.ExecuteNonQuery();
        }
    }
}