using System;
using System.Collections.Generic;
using UnityEngine;

namespace Employees.UI.Positions
{
    public class PositionsUI : MonoBehaviour, IPositionsUI
    {
        [SerializeField] PositionRow positionRowPrefab;
        [SerializeField] Transform rowsParent;
        List<PositionRow> _currentRows = new();

        public event Action<int> EditPositionRequested;
        
        public void ShowUI() => gameObject.SetActive(true);
        public void HideUI() => gameObject.SetActive(false);

        public void OnEditPositionRequested(int id) => EditPositionRequested?.Invoke(id);

        public void AddPosition(string positionName, int seniorityCount, int employeesCount)
        {
            PositionRow row = Instantiate(positionRowPrefab, rowsParent);
            row.Name = positionName;
            row.SeniorityCount = seniorityCount;
            row.EmployeesCount = employeesCount;
            row.EditButtonClicked += OnEditPositionRequested;
            _currentRows.Add(row);
        }

        public void Clear()
        {
            foreach (var row in _currentRows)
            {
                row.EditButtonClicked -= OnEditPositionRequested;
                Destroy(row.gameObject);
            }
            _currentRows.Clear();
        }
    }
}