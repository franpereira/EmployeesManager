namespace Employees.Model.DataAccess
{
    public interface IDataRepository
    {
        public IPositionDA Positions { get; }
        public ISeniorityDA Seniorities { get; }
        public IEmployeeDA Employees { get; }
    }
}