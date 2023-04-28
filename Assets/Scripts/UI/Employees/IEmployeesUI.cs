namespace Employees.UI.Employees
{
    public interface IEmployeesUI
    {
        public void ShowUI();
        public void HideUI();
        public void AddEmployee(string firstName, string lastName, string seniorityName, string positionName, double salary);
        public void Clear();
    }
}