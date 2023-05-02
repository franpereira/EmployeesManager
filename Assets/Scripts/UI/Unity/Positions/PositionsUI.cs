using System;
using System.Collections.Generic;
using Employees.UI.Interfaces.Positions;
using UnityEngine;

namespace Employees.UI.Unity.Positions
{
    public class PositionsUI : ViewUI, IPositionsUI
    {
        [SerializeField] PositionRow positionRowPrefab;
        [SerializeField] Transform rowsParent;
        readonly List<PositionRow> _currentRows = new();

        public event Action<int> EditPositionRequested;
        public event Action<int> SenioritiesRequested;
        public event Action<int> EmployeesRequested;
        
        public event Action<string> SortRequested; 

        public void OnEditPositionRequested(int id) => EditPositionRequested?.Invoke(id);

        public void AddPosition(int id, string positionName, int seniorityCount, int employeesCount)
        {
            PositionRow row = Instantiate(positionRowPrefab, rowsParent);
            row.Id = id;
            row.Name = positionName;
            row.SeniorityCount = seniorityCount;
            row.EmployeesCount = employeesCount;
            row.EditButtonClicked += OnEditPositionRequested;
            row.SenioritiesButtonClicked += OnSenioritiesRequested;
            row.EmployeesButtonClicked += OnEmployeesRequested;
            _currentRows.Add(row);
        }

        void OnSenioritiesRequested(int positionId) => SenioritiesRequested?.Invoke(positionId);
        void OnEmployeesRequested(int positionId) => EmployeesRequested?.Invoke(positionId);
        
        public void Clear()
        {
            foreach (var row in _currentRows)
            {
                row.EditButtonClicked -= OnEditPositionRequested;
                row.EmployeesButtonClicked -= OnEmployeesRequested;
                Destroy(row.gameObject);
            }
            _currentRows.Clear();
        }
        
        public void SortByName() => SortRequested?.Invoke("Name");
    }
}