using System;
using System.Collections.Generic;
using Employees.UI.Interfaces.Seniorities;
using UnityEngine;

namespace Employees.UI.Unity.Seniorities
{
    public class SenioritiesUI : ViewUI, ISenioritiesUI
    {
        [SerializeField] SeniorityRow seniorityRowPrefab;
        [SerializeField] Transform rowsParent;
        readonly List<SeniorityRow> _currentRows = new();
        
        public event Action<int> EmployeesRequested;
        public event Action<string> SortRequested;

        public void AddSeniority(int id, string seniorityName, string positionName, int employeesCount, double baseSalary,
                double percentagePerIncrement, int currentIncrements, double salary)
        {
            SeniorityRow row = Instantiate(seniorityRowPrefab, rowsParent);
            row.Id = id;
            row.Name = seniorityName;
            row.PositionName = positionName;
            row.EmployeesCount = employeesCount;
            row.BaseSalary = baseSalary;
            row.PercentagePerIncrement = percentagePerIncrement;
            row.CurrentIncrements = currentIncrements;
            row.Salary = salary;
            row.EmployeesButtonClicked += OnEmployeesRequested;
            _currentRows.Add(row);
        }

        void OnEmployeesRequested(int seniorityId) => EmployeesRequested?.Invoke(seniorityId);
        
        public void Clear()
        {
            foreach (var row in _currentRows)
                if (row != null)
                    Destroy(row.gameObject);

            _currentRows.Clear();
        }
        
        public void SortByOrdinal() => SortRequested?.Invoke("Ordinal");
        public void SortByPosition() => SortRequested?.Invoke("Position");
        public void SortByBaseSalary() => SortRequested?.Invoke("BaseSalary");
        public void SortByPercentagePerIncrement() => SortRequested?.Invoke("PercentagePerIncrement");
        public void SortByCurrentIncrements() => SortRequested?.Invoke("CurrentIncrements");
        public void SortBySalary() => SortRequested?.Invoke("Salary");
    }
}