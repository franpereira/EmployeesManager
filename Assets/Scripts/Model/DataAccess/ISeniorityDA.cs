using System.Collections.Generic;

namespace Employees.Model.DataAccess
{
    public interface ISeniorityDA : IReadDA<int, Seniority>, IWriteDA<int, Seniority>
    {
        public IEnumerable<Seniority> GetByPosition(Position position);
    }
}