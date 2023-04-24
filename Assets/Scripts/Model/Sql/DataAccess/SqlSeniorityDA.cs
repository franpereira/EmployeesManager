using System.Collections.Generic;
using System.Data;
using Employees.Model.DataAccess;
using static Employees.Model.Sql.DbIdentifiers;

namespace Employees.Model.Sql.DataAccess
{
    public class SqlSeniorityDA : SqlDataAccess<int, Seniority>, ISeniorityDA
    {
        readonly IPositionDA _positionDA;
        
        public SqlSeniorityDA(SqlExecutor executor, IPositionDA positionDA) : base(executor)
        {
            _positionDA = positionDA;
        }
        
        protected override Seniority EntityFromReader(IDataReader r)
        {
            int id = r.GetInt32(r.GetOrdinal(SENIORITY_ID));
            return GetFromCacheOrDb(id, () => new Seniority()
            {
                Id = id,
                Position = _positionDA.Get(r.GetInt32(r.GetOrdinal(POSITION_ID))),
                Name = r.GetString(r.GetOrdinal(SENIORITY_NAME)),
                Ordinal = r.GetInt32(r.GetOrdinal(SENIORITY_ORDINAL)),
                BaseSalary = r.GetDouble(r.GetOrdinal(SENIORITY_BASE_SALARY)),
                PercentagePerIncrement = r.GetDouble(r.GetOrdinal(SENIORITY_PERCENTAGE_PER_INCREMENT)),
                CurrentIncrements = r.GetInt32(r.GetOrdinal(SENIORITY_CURRENT_INCREMENTS))
            });
        }

        public IEnumerable<Seniority> GetAll() => QueryAllBy($"SELECT * FROM {SENIORITY_TABLE}");

        public Seniority Get(int key) => QueryOneBy(
            $"SELECT * FROM {SENIORITY_TABLE} WHERE {SENIORITY_ID} = @id",
            cmd => { cmd.AddParameter("@id", key); }
        );

        public IEnumerable<Seniority> GetByPosition(Position position) => QueryAllBy(
            $"SELECT * FROM {SENIORITY_TABLE} WHERE {POSITION_ID} = @positionId",
            cmd => { cmd.AddParameter("@positionId", position.Id); }
        );

        public void Add(Seniority entity) => NonQuery(
            $"INSERT INTO {SENIORITY_TABLE}" +
            $"{SENIORITY_POSITION_ID}, {SENIORITY_NAME}, {SENIORITY_ORDINAL}, {SENIORITY_BASE_SALARY}, {SENIORITY_PERCENTAGE_PER_INCREMENT}, {SENIORITY_CURRENT_INCREMENTS}) VALUES " +
            $"(@positionId, @name, @ordinal, @baseSalary, @percentagePerIncrement, @currentIncrements)" +
            $"ON CONFLICT ({SENIORITY_ID}) DO UPDATE SET " +
            $"{SENIORITY_POSITION_ID} = @positionId, {SENIORITY_NAME} = @name, {SENIORITY_ORDINAL} = @ordinal, {SENIORITY_BASE_SALARY} = @baseSalary, {SENIORITY_PERCENTAGE_PER_INCREMENT} = @percentagePerIncrement, {SENIORITY_CURRENT_INCREMENTS} = @currentIncrements",

            cmd =>
            {
                cmd.AddParameter("@id", entity.Id);
                cmd.AddParameter("@positionId", entity.Position.Id);
                cmd.AddParameter("@name", entity.Name);
                cmd.AddParameter("@ordinal", entity.Ordinal);
                cmd.AddParameter("@baseSalary", entity.BaseSalary);
                cmd.AddParameter("@percentagePerIncrement", entity.PercentagePerIncrement);
                cmd.AddParameter("@currentIncrements", entity.CurrentIncrements);
            }
        );
        
        public void Update(Seniority entity) => NonQuery(
            $"INSERT INTO {SENIORITY_TABLE}" +
            $"({SENIORITY_ID}, {SENIORITY_POSITION_ID}, {SENIORITY_NAME}, {SENIORITY_ORDINAL}, {SENIORITY_BASE_SALARY}, {SENIORITY_PERCENTAGE_PER_INCREMENT}, {SENIORITY_CURRENT_INCREMENTS}) VALUES " +
            $"(@id, @positionId, @name, @ordinal, @baseSalary, @percentagePerIncrement, @currentIncrements)" +
            $"ON CONFLICT ({SENIORITY_ID}) DO UPDATE SET " +
            $"{SENIORITY_POSITION_ID} = @positionId, {SENIORITY_NAME} = @name, {SENIORITY_ORDINAL} = @ordinal, {SENIORITY_BASE_SALARY} = @baseSalary, {SENIORITY_PERCENTAGE_PER_INCREMENT} = @percentagePerIncrement, {SENIORITY_CURRENT_INCREMENTS} = @currentIncrements",

            cmd =>
            {
                cmd.AddParameter("@id", entity.Id);
                cmd.AddParameter("@positionId", entity.Position.Id);
                cmd.AddParameter("@name", entity.Name);
                cmd.AddParameter("@ordinal", entity.Ordinal);
                cmd.AddParameter("@baseSalary", entity.BaseSalary);
                cmd.AddParameter("@percentagePerIncrement", entity.PercentagePerIncrement);
                cmd.AddParameter("@currentIncrements", entity.CurrentIncrements);
            }
        );

        public void Delete(Seniority entity) => Delete(entity.Id);
        public void Delete(int key) => NonQuery(
            $"DELETE FROM {SENIORITY_TABLE} WHERE {SENIORITY_ID} = @id",
            cmd => { cmd.AddParameter("@id", key); }
        );
    }
}