using System;
using System.Collections.Generic;
using Employees.UI.Interfaces.Employees;
using UnityEngine;

namespace Employees.UI.Unity.Employees
{
    public class EmployeesUI : ViewUI, IEmployeesUI
    {
        [SerializeField] EmployeeRow employeeRowPrefab;
        [SerializeField] Transform rowsParent;
        readonly List<EmployeeRow> _currentRows = new();

        public void AddEmployee(string firstName, string lastName, string seniorityName, string positionName, double salary)
        {
            EmployeeRow row = Instantiate(employeeRowPrefab, rowsParent);
            row.FirstName = firstName;
            row.LastName = lastName;
            row.Seniority = seniorityName;
            row.Position = positionName;
            row.Salary = salary;
            _currentRows.Add(row);
        }

        public void Clear()
        {
            foreach (var row in _currentRows)
                if (row != null)
                    Destroy(row.gameObject);
        }
    }
}