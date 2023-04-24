using System;
using System.Collections.Generic;
using Employees.Model;
using UnityEngine;

namespace Employees.UI.Seniorities
{
    public class SenioritiesUI : MonoBehaviour, ISenioritiesUI
    {
        [SerializeField] SeniorityRow seniorityRowPrefab;
        [SerializeField] Transform rowsParent;
        readonly List<SeniorityRow> _currentRows = new();

        public void LoadSeniorities(IEnumerable<Seniority> seniorities)
        {
            Clear();
            foreach (var seniority in seniorities)
            {
                SeniorityRow row = Instantiate(seniorityRowPrefab, rowsParent);
                row.Name = seniority.Name;
                //Debug.Log(seniority.Position == null ? "null" : seniority.Position.Name);
                row.PositionName = seniority.Position.Name;
                _currentRows.Add(row);
            }
        }

        public void ShowUI() => gameObject.SetActive(true);

        public void HideUI() => gameObject.SetActive(false);

        void Clear()
        {
            foreach (var row in _currentRows)
            {
                Destroy(row.gameObject);
            }
            _currentRows.Clear();
        }
    }
}