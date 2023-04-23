using System.Collections;
using System.Collections.Generic;

namespace Employees.Model.DataAccess
{
    public interface IEmployeeDA : IReadDA<int, Employee>, IWriteDA<int, Employee>
    {
        public IEnumerable<Employee> GetByPosition(Position position);
        public IEnumerable<Employee> GetBySeniority(Seniority seniority);
    }
}