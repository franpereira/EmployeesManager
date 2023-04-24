using System;
using System.Collections.Generic;
using Employees.Model;
using UnityEngine;

namespace Employees.UI
{
    public class PositionsUI : MonoBehaviour, IPositionsUI
    {
        [SerializeField] PositionRow positionRowPrefab;
        [SerializeField] Transform rowsParent;
        List<PositionRow> _currentRows = new();

        public event Action<int> EditPositionRequested;
        
        public void ShowUI() => gameObject.SetActive(true);
        
        public void OnEditPositionRequested(int id) => EditPositionRequested?.Invoke(id);

        public void LoadPositions(IEnumerable<Position> positions)
        {
            Clear();
            foreach (var position in positions)
            {
                PositionRow row = Instantiate(positionRowPrefab, rowsParent);
                row.Name = position.Name;
                row.Id = position.Id;
                row.EditButtonClicked += OnEditPositionRequested;
                _currentRows.Add(row);
            }
        }

        void Clear()
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