using System.Collections.Generic;
using System.Linq;
using Employees.Model;
using Employees.Model.DataAccess;
using Employees.Presenters.Employees;
using Employees.UI.Interfaces.Seniorities;

namespace Employees.Presenters.Seniorities
{
    public class SenioritiesPresenter : INavigable
    {
        readonly ISenioritiesUI _ui;
        readonly IDataRepository _repository;
        readonly EmployeesPresenter _employeesPresenter;
        
        public SenioritiesPresenter(IDataRepository repository, ISenioritiesUI ui, EmployeesPresenter employeesPresenter)
        {
            _repository = repository;
            _ui = ui;
            _ui.BackRequested += ViewsNavigation.NavigateBack;
            _ui.EmployeesRequested += OnEmployeesRequested;
            _employeesPresenter = employeesPresenter;
        }
        
        public void ShowUI() => _ui.ShowUI();
        public void HideUI() => _ui.HideUI();

        void Load(IEnumerable<Seniority> seniorities)
        {
            _ui.Clear();
            foreach (var seniority in seniorities)
            {
                int employeesCount = _repository.Employees.GetBySeniority(seniority).Count(); // Having a CountBySeniority() could be better?
                _ui.AddSeniority(seniority.Id, seniority.Name, seniority.Position.Name, employeesCount, seniority.BaseSalary, seniority.PercentagePerIncrement,
                    seniority.CurrentIncrements, seniority.Salary);
            }
        }

        public void LoadAllSeniorities()
        {
            Load(_repository.Seniorities.GetAll());
            ViewsNavigation.NavigateTo(this);
        }

        public void LoadSenioritiesByPosition(int positionId)
        {
            var position = _repository.Positions.Get(positionId);
            Load(_repository.Seniorities.GetByPosition(position));
            ViewsNavigation.NavigateTo(this);
        }
        
        
        
        void OnEmployeesRequested(int seniorityId) => _employeesPresenter.LoadEmployeesBySeniority(seniorityId);
    }
}