using System.Collections.Generic;
using UnityEngine;

namespace Employees.UI.Seniorities
{
    public class SenioritiesUI : MonoBehaviour, ISenioritiesUI
    {
        [SerializeField] SeniorityRow seniorityRowPrefab;
        [SerializeField] Transform rowsParent;
        readonly List<SeniorityRow> _currentRows = new();

        public void ShowUI() => gameObject.SetActive(true);

        public void HideUI() => gameObject.SetActive(false);

        public void AddSeniority(string seniorityName, string positionName, int employeesCount, double baseSalary,
                double percentagePerIncrement, int currentIncrements, double salary)
        {
            SeniorityRow row = Instantiate(seniorityRowPrefab, rowsParent);
            row.Name = seniorityName;
            row.PositionName = positionName;
            row.EmployeesCount = employeesCount;
            row.BaseSalary = baseSalary;
            row.PercentagePerIncrement = percentagePerIncrement;
            row.CurrentIncrements = currentIncrements;
            row.Salary = salary;
            _currentRows.Add(row);
        }

        public void Clear()
        {
            foreach (var row in _currentRows) Destroy(row.gameObject);

            _currentRows.Clear();
        }
    }
}