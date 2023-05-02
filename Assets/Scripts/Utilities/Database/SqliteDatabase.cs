using System.Data;
using Mono.Data.Sqlite;

namespace Employees.Utilities.Database
{
    public class SqliteDatabase : IDatabase
    {
        public string FilePath { get; }
        string ConnectionString => $"Data Source={FilePath}";

        public SqliteDatabase(string filePath) => FilePath = filePath;

        public IDbConnection NewConnection() => new SqliteConnection(ConnectionString);
    }
}