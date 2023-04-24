using System.Collections.Generic;
using System.Data;
using Employees.Model.DataAccess;
using static Employees.Model.Sql.DbIdentifiers;


namespace Employees.Model.Sql.DataAccess
{
    public class SqlEmployeeDA : SqlDataAccess<int, Employee>, IEmployeeDA
    {
        readonly ISeniorityDA _seniorityDA;
        
        public SqlEmployeeDA(SqlExecutor executor, ISeniorityDA seniorityDA) : base(executor)
        {
            _seniorityDA = seniorityDA;    
        }

        protected override Employee EntityFromReader(IDataReader r)
        {
            int id = r.GetInt32(r.GetOrdinal(EMPLOYEE_ID));
            return GetFromCacheOrDb(id, () => new Employee()
            {
                Id = id,
                Seniority = _seniorityDA.Get(r.GetInt32(r.GetOrdinal(EMPLOYEE_SENIORITY_ID))),
                FirstName = r.GetString(r.GetOrdinal(EMPLOYEE_FIRST_NAME)),
                LastName = r.GetString(r.GetOrdinal(EMPLOYEE_LAST_NAME)),
            });
        }

        public IEnumerable<Employee> GetAll() => QueryAllBy($"SELECT * FROM {EMPLOYEE_TABLE}");

        public Employee Get(int key) => QueryOneBy(
            $"SELECT * FROM {EMPLOYEE_TABLE} WHERE {EMPLOYEE_ID} = @id",
            cmd => { cmd.AddParameter("@id", key); }
        );
        
        public IEnumerable<Employee> GetBySeniority(Seniority seniority) => QueryAllBy(
            $"SELECT * FROM {EMPLOYEE_TABLE} WHERE {EMPLOYEE_SENIORITY_ID} = @seniorityId",
            cmd => { cmd.AddParameter("@seniorityId", seniority.Id); }
        );

        public IEnumerable<Employee> GetByPosition(Position position)
        {
            var seniorities = _seniorityDA.GetByPosition(position);
            var employees = new List<Employee>();
            foreach (var seniority in seniorities) 
                employees.AddRange(GetBySeniority(seniority));
            return employees;
        }

        public void Add(Employee entity) => NonQuery(
            $"INSERT INTO {EMPLOYEE_TABLE} ({EMPLOYEE_SENIORITY_ID}, {EMPLOYEE_FIRST_NAME}, {EMPLOYEE_LAST_NAME}) VALUES " +
            $"(@seniorityId, @firstName, @lastName) ",
            cmd =>
            {
                cmd.AddParameter("@seniorityId", entity.Seniority.Id);
                cmd.AddParameter("@firstName", entity.FirstName);
                cmd.AddParameter("@lastName", entity.LastName);
            }
        );
        
        public void Update(Employee entity) => NonQuery(
            $"INSERT INTO {EMPLOYEE_TABLE} ({EMPLOYEE_ID}, {EMPLOYEE_SENIORITY_ID}, {EMPLOYEE_FIRST_NAME}, {EMPLOYEE_LAST_NAME}) VALUES " +
            $"(@id, @seniorityId, @firstName, @lastName) " +
            $"ON CONFLICT ({EMPLOYEE_ID}) DO UPDATE SET " +
            $"{EMPLOYEE_SENIORITY_ID} = @seniorityId, {EMPLOYEE_FIRST_NAME} = @firstName, {EMPLOYEE_LAST_NAME} = @lastName",
            cmd =>
            {
                cmd.AddParameter("@id", entity.Id);
                cmd.AddParameter("@seniorityId", entity.Seniority.Id);
                cmd.AddParameter("@firstName", entity.FirstName);
                cmd.AddParameter("@lastName", entity.LastName);
            }
        );

        public void Delete(Employee entity) => Delete(entity.Id);
        public void Delete(int key) => NonQuery(
            $"DELETE FROM {EMPLOYEE_TABLE} WHERE {EMPLOYEE_ID} = @id",
            cmd => { cmd.AddParameter("@id", key); }
        );
    }
}