using System.Collections.Generic;
using System.Data;
using Employees.Model.DataAccess;
using static Employees.Model.Sql.DbIdentifiers;

namespace Employees.Model.Sql.DataAccess
{
    public class SqlPositionDA : SqlDataAccess<int, Position>, IPositionDA
    {
        public SqlPositionDA(SqlExecutor sqlExecutor) : base(sqlExecutor)
        {
        }


        protected override Position EntityFromReader(IDataReader reader)
        {
            int id = reader.GetInt32(reader.GetOrdinal(POSITION_ID));
            return GetFromCacheOrDb(id, () => new Position()
            {
                Id = id,
                Name = reader.GetString(reader.GetOrdinal(POSITION_NAME)),
            });
        }

        public IEnumerable<Position> GetAll() => QueryAllBy($"SELECT * FROM {POSITION_TABLE}");

        public Position Get(int id) => QueryOneBy(
            $"SELECT * FROM {POSITION_TABLE} WHERE {POSITION_ID} = @id",
            cmd => { cmd.AddParameter("@id", id); }
        );

        public void Add(Position entity)
        {
            if (entity.Id <= 0)
            {
                NonQuery(
                    $"INSERT INTO {POSITION_TABLE} ({POSITION_NAME}) VALUES (@name)",
                    cmd => { cmd.AddParameter("@name", entity.Name); }
                );
            }
            else Update(entity);
        } 

        public void Update(Position entity) => NonQuery(
            $"INSERT INTO {POSITION_TABLE} ({POSITION_ID}, {POSITION_NAME}) VALUES (@id, @name)" +
            $"ON CONFLICT ({POSITION_ID}) DO UPDATE SET {POSITION_NAME} = @name",
            cmd =>
            {
                cmd.AddParameter("@id", entity.Id);
                cmd.AddParameter("@name", entity.Name);
            }
        );

        public void Delete(Position entity) => Delete(entity.Id);
        public void Delete(int id) => NonQuery(
            $"DELETE FROM {POSITION_TABLE} WHERE {POSITION_ID} = @id",
            cmd => { cmd.AddParameter("@id", id); }
        );
    }
}