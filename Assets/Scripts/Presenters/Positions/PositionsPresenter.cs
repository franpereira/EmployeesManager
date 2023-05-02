using System;
using System.Collections.Generic;
using System.Linq;
using Employees.Model;
using Employees.Model.DataAccess;
using Employees.Presenters.Employees;
using Employees.Presenters.Seniorities;
using Employees.UI.Interfaces.Positions;
using Employees.Utilities.Sorting;
using UnityEngine;

namespace Employees.Presenters.Positions
{
    public class PositionsPresenter : INavigable
    {
        readonly IPositionsUI _ui;
        readonly PositionEditorPresenter _editorPresenter;
        readonly IDataRepository _repository;
        readonly SenioritiesPresenter _senioritiesPresenter;
        readonly EmployeesPresenter _employeesPresenter;

        List<Position> _currentPositions = new();
        readonly PositionsSorter _sorter = new();
        PositionsSortType _currentSortType = PositionsSortType.Default;
        bool _isSortedAscending = true;

        public PositionsPresenter(IDataRepository repository, IPositionsUI ui, PositionEditorPresenter editorPresenter, SenioritiesPresenter senioritiesPresenter, EmployeesPresenter employeesPresenter)
        {
            _repository = repository;
            _ui = ui;
            _ui.BackRequested += ViewsNavigation.NavigateBack;
            _ui.EditPositionRequested += OnEditPositionRequested;
            _ui.SenioritiesRequested += OnSenioritiesRequested;
            _ui.EmployeesRequested += OnEmployeesRequested;
            _ui.SortRequested += Sort;
            _editorPresenter = editorPresenter;
            _editorPresenter.DataSaved += LoadAllPositions;
            _senioritiesPresenter = senioritiesPresenter;
            _employeesPresenter = employeesPresenter;
        }

        public void ShowUI() => _ui.ShowUI();
        public void HideUI() => _ui.HideUI();
        
        void OnEditPositionRequested(int id) => _editorPresenter.LoadPosition(id);
        
        void OnSenioritiesRequested(int id) => _senioritiesPresenter.LoadSenioritiesByPosition(id);

        void OnEmployeesRequested(int id) => _employeesPresenter.LoadEmployeesByPosition(id);

        void Load(IEnumerable<Position> positions)
        {
            _ui.Clear();
            _currentPositions = new List<Position>(positions);
            foreach (var position in _currentPositions)
            {
                int seniorityCount = _repository.Seniorities.GetByPosition(position).Count();
                int employeesCount = _repository.Employees.GetByPosition(position).Count();
                _ui.AddPosition(position.Id, position.Name, seniorityCount, employeesCount);
            }
        }
        
        public void LoadAllPositions()
        {
            Load(_repository.Positions.GetAll());
            ViewsNavigation.NavigateTo(this);
        }
        
        void Sort(string sortType)
        {
            if (Enum.TryParse<PositionsSortType>(sortType, out var parsedType))
            {
                if (_currentSortType == parsedType)
                    _isSortedAscending = !_isSortedAscending;
                else
                {
                    _currentSortType = parsedType;
                    _isSortedAscending = true;
                }

                Load(_sorter.SortPositions(_currentPositions, parsedType, _isSortedAscending));
            }
            else
                Debug.LogError($"PositionsSortType enum value {sortType} does not exist");
        }
    }
}