using System;
using System.Collections.Generic;
using Employees.Model;
using Employees.Model.DataAccess;
using Employees.Services.Sorting;
using Employees.UI.Interfaces.Employees;
using UnityEngine;

namespace Employees.Presenters.Employees
{
    public class EmployeesPresenter : INavigable
    {
        readonly IEmployeesUI _ui;
        readonly IDataRepository _repository;

        List<Employee> _currentEmployees = new();
        readonly EmployeesSorter _sorter = new();
        EmployeesSortType _currentSortType = EmployeesSortType.Default;
        bool _isSortedAscending = true;

        public EmployeesPresenter(IDataRepository repository, IEmployeesUI ui)
        {
            _repository = repository;
            _ui = ui;
            _ui.BackRequested += ViewsNavigation.NavigateBack;
            _ui.SortRequested += Sort;
        }

        public void ShowUI() => _ui.ShowUI();
        public void HideUI() => _ui.HideUI();

        void Load(IEnumerable<Employee> employees)
        {
            _ui.Clear();
            _currentEmployees = new List<Employee>(employees);
            foreach (var employee in _currentEmployees)
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

        void Sort(string sortType)
        {
            if (Enum.TryParse<EmployeesSortType>(sortType, out var type))
            {
                if (_currentSortType == type)
                    _isSortedAscending = !_isSortedAscending;
                else
                {
                    _currentSortType = type;
                    _isSortedAscending = true;   
                }

                Load(_sorter.SortEmployees(_currentEmployees, type, _isSortedAscending));
            }
            else
                Debug.LogError($"EmployeesSortType {sortType} enum value does not exist");
        }
    }
}