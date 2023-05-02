using System;
using System.Collections.Generic;
using Employees.Model;

namespace Employees.Services.Sorting
{
    public class EmployeesSorter
    {
        public IEnumerable<Employee> SortEmployees(IEnumerable<Employee> employees, EmployeesSortType type, bool ascending = true)
        {
            List<Employee> results = new(employees);
            int sortOrder = ascending ? 1 : -1;
            
            switch (type)
            {
                case EmployeesSortType.FirstName:
                    results.Sort((a, b) => string.Compare(a.FirstName, b.FirstName, StringComparison.Ordinal) * sortOrder);
                    break;
                case EmployeesSortType.LastName:
                    results.Sort((a, b) => string.Compare(a.LastName, b.LastName, StringComparison.Ordinal) * sortOrder);
                    break;
                case EmployeesSortType.Seniority:
                    results.Sort((a, b) => a.Seniority.Ordinal.CompareTo(b.Seniority.Ordinal) * sortOrder);
                    break;
                case EmployeesSortType.Position:
                    results.Sort((a, b) => string.Compare(a.Position.Name, b.Position.Name, StringComparison.Ordinal) * sortOrder);
                    break;
                case EmployeesSortType.Salary:
                    results.Sort((a, b) => a.Seniority.Salary.CompareTo(b.Seniority.Salary) * sortOrder);
                    break;
                case EmployeesSortType.Default:
                    results.Sort((a, b) => a.Id.CompareTo(b.Id) * sortOrder);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, "EmployeesSortType not implemented");
            }

            return results;
        }
    }
}