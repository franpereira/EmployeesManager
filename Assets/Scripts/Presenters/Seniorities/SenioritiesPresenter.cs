using System;
using System.Collections.Generic;
using System.Linq;
using Employees.Model;
using Employees.Model.DataAccess;
using Employees.Presenters.Employees;
using Employees.UI.Interfaces.Seniorities;
using Employees.Utilities.Sorting;
using UnityEngine;

namespace Employees.Presenters.Seniorities
{
    public class SenioritiesPresenter : INavigable
    {
        readonly ISenioritiesUI _ui;
        readonly IDataRepository _repository;
        readonly EmployeesPresenter _employeesPresenter;
        
        List<Seniority> _currentSeniorities = new();
        readonly SenioritiesSorter _sorter = new();
        SenioritiesSortType _currentSortType = SenioritiesSortType.Default;
        bool _isSortedAscending = true;

        public SenioritiesPresenter(IDataRepository repository, ISenioritiesUI ui, EmployeesPresenter employeesPresenter)
        {
            _repository = repository;
            _ui = ui;
            _ui.BackRequested += ViewsNavigation.NavigateBack;
            _ui.EmployeesRequested += OnEmployeesRequested;
            _ui.SortRequested += Sort;
            _employeesPresenter = employeesPresenter;
        }
        
        public void ShowUI() => _ui.ShowUI();
        public void HideUI() => _ui.HideUI();

        void Load(IEnumerable<Seniority> seniorities)
        {
            _ui.Clear();
            _currentSeniorities = new List<Seniority>(seniorities);
            foreach (var seniority in _currentSeniorities)
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

        void Sort(string sortType)
        {
            if (Enum.TryParse<SenioritiesSortType>(sortType, out var parsedType))
            {
                if (_currentSortType == parsedType)
                    _isSortedAscending = !_isSortedAscending;
                else
                {
                    _currentSortType = parsedType;
                    _isSortedAscending = true;
                }
                
                Load(_sorter.SortSeniorities(_currentSeniorities, parsedType, _isSortedAscending));
            }
            else
                Debug.LogError($"SenioritiesSortType enum value {sortType} does not exist.");
        }
    }
}