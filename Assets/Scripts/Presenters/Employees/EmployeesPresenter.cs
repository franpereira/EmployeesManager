using System;
using System.Collections.Generic;
using Employees.Model;
using Employees.Model.DataAccess;
using Employees.UI.Interfaces.Employees;
using UnityEngine;

namespace Employees.Presenters.Employees
{
    public class EmployeesPresenter : INavigable
    {
        readonly IEmployeesUI _ui;
        readonly IDataRepository _repository;
        
        public EmployeesPresenter(IDataRepository repository, IEmployeesUI ui)
        {
            _repository = repository;
            _ui = ui;
            _ui.BackRequested += ViewsNavigation.NavigateBack;
        }

        public void ShowUI() => _ui.ShowUI();
        public void HideUI() => _ui.HideUI();

        void Load(IEnumerable<Employee> employees)
        {
            _ui.Clear();
            foreach (var employee in employees)
            {
                _ui.AddEmployee(employee.FirstName, employee.LastName, employee.Seniority.Name, employee.Position.Name,
                    employee.Seniority.Salary);
            }
        }
        
        public void LoadAllEmployees()
        {
            Load(_repository.Employees.GetAll());
            ViewsNavigation.NavigateTo(this);
        }

        public void LoadEmployeesByPosition(int positionId)
        {
            var position = _repository.Positions.Get(positionId);
            Load(_repository.Employees.GetByPosition(position));
            ViewsNavigation.NavigateTo(this);
        }
        
        public void LoadEmployeesBySeniority(int seniorityId)
        {
            var seniority = _repository.Seniorities.Get(seniorityId);
            Load(_repository.Employees.GetBySeniority(seniority));
            ViewsNavigation.NavigateTo(this);
        }
    }
}