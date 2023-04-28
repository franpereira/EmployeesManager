using Employees.Model.DataAccess;
using Employees.UI.Employees;

namespace Employees.Presenters.Employees
{
    public class EmployeesPresenter
    {
        readonly IEmployeesUI _ui;
        readonly IDataRepository _repository;
        
        public EmployeesPresenter(IDataRepository repository, IEmployeesUI ui)
        {
            _repository = repository;
            _ui = ui;
        }

        public void LoadAllEmployees()
        {
            _ui.Clear();
            var employees = _repository.Employees.GetAll();
            foreach (var employee in employees)
            {
                _ui.AddEmployee(employee.FirstName, employee.LastName, employee.Seniority.Name, employee.Position.Name,
                    employee.Seniority.Salary);
            }
            _ui.ShowUI();
        }
    }
}