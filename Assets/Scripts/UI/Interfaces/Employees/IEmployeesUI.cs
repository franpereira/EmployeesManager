namespace Employees.UI.Interfaces.Employees
{
    public interface IEmployeesUI : IViewUI
    {
        public void AddEmployee(string firstName, string lastName, string seniorityName, string positionName, double salary);
        public void Clear();
    }
}