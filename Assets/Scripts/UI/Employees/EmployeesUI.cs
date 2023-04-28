using System.Collections.Generic;
using UnityEngine;

namespace Employees.UI.Employees
{
    public class EmployeesUI : MonoBehaviour, IEmployeesUI
    {
        [SerializeField] EmployeeRow employeeRowPrefab;
        [SerializeField] Transform rowsParent;
        readonly List<EmployeeRow> _currentRows = new();

        public void ShowUI() => gameObject.SetActive(true);

        public void HideUI() => gameObject.SetActive(false);

        public void AddEmployee(string firstName, string lastName, string seniorityName, string positionName, double salary)
        {
            EmployeeRow row = Instantiate(employeeRowPrefab, rowsParent);
            row.FirstName = firstName;
            row.LastName = lastName;
            row.Seniority = seniorityName;
            row.Position = positionName;
            row.Salary = salary;
        }

        public void Clear()
        {
            foreach (var row in _currentRows) Destroy(row.gameObject);
        }
    }
}